using System.ComponentModel;
using System.Drawing.Drawing2D;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.DuotoneIcons.GoogleMaterial;
using Rop.Winforms9.DuotoneIcons.MaterialDesign;
using Rop.Winforms9.DuoToneIcons.MaterialDesign;

namespace DuotoneBrowser
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("WinForms Security", "WFO1000", Justification = "No Error on forms")]
    public partial class Form1 : Form
    {
        public IBankIcon? CurrentBank { get; private set; }
        public Form1()
        {
            InitializeComponent();
            AjCbBank();
        }

        private void AjCbBank()
        {
            MaterialDesignIcons.Register();
            GoogleMaterialIcons.Register();

            cbBank.BeginUpdate();
            cbBank.Items.Clear();
            var banks = IconRepository.GetRegisteredIcons();
            cbBank.Items.AddRange(banks.ToArray<object>());
            cbBank.EndUpdate();
        }

        private int _dwgcolumnas;
        private int _dwgfilas;
        private int _dwgsize;
        private int _tablawidth = 420;
        private int _lastover = -1;
        private Bitmap? _dwg;

        private async void cbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                biglabel.Text = edicontext.Text;
                lst.BeginUpdate();
                lst.Items.Clear();
                ClearLabels();
                var i = cbBank.SelectedIndex;
                var bname = (i < 0) ? "" : cbBank.Items[i] as string;
                CurrentBank = (bname?.ToLower()) switch
                {
                    "googlematerial" => CurrentBank = new GoogleMaterialBank(),
                    //"fontawesone" => CurrentBank = new FontAwesoneBank(),
                    "materialdesign" => CurrentBank = new MaterialDesignBank(),
                    _ => null
                };
                _dwg = null;
                if (CurrentBank != null)
                {
                    var numcodes = CurrentBank.Bank.Count;
                    _dwgcolumnas = 16;
                    _dwgfilas = (int)Math.Ceiling((numcodes * 1.0) / _dwgcolumnas);
                    _dwgsize = 2 + (_tablawidth / _dwgcolumnas);
                    _dwg = new Bitmap(_dwgsize * _dwgcolumnas, _dwgsize * _dwgfilas);
                    var x = 0;
                    var y = 0;
                    var br = new DuoToneColor(Color.Black, Color.Gray);
                    using var gr = Graphics.FromImage(_dwg);
                    foreach (var codename in CurrentBank.Bank.Codes)
                    {
                        lst.Items.Add(codename);
                        //var bmp=CurrentBank.Bank.GetBaseBitmap(codename);
                        //bmp.Save("c:\\tmp\\baseicon.png",ImageFormat.Png);
                        CurrentBank.Bank.DrawIconFit(gr, codename, br, 1 + x * _dwgsize, 1 + y * _dwgsize, _dwgsize - 2);
                        x++;
                        if (x >= _dwgcolumnas)
                        {
                            x = 0;
                            y++;
                        }
                        await Task.Yield();
                    }

                    //biglabel.BankIcon = CurrentBank;
                    //soloIcon.BankIcon = CurrentBank;
                    //btnIcon.BankIcon = CurrentBank;
                    tabla.Size = _dwg.Size;
                    tabla.BackgroundImage = _dwg;
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw;
            }
            finally
            {
                lst.EndUpdate();
            }
        }

        private void ClearLabels()
        {
            SetIcon("");
        }

        private void SetIcon(string name)
        {
            if (name == "" || CurrentBank is null)
            {
                biglabel.IconCode = "";
                soloIcon.IconCode = "";
                btnIcon.IconCode = "";
                return;
            }
            biglabel.Icons = CurrentBank.Bank;
            soloIcon.Icons = CurrentBank.Bank;
            btnIcon.Icons = CurrentBank.Bank;

            biglabel.IconCode = name;
            soloIcon.IconCode = name;
            btnIcon.IconCode = name;
            namelabel.Text = name;
        }

        private string ToUniHex(string code)
        {
            var chs = code.ToCharArray();
            var hxs = chs.Select(c => $"\\U{(int)c:X4}");
            return string.Join("", hxs);
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            var i = lst.SelectedIndex;
            if (i < 0)
            {
                ClearLabels();
                return;
            }

            var codename = lst.Items[i] as string;
            SetIcon(codename ?? "");
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            var i = e.Index;
            if (i < 0) return;
            var codename = lst.Items[i] as string;
            if (codename is null) return;
            var fc = new DuoToneColor(Color.Black, Color.Gray);
            var br = new SolidBrush(e.ForeColor);
            if (CurrentBank is not null)
            {
                var icon = CurrentBank.Bank.GetIcon(codename);
                if (icon is not null)
                {
                    var bmp = icon.GetBitmap(fc);
                    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    e.Graphics.DrawImage(bmp, e.Bounds.X, e.Bounds.Y, 32, 32);
                }
            }
            e.Graphics.DrawString(codename, e.Font ?? lst.Font, br, 40, e.Bounds.Y + 4);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(namelabel.Text ?? "");
        }

        private Bitmap? _dwgover;

        private void tabla_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dwgover is null) return;
            using var gr = Graphics.FromImage(_dwgover);
            var p = e.Location;
            var plast = new Point(p.X / _dwgsize, p.Y / _dwgsize);
            _lastover = plast.X + plast.Y * _dwgcolumnas;
            var x = _dwgsize * plast.X;
            var y = _dwgsize * plast.Y;
            var c = new SolidBrush(Color.FromArgb(128, Color.Yellow));
            gr.Clear(Color.Transparent);
            gr.FillRectangle(c, x, y, _dwgsize, _dwgsize);
            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if (_dwg!=null) gr.DrawImage(_dwg, x, y, new Rectangle(x, y, _dwgsize, _dwgsize), GraphicsUnit.Pixel);
            tabla.Image = _dwgover;
        }

        private void tabla_MouseEnter(object sender, EventArgs e)
        {
            if (_dwg is null)
            {
                _dwgover = null;
                _lastover = -1;
                return;
            }
            _dwgover = new Bitmap(_dwg.Width, _dwg.Height);
        }

        private void tabla_MouseLeave(object sender, EventArgs e)
        {
            tabla.Image = null;
            _dwgover = null;
            _lastover = -1;
        }

        private void tabla_Click(object sender, EventArgs e)
        {
            if (_lastover == -1) return;
            lst.SelectedIndex = _lastover;
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            var txt = edbuscar.Text;
            if (string.IsNullOrEmpty(txt)) return;
            var i = lst.SelectedIndex + 1;
            var n = lst.Items.Count;
            for (var j = i; j < n; j++)
            {
                var codename = lst.Items[j] as string;
                if (codename is null) continue;
                if (codename.Contains(txt, StringComparison.OrdinalIgnoreCase))
                {
                    lst.SelectedIndex = j;
                    return;
                }
            }
        }

        private void edoffseticon_ValueChanged(object sender, EventArgs e)
        {
            biglabel.OffsetIcon = (int)edoffseticon.Value;
            btnIcon.OffsetIcon = biglabel.OffsetIcon;
        }

        private void edoffsettext_ValueChanged(object sender, EventArgs e)
        {
            biglabel.OffsetText = (int)edoffsettext.Value;
            btnIcon.OffsetText = biglabel.OffsetText;
        }

        private void edicontext_TextChanged(object sender, EventArgs e)
        {
            biglabel.Text = edicontext.Text;
        }

        private void lcb_Click(object sender, EventArgs e)
        {
            SetColor(lcb);
        }

        private void SetColor(Label label)
        {
            var r = colorDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                var c = colorDialog1.Color;
                label.BackColor = c;
                if (label == lcb)
                {
                    biglabel.BackColor = c;
                    soloIcon.BackColor = c;
                }

                if (label == lc1)
                {
                    biglabel.IconColor = biglabel.IconColor.WithColor1(c);
                    soloIcon.IconColor = biglabel.IconColor;
                    btnIcon.IconColor = biglabel.IconColor;
                }

                if (label == lc2)
                {
                    biglabel.IconColor = biglabel.IconColor.WithColor2(c);
                    soloIcon.IconColor = biglabel.IconColor;
                    btnIcon.IconColor = biglabel.IconColor;
                }
            }
        }

        private void lc1_Click(object sender, EventArgs e)
        {
            SetColor(lc1);
        }

        private void lc2_Click(object sender, EventArgs e)
        {
            SetColor(lc2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var icon = CurrentBank?.Bank.GetIcon(namelabel.Text);
            if (icon is null) return;
            Clipboard.SetImage(icon.GetBaseBitmap());
        }

        private void edminascent_ValueChanged(object sender, EventArgs e)
        {
            biglabel.MinAscent = (int)edminascent.Value;
            btnIcon.MinAscent = biglabel.MinAscent;
        }

        private void ediconmarginleft_ValueChanged(object sender, EventArgs e)
        {
            biglabel.IconMarginLeft = (int)ediconmarginleft.Value;
            btnIcon.IconMarginLeft = biglabel.IconMarginLeft;
        }

        private void ediconmarginright_ValueChanged(object sender, EventArgs e)
        {
            biglabel.IconMarginRight = (int)ediconmarginright.Value;
            btnIcon.IconMarginRight = biglabel.IconMarginRight;
        }

        private void edusesuffix_CheckedChanged(object sender, EventArgs e)
        {
            biglabel.IsSuffix = edusesuffix.Checked;
            btnIcon.IsSuffix = biglabel.IsSuffix;
        }

        private void edminheight_ValueChanged(object sender, EventArgs e)
        {
            biglabel.MinHeight = (int)edminheight.Value;
            btnIcon.MinHeight = biglabel.MinHeight;
        }

        private void edpaddingleft_ValueChanged(object sender, EventArgs e)
        {
            var p = biglabel.Padding;
            p.Left = (int)edpaddingleft.Value;
            biglabel.Padding = p;
            btnIcon.Padding = p;
        }

        private void edpaddingright_ValueChanged(object sender, EventArgs e)
        {
            var p= biglabel.Padding;
            p.Right = (int)edpaddingright.Value;
            biglabel.Padding = p;
            btnIcon.Padding = p;
        }
    }
}

