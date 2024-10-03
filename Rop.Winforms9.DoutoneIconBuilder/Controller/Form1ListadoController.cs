using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Rop.Winforms8._1.DoutoneIconBuilder.Controller
{
    public class Form1ListadoController : BaseController<Form1>
    {
        public BankJson BankJson=> ParentForm.Controller.BankJson;
        public Form1Controller Controller => ParentForm.Controller;
        public ListBox Listado => ParentForm.listado;
        public ComboBox Filter => ParentForm.cbfilter;
        public ListFilter CurrentFilter => (Filter.SelectedIndex>=0)?(ListFilter)Filter.SelectedIndex:ListFilter.All;
        public BmpIcon? SelectedIcon => ParentForm.Controller.SelectedIcon;
        public int SelectedIndex => Listado.SelectedIndex;
        public BmpIcon? GetItem(int index) => (index < 0 || index >= Listado.Items.Count) ? null:(Listado.Items[index] as BmpIcon);

        private bool _isUserClick;
        public Form1ListadoController(Form1 parentForm) : base(parentForm)
        {
            AjFilter();
            Listado.DrawItem += Listado_DrawItem;
            Listado.MouseDown += Listado_MouseDown;
            Listado.SelectedIndexChanged += Listado_SelectedIndexChanged1;
            Filter.SelectedIndexChanged += Filter_SelectedIndexChanged;
        }

        private void Listado_SelectedIndexChanged1(object? sender, EventArgs e)
        {
            try
            {
                if (_isUserClick)
                {
                    _isUserClick = false;
                    DirectSelectedIndexChanged();
                }
                else
                {
                    FixSelectedIconChanged();
                }
            }
            finally
            {
                _isUserClick = false;
            }
        }
        private int _getindex(BmpIcon? item)
        {
            if (item == null) return -1;
            for (var i = 0; i < Listado.Items.Count; i++)
            {
                if (Listado.Items[i] is BmpIcon b && b.Code == item.Code) return i;
            }
            return -1;
        }

        public void FixSelectedIconChanged()
        {
            var item = Listado.SelectedItem as BmpIcon;
            var sitem=Controller.SelectedIcon;
            if (item==null && sitem == null) return;
            if (item?.Code== sitem?.Code) return;
            if (sitem == null)
            {
                Listado.SelectedIndex = -1;
                return;
            }
            var i = _getindex(sitem);
            Listado.SelectedIndex = i;
        }

        private void DirectSelectedIndexChanged()
        {
            var item = Listado.SelectedItem as BmpIcon;
            if (Controller.Status == Form1Status.Editing) return;
            if (item == null)
            {
                Controller.SetStatusNone();
                if (Listado.SelectedItem is string)
                {
                    DoAddNew();
                }
                return;
            }
            Controller.SetStatusSelected(item);
        }

        private void Listado_MouseDown(object? sender, MouseEventArgs e)
        {
            var p=Listado.IndexFromPoint(e.Location);
            if (p==ListBox.NoMatches) return;
            if (e.Button==MouseButtons.Left)
            {
                _isUserClick = true;
            }
        }

        private void Filter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (Controller.Status==Form1Status.Editing) 
                ParentForm.Controller.SetStatusNone();
            AjListado();
        }
        private void AjFilter()
        {
            Filter.BeginUpdate();
            Filter.Items.Clear();
            Filter.Items.AddRange(Enum.GetNames(typeof(ListFilter)).ToArray<object>());
            Filter.EndUpdate();
            Filter.SelectedIndex = 0;
        }

        private void Listado_DrawItem(object? sender, DrawItemEventArgs e)
        {
            var i = e.Index;
            if (i < 0) return;
            var item = Listado.Items[e.Index] as BmpIcon;
            if (item == null)
            {
                if (Listado.Items[e.Index] is string s)
                {
                    e.DrawBackground();
                    e.Graphics.DrawString(s, e.Font ?? Listado.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y);
                }
                return;
            }
            e.DrawBackground();
            if (item.IsNew)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Yellow),e.Bounds.X+4, e.Bounds.Y,e.Bounds.Height,e.Bounds.Height);
            }
            if (item.IsExtra)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Red),e.Bounds.X, e.Bounds.Y,4,e.Bounds.Height);
            }
            
            var f=e.Font?? Listado.Font;
            var bmp = Controller.GetBitmap(item.Code);
            var szstr = $"{item.Size.Width}x{item.Size.Height}";
            var m = e.Graphics.MeasureString(szstr, f);
            var h=m.Height;
            var y0=e.Bounds.Y+ (e.Bounds.Height - h) / 2;
            var x0 = e.Bounds.X+4;
            e.Graphics.DrawImage(bmp, x0,e.Bounds.Y,e.Bounds.Height,e.Bounds.Height);
            x0 += e.Bounds.Height;
            var isselected=e.State.HasFlag(DrawItemState.Selected);
            var forecolor=(isselected) ? Color.White:Color.Black;
            if (!Listado.Enabled)
                forecolor = Color.Silver;
            e.Graphics.DrawString(szstr, e.Font??Listado.Font, new SolidBrush(forecolor), x0, y0);
            x0 += (int)m.Width+3;
            e.Graphics.DrawString(item.Code, e.Font??Listado.Font, new SolidBrush(forecolor), x0, y0);
            var alias = BankJson.GetAlias(item);
            if (alias.Any())
            {
                var m2 = e.Graphics.MeasureString(item.Code,f);
                x0 += (int)m2.Width+8;
                var str = string.Join(", ",alias);
                e.Graphics.DrawString(str, e.Font??Listado.Font, new SolidBrush(Color.Red), x0, y0);
            }
        }

        protected override void Init()
        {
            AjListado();
            Controller.StatusChanged += Controller_StatusChanged;
            Controller.BankJsonChanged += Controller_BankJsonChanged;
        }

        private void Controller_BankJsonChanged(object? sender, EventArgs e)
        {
            AjListado();
        }
        private void Controller_StatusChanged(object? sender, EventArgs e)
        {
            switch (Controller.Status)
            {
                case Form1Status.None:
                    Listado.Enabled = true;
                    break;
                case Form1Status.Selected:
                    Listado.Enabled = true;
                    break;
                case Form1Status.Editing:
                    Listado.Enabled = false;
                    break;
                default:
                    Listado.Enabled = false;
                    break;
            }
            FixSelectedIconChanged();
        }
        private void DoAddNew()
        {
            BankJson.AddNew();
            ParentForm.Controller.SaveBankJson();   
        }

        private void AjListado()
        {
            var top=ParentForm.listado.TopIndex;
            var topitem=(top>=0 && top<Listado.Items.Count ) ? Listado.Items[top] as BmpIcon : null;
            var lst = BankJson.Icons.Values.AsEnumerable();
            switch (CurrentFilter)
            {
                case ListFilter.All:
                    break;
                case ListFilter.New:
                    lst = lst.Where(i => i.IsNew);
                    break;
                case ListFilter.Standard:
                    lst = lst.Where(i => !i.IsExtra);
                    break;
                case ListFilter.Aliased:
                    lst = lst.Where(i => BankJson.IsAliased(i));
                    break;
                case ListFilter.Extra:
                    lst = lst.Where(i => i.IsExtra);
                    break;
                case ListFilter.NoBaseline:
                    lst = lst.Where(i => i.BaseLine==0 || i.BaseLine==i.Size.Height);
                    break;
            }
            Listado.BeginUpdate();
            Listado.Items.Clear();
            Listado.Items.AddRange(lst.OrderBy(v=>v.Code).ToArray<object>());
            Listado.Items.Add("<Add>");
            Listado.EndUpdate();
            Listado.TopIndex = _getindex(topitem);
            FixSelectedIconChanged();
        }
    }

    public enum ListFilter
    {
        All,
        New,
        Standard,
        Extra,
        Aliased,
        NoBaseline
    }
}
