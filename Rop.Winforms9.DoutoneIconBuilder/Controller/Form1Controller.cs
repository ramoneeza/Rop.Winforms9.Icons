using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Rop.Winforms8.DuotoneIcons;
using Rop.Winforms9.DoutoneIconBuilder;
using Rop.Winforms9.DoutoneIconBuilder.Controller;

namespace Rop.Winforms8._1.DoutoneIconBuilder.Controller
{
    
    public class Form1Controller:BaseController<Form1>
    {
        public BankJson BankJson { get; private set; }
        public string BasePath=>BankJson.BasePath;
        public Form1ListadoController ListadoController => ParentForm.ListadoController;
        public Form1Status Status { get; private set; }
        private BmpIcon? _selectedIcon;

        public BmpIcon? SelectedIcon
        {
            get => _selectedIcon;
            private set
            {
                _selectedIcon = value;
                ListadoController.FixSelectedIconChanged();
            }
        }

        public event EventHandler? StatusChanged;
        public event EventHandler? BankJsonChanged;
        public Form1Controller(Form1 parentForm,BankJson bankjson) : base(parentForm)
        {
            //BankJson= BankJson.Load(parentForm.OriginalPath.Path) ?? throw new Exception("No se ha podido cargar el archivo bank.json");
            BankJson = bankjson;
            ParentForm.KeyPreview = true;
            ParentForm.KeyDown += ParentForm_KeyDown;
            AjButtons(ParentForm);
            ParentForm.btnsavebin.Click += Btnsavebin_Click;
            ParentForm.btntest.Click += Btntest_Click;
            ParentForm.btnupgrade.Click += Btnupgrade_Click;
            ParentForm.barra.Visible = false;
        }

        private void Btnupgrade_Click(object? sender, EventArgs e)
        {
            if (Status==Form1Status.Editing) return;
            // abrir un dialogo para seleccionar la carpeta
            using var ffolder = new FolderBrowserDialog();
            ffolder.Description = "Select new icons folder";
            var r=ffolder.ShowDialog();
            if (r != DialogResult.OK) return;
            var folder= ffolder.SelectedPath;
            if (folder == BasePath) return;
            using var formaddnew = new FormAddNewIcons(folder,BasePath);
            formaddnew.ShowDialog();
            if (formaddnew.DialogResult != DialogResult.OK) return;
            
            var all=formaddnew.listado2.Items.Cast<SvgFile>().ToList();
            foreach (var svgFile in all)
            {
                var png = SvgHelper.SvgToPng(svgFile.Path,BankJson.BaseSize.Height);
                if (png== null) continue;
                File.WriteAllBytes(Path.Combine(BankJson.BankPath, svgFile.Name + ".png"), png);
            }
            SetStatusNone();
            BankJson.AddNew();
            ParentForm.Controller.SaveBankJson();  
        }

        private void Btntest_Click(object? sender, EventArgs e)
        {
            var icon = BankJson.Icons.Values.First();
            var bmp = GetBitmap(icon.Code);
            var normal = Converter.Normalize(bmp);
            var cuatro = Converter.From32BTo4B(bmp);
            var cuatro2=Converter.From32BTo4B(normal);
            Debug.Assert(cuatro.Length==cuatro2.Length);
            Debug.Assert(cuatro.SequenceEqual(cuatro2));
            var to32=Converter.From4BTo32B(cuatro);
            Debug.Assert(normal.SequenceEqual(to32));
        }

        private async void Btnsavebin_Click(object? sender, EventArgs e)
        {
            await DoSaveBinary();
        }

        private async Task DoSaveBinary()
        {
            ParentForm.btnsavebin.Enabled=false;
            ParentForm.barra.Visible = true;
            ParentForm.Enabled = false;
            var p = new Progress<(int, int)>();
            p.ProgressChanged += (_,t) =>
            {
                var (i, m) = t;
                if (i == -1 || m == -1)
                {
                    ParentForm.barra.Style = ProgressBarStyle.Marquee;
                    return;
                }
                ParentForm.barra.Style = ProgressBarStyle.Continuous;
                ParentForm.barra.Maximum = m;
                ParentForm.barra.Value = i;
            };
            await Task.Run(() =>BankJson.SaveBinary(p));
            ParentForm.barra.Visible = false;
            ParentForm.btnsavebin.Enabled = true;
            ParentForm.Enabled = true;
        }

        

        private void AjButtons(Control c)
        {
            if (!c.HasChildren) return;
            foreach (Control hijo in c.Controls)
            {
                if (hijo is Button b)
                {
                    b.PreviewKeyDown += GenButton_Keydown;
                }
                if (hijo.HasChildren)
                {
                    AjButtons(hijo);
                }
            }
        }

