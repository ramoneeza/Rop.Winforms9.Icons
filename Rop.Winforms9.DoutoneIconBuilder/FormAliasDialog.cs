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
    public partial class FormAliasDialog : Form
    {
        public string[] Alias => edalias.Lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        public FormAliasDialog(BmpIcon icon, string[] alias)
        {
            InitializeComponent();
            edicon.Text = icon.Code;
            edalias.Lines = alias;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
