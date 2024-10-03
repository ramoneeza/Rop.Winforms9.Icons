using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.Winforms8.DuotoneIcons;

namespace Rop.Winforms8._1.DoutoneIconBuilder.Controller
{
    public class Form1PanelMainController: BaseController<Form1>
    {
        public Form1Controller Controller => ParentForm.Controller;
        public Form1Status Status => Controller.Status;
        public BmpIcon? SelectedIcon=> Controller.SelectedIcon;
        public Lienzo Lienzo => ParentForm.lienzo;
        public ComboBox CbColor=>ParentForm.cbcolor;
        public ColorMode ColorMode => (ColorMode)CbColor.SelectedIndex;
        public int Zoom { get; private set; }
        public Form1PanelMainController(Form1 parentForm) : base(parentForm)
        {
            AjCbColor();
            ParentForm.lbiconname.Text = "";
            ParentForm.lbposition.Text = "";
            ParentForm.lbbaseline.Text = "";
        }

        private void AjCbColor()
        {
            CbColor.BeginUpdate();
            CbColor.Items.Clear();
            CbColor.Items.AddRange(Enum.GetNames(typeof(ColorMode)).ToArray<object>());
            CbColor.EndUpdate();
            CbColor.SelectedIndex = 0;
            CbColor.DrawItem += CbColor_DrawItem;
        }

        private void CbColor_DrawItem(object? sender, DrawItemEventArgs e)
        {
            var i = e.Index;
            if (i < 0) return;
            var item = (ColorMode)i;
            var g = e.Graphics;
            var r = e.Bounds;
            var t = item.ToString();
            var rg=new Rectangle(r.X+1, r.Y,4*3, r.Height-2);
            var r0=new Rectangle(r.X+1, r.Y,4, r.Height-2);
            var r1=new Rectangle(r0.X+4, r0.Y,r0.Width, r0.Height);
            var r2=new Rectangle(r1.X+4, r1.Y,r1.Width, r1.Height);
            var rt= new Rectangle(r2.X + 5, r2.Y, r.Width - r2.X-5, r.Height);
            g.FillRectangle(Brushes.White, rg);
            var (c1,c2,c3)= item switch
            {
                ColorMode.Cicle => (Color.White, Color.Black, Color.Gray),
                ColorMode.Transparent => (Color.White,Color.White,Color.White),
                ColorMode.Color1 => (Color.Black, Color.Black, Color.Black),
                ColorMode.Color2 => (Color.Gray, Color.Gray, Color.Gray),
                _ => (Color.Black, Color.Black, Color.Black)
            };
            g.FillRectangle(new SolidBrush(c1), r0);
            g.FillRectangle(new SolidBrush(c2), r1);
            g.FillRectangle(new SolidBrush(c3), r2);
            g.DrawRectangle(Pens.Black, rg);
            g.DrawString(t, e.Font, Brushes.Black, rt);
        }

        protected override void Init()
        {
            ParentForm.Controller.StatusChanged += Controller_StatusChanged;
            ParentForm.btneditsave.Click += Btneditsave_Click;
            ParentForm.btncancel.Click += Btncancel_Click;
            ParentForm.lienzo.PixelClick += Lienzo_PixelClick;
            ParentForm.lienzo.PixelMove += Lienzo_PixelMove;
            ParentForm.lienzo.CursorPositionChanged += _lienzoCursorPositionChanged;
            ParentForm.edbaseline.ValueChanged += Edbaseline_ValueChanged;
            ParentForm.btnalias.Click += Btnalias_Click;
        }

        private void Lienzo_PixelMove(object? sender, (Point, MouseButtons) t)
        {
            if (SelectedIcon == null) return;
            if (Status != Form1Status.Editing) return;
            var (e, b) = t;
            if (b!=MouseButtons.Left && b!=MouseButtons.Right) return;
            Lienzo.SetPixel(e.X, e.Y, _movecolor);
        }

        private void Btnalias_Click(object? sender, EventArgs e)
        {
            if (SelectedIcon == null) return;
            using var f = new FormAliasDialog(SelectedIcon,ParentForm.Controller.BankJson.GetAlias(SelectedIcon));
            var r = f.ShowDialog();
            if (r != System.Windows.Forms.DialogResult.OK) return;
            ParentForm.Controller.ChangeAlias(SelectedIcon, f.Alias);
        }

        private void Edbaseline_ValueChanged(object? sender, EventArgs e)
        {
            if (SelectedIcon == null) return;
            if (Status!= Form1Status.Editing) return;
            Lienzo.Baseline=(int)ParentForm.edbaseline.Value;
            ParentForm.ejemplo.BaseLine=Lienzo.Baseline;
        }