        private void GenButton_Keydown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        private void ParentForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SetStatusNone();
                e.Handled = true;
                return;
            }

            if (Status != Form1Status.Editing)
            {

                if (e.KeyCode == Keys.Down)
                {
                    var i = ListadoController.SelectedIndex;
                    var next = ListadoController.GetItem(i + 1);
                    SetStatusSelected(next);
                    e.Handled = true;
                    return;
                }

                if (e.KeyCode == Keys.Up)
                {
                    var i = ListadoController.SelectedIndex;
                    var prev= ListadoController.GetItem(i - 1);
                    SetStatusSelected(prev);
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.E)
                {
                    if (SelectedIcon == null) return;
                    SetStatusEditing(SelectedIcon);
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.PageDown)
                {
                    var i = ListadoController.SelectedIndex+10;
                    if (i >= ParentForm.listado.Items.Count) i = ParentForm.listado.Items.Count - 1;
                    var next = ListadoController.GetItem(i);
                    SetStatusSelected(next);
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.PageUp)
                {
                    var i = ListadoController.SelectedIndex-10;
                    if (i < 0) i = 0;
                    var prev = ListadoController.GetItem(i);
                    SetStatusSelected(prev);
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.P)
                {
                    if (SelectedIcon == null) return;
                    var i = ListadoController.SelectedIndex;
                    if (i <= 0) return;
                    var item= ListadoController.GetItem(i-1);
                    if (item== null) return;
                    SetStatusEditing(SelectedIcon);
                    ParentForm.edbaseline.Value = item.BaseLine;
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.N)
                {
                    if (SelectedIcon == null) return;
                    var i = ListadoController.SelectedIndex;
                    var item= ListadoController.GetItem(i + 1);
                    if (item== null) return;
                    SetStatusEditing(SelectedIcon);
                    ParentForm.edbaseline.Value = item.BaseLine;
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.A && e.Alt)
                {
                    DoNormalizeNew();
                    e.Handled = true;
                    SetStatusNone();
                    return;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Down)
                {
                    try
                    {
                        ParentForm.edbaseline.Value++;
                    }
                    catch
                    {
                        
                    }

                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.Up)
                {
                    try
                    {
                        ParentForm.edbaseline.Value--;
                    }
                    catch
                    {
                        
                    }

                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.S)
                {
                    ParentForm.PanelMainController.DoSave();
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.P)
                {
                    var i =  ListadoController.SelectedIndex;
                    var item= ListadoController.GetItem(i - 1);
                    if (item== null) return;
                    ParentForm.edbaseline.Value = item.BaseLine;
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.N)
                {
                    var i = ListadoController.SelectedIndex;
                    var item= ListadoController.GetItem(i + 1);
                    if (item== null) return;
                    ParentForm.edbaseline.Value = item.BaseLine;
                    e.Handled = true;
                    return;
                }
            }
        }

        private void DoNormalizeNew()
        {
            foreach (var icon in BankJson.Icons.Values.Where(i=>i.IsNew))
            {
                var w=icon.Size.Width/3;
                var bs=icon.Size.Height;
                while (bs > 0)
                {
                    var p = icon.GetPixel(w, bs-1);
                    if (p!=0) break;
                    bs--;
                }
                icon.BaseLine = bs;
            }
            SaveBankJson();
        }


        public void SetStatusNone()
        {
            SelectedIcon= null;
            Status = Form1Status.None;
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
        public void SetStatusSelected(BmpIcon? icon)
        {

            if (icon == null)
            {
                SetStatusNone();
                return;
            }
            SelectedIcon = icon;
            Status = Form1Status.Selected;
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
        public void SetStatusEditing(BmpIcon? icon)
        {
            if (icon== null)
            {
                SetStatusNone();
                return;
            }

            SelectedIcon = icon;
            Status = Form1Status.Editing;
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SaveBankJson()
        {
            BankJson.Save();
            BankJsonChanged?.Invoke(this, EventArgs.Empty);
        }
        public Bitmap GetBitmap(string icon)
        {
            return BankJson.Icons[icon].Bitmap;
        }
        public void SaveIcon(BmpIcon? selectedIcon)
        {
            if (selectedIcon==null) return;
            selectedIcon.SaveBitmap();
            SaveBankJson();
            SetStatusSelected(selectedIcon);
        }

        public void UpdateBankJson(BankJson bankJson)
        {
            BankJson = bankJson;
            SaveBankJson();
        }

        public void ChangeAlias(BmpIcon selectedIcon, string[] newalias)
        {
            BankJson.ChangeAlias(selectedIcon, newalias);
            SaveBankJson();
        }
    }
    
    public enum Form1Status
    {
        None,
        Selected,
        Editing,
    }

}
