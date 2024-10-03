using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    public partial class InputDialog : Form
    {
        public string BankName { get; set; } = "";
        public string Initials { 
            get=>edinitials.Text;
            private set=>edinitials.Text=value; }
        
        public Size BaseSize { get; set; }
        public InputDialog()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            edname.Text= BankName;
            edh.Text = BaseSize.Height.ToString();
            edw.Text = BaseSize.Width.ToString();
            _ajInitials();
            edname.TextChanged += Edname_TextChanged;
            edw.TextChanged += Edw_TextChanged;
            edh.TextChanged += Edh_TextChanged;
            edw.KeyPress += wh_KeyPress;
            edh.KeyPress += wh_KeyPress;
        }

        private void wh_KeyPress(object? sender, KeyPressEventArgs e)
        {
            var c = e.KeyChar;
            if (c<32) return;
            if (!char.IsDigit(c))
            {
                e.Handled = true;
                return;
            }
        }

        private void Edh_TextChanged(object? sender, EventArgs e)
        {
            if (int.TryParse(edh.Text, out var i))
            {
                BaseSize = new(BaseSize.Width, i);
            }
            else
            {
                edh.Text= BaseSize.Height.ToString();
            }
        }
        

        private void Edw_TextChanged(object? sender, EventArgs e)
        {
            if (int.TryParse(edw.Text, out var i))
            {
                BaseSize = new(i, BaseSize.Height);
            }
            else
            {
                edw.Text = BaseSize.Width.ToString();
            }
        }

        private void Edname_TextChanged(object? sender, EventArgs e)
        {
            BankName = edname.Text;
            _ajInitials();
        }

        private void _ajInitials()
        {
            var validname = BankName.All(char.IsLetterOrDigit);
            if (!validname)
            {
                edname.BackColor = Color.LightSalmon;
                Initials = "";
            }
            else
            {
                edname.BackColor = Color.White;
                Initials = new(BankName.Where(char.IsUpper).ToArray());
            }
            button2.Enabled = Initials.Length > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