        private int _movecolor = 0;
        private void Lienzo_PixelClick(object? sender, (Point, MouseButtons) t)
        {
            if (SelectedIcon == null) return;
            if (Status != Form1Status.Editing) return;
            var (e, b) = t;
            if (b!=MouseButtons.Left && b!=MouseButtons.Right) return;
            var cm = ColorMode;
            var (c1,c2)=cm switch
            {
                ColorMode.Cicle => (4,0),
                ColorMode.Transparent => (0,1),
                ColorMode.Color1 => (1,0),
                ColorMode.Color2 => (2,0),
                _ => (4,0)
            };
            var cf = (b == MouseButtons.Left) ? c1 : c2;
            var old = Lienzo.GetPixel(e.X, e.Y);
            var np=cf switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                4 => (old<2)?(old+1):0,
                _ => 1
            };
            _movecolor = np;
            if (Control.ModifierKeys.HasFlag(Keys.Control))
            {
                Lienzo.Fill(e.X, e.Y, np);
            }
            else
            {
                Lienzo.SetPixel(e.X, e.Y, np);
            }

        }

        private void _lienzoCursorPositionChanged(object? sender, Point e)
        {
            AjTop();
        }

        private void AjTop()
        {
            var e = Lienzo.CursorPosition;
            if (e.X < 0)
            {
                ParentForm.lbposition.Text = "";
            }
            else
                ParentForm.lbposition.Text = $"X:{e.X} Y:{e.Y}";
            ParentForm.lbiconname.Text = SelectedIcon?.Code ?? "";
            ParentForm.lbbaseline.Text = $"B:{SelectedIcon?.BaseLine}";
        }

        private void Btncancel_Click(object? sender, EventArgs e)
        {
            if (SelectedIcon == null) return;
            Controller.SetStatusSelected(SelectedIcon);
        }

        private void Btneditsave_Click(object? sender, EventArgs e)
        {
            if (SelectedIcon == null) return;
            switch (Status)
            {
                case Form1Status.None:
                    break;
                case Form1Status.Selected:
                    Controller.SetStatusEditing(SelectedIcon);
                    break;
                case Form1Status.Editing:
                    DoSave();
                    break;
            }
        }

        public void DoSave()
        {
            var s = SelectedIcon;
            if (SelectedIcon == null) return;
            if (Lienzo.BmpIcon==null) return;
            var bmp = Lienzo.BmpIcon.Data;
            var baseLine = Lienzo.Baseline;
            SelectedIcon.BaseLine = baseLine;
            SelectedIcon.Data = bmp;
            SelectedIcon.IsNew = false;
            Controller.SaveIcon(s);
        }

        private void Controller_StatusChanged(object? sender, EventArgs e)
        {
            AjPanel();
        }
        private void AjPanel()
        {
           if (Status==Form1Status.None|| SelectedIcon == null)
           {
             ParentForm.panelmain.Visible = false;
             ParentForm.btneditsave.Enabled = false;
             ParentForm.btncancel.Visible = false;
             ParentForm.edbaseline.Visible= false;
             ParentForm.btnalias.Visible = false;
             ParentForm.lbiconname.Text = "";
             ParentForm.lbposition.Text = "";
             ParentForm.lbbaseline.Text = "";
             ParentForm.ejemplo.Visible = false;
             ParentForm.panel4.Visible = false;
                return;
           }

           ParentForm.panel4.Visible = true;
           ParentForm.panelmain.Visible = true;
           Lienzo.BmpIcon = SelectedIcon;
           Lienzo.Baseline = SelectedIcon.BaseLine;
           ParentForm.ejemplo.Visible = true;
           ParentForm.ejemplo.BaseLine = Lienzo.Baseline;
           ParentForm.ejemplo.Bitmap = Lienzo.BmpIcon.Bitmap;
            ParentForm.edbaseline.Visible= true;
           ParentForm.edbaseline.Value = SelectedIcon.BaseLine;
           ParentForm.edbaseline.Maximum = SelectedIcon.Size.Height;
           ParentForm.btnalias.Visible = true;
            if (Status == Form1Status.Editing)
           {
               Lienzo.Editing = true;
               ParentForm.btneditsave.Text = "Save";
               ParentForm.btneditsave.Enabled = true;
               ParentForm.btncancel.Visible = true;
               ParentForm.edbaseline.Enabled = true;
            }
           else
           {
              Lienzo.Editing = false;
              ParentForm.btneditsave.Text = "Edit";
              ParentForm.btneditsave.Enabled = true;
              ParentForm.btncancel.Visible = false;
              ParentForm.edbaseline.Enabled = false;
           }
            AjTop();
        }
        
    }

    public enum ColorMode
    {
        Cicle,
        Transparent,
        Color1,
        Color2
    }
}
