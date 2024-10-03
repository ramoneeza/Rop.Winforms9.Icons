using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rop.Winforms9.DoutoneIconBuilder.Controller;

namespace Rop.Winforms9.DoutoneIconBuilder
{
    public partial class FormAddNewIcons : Form
    {
        public FormAddNewIconsController Controller { get; }
        public FormAddNewIcons(string folder, string basePath)
        {
            Folder = folder;
            BasePath = basePath;
            InitializeComponent();
            Controller = new FormAddNewIconsController(this);
        }
        public string Folder { get; }
        public string BasePath { get; }
        public Size DesiredSize => new Size(96, 96);
    }
}
