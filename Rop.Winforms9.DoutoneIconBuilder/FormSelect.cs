using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    public partial class FormSelect : Form
    {
        public FormSelect()
        {
            InitializeComponent();
            AjListado();
            if (Program.Configuration.Banks.Count > 0)
            {
                var bank = Program.Configuration.Banks[0];
                folderBrowserDialog1.SelectedPath= bank.Path;
            }
            listado.DoubleClick += Listado_DoubleClick;
        }

        private void Listado_DoubleClick(object? sender, EventArgs e)
        {
            var item=listado.SelectedItem as BankPath;
            if (item == null) return;
            if (!item.IsValid())
            {
                MessageBox.Show("Invalid bank path");
                Program.Configuration.Banks.Remove(item);
                Program.GuardarConfiguracion();
                AjListado();
                return;
            }
            
            ExecuteBank(item);
        }

        private void AjListado()
        {
            listado.BeginUpdate();
            listado.Items.Clear();
            foreach (var item in Program.Configuration.Banks)
            {
                listado.Items.Add(item);
            }
            listado.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                var r = folderBrowserDialog1.ShowDialog();
                if (r != DialogResult.OK) return;
                var currentpath = folderBrowserDialog1.SelectedPath;
                if (Program.Configuration.Banks.Any(b => b.Path == currentpath))
                {
                    MessageBox.Show("This path is already in the list.");
                    return;
                }
                var png = Directory.EnumerateFiles(currentpath, "*.png").FirstOrDefault()??"";
                if (png=="")
                {
                    MessageBox.Show("This path is not a valid bank. No icons found");
                    return;
                }

                var bankjson = Path.Combine(currentpath, BankJson.Jsonfilename);
                BankPath? bank = null;
                if (!File.Exists(bankjson))
                {
                    var r2 = MessageBox.Show(
                        "This path is not a valid bank. Json Not Found. ¿Do you want to create a new one?",
                        "Create Bank",
                        MessageBoxButtons.YesNo);
                    if (r2 != DialogResult.Yes) return;
                    bank = await CreateBank(currentpath,png);
                }
                else
                {
                    bank =await LoadBank(currentpath);
                }

                if (bank == null)
                {
                    MessageBox.Show("Error creating bank");
                    return;
                }

                Program.Configuration.Banks.Insert(0, bank);
                Program.GuardarConfiguracion();
                await ExecuteBank(bank);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private async Task ExecuteBank(BankPath bank)
        {
            var list = Program.Configuration.Banks.Where(b => b != bank).ToList();
            list.Insert(0, bank);
            Program.Configuration.Banks = list;
            Program.GuardarConfiguracion();
            AjListado();
            var p = new Progress<(int, int)>();
            p.ProgressChanged += (sender, args) =>
            {
                _setProgress(args.Item1, args.Item2);
            };
            var bankjson = await Task.Run(()=>BankJson.Load(bank.Path,p));
            if (bankjson == null) throw new Exception("Error loading bank");
            _clearbarra();
            using var f = new Form1(bankjson);
            f.ShowDialog();
        }

        private async Task<BankPath?> CreateBank(string currentpath,string png)
        {
            var sz=Size.Empty;
            if (png!="")
            {
                using var bmp = new Bitmap(png);
                sz = bmp.Size;
            }
            using var f=new InputDialog();
            f.BaseSize = sz;
            var r = f.ShowDialog();
            if (r != DialogResult.OK) return null;
            var p = new Progress<(int, int)>();
            p.ProgressChanged += (sender, args) =>
            {
                _setProgress(args.Item1, args.Item2);
            };
            try
            {
                var minijson = await Task.Run(()=>BankJson.CreateNew(f.BankName, f.Initials, currentpath,p));
                minijson.Save();
                return await LoadBank(currentpath);
            }
            finally
            {
                _setProgress(0, 0);
            }
        }

        private void _clearbarra() => _setProgress(0, 0);
        private void _setProgress(int n, int maximo)
        {
            if (maximo == 0)
            {
                barra.Visible = false;
                return;
            }
            barra.Visible = true;
            barra.Maximum = maximo;
            barra.Minimum = 0;
            barra.Value = n;
        }

        private async Task<BankPath?> LoadBank(string currentpath)
        {
            var p = new Progress<(int, int)>();
            p.ProgressChanged += (sender, args) =>
            {
                _setProgress(args.Item1, args.Item2);
            };
            var bj=await Task.Run(()=>BankJson.Load(currentpath,p));
            if (bj == null) return null;
            return new BankPath(bj.Name, currentpath);
        }
    }
}
