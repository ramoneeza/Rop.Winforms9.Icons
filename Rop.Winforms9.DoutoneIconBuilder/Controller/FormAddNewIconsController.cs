using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.Winforms8._1.DoutoneIconBuilder.Controller;
using Svg;

namespace Rop.Winforms9.DoutoneIconBuilder.Controller
{
    public class FormAddNewIconsController:BaseController<FormAddNewIcons>
    {
        public FormAddNewIconsController(FormAddNewIcons parentForm) : base(parentForm)
        {
        }

        protected override async ValueTask InitAsync()
        {
            ParentForm.listado1.DisplayMember=nameof(SvgFile.Name);
            ParentForm.listado2.DisplayMember = nameof(SvgFile.Name);

            ParentForm.Enabled = false;
            ParentForm.barra.Visible = true;
            var allfiles = await Task.Run(()=>Directory.EnumerateFiles(Path.Combine(ParentForm.BasePath,"Bank"), "*.png").Select(Path.GetFileNameWithoutExtension).OrderBy(f=>f).ToFrozenSet(StringComparer.OrdinalIgnoreCase));
            var allnewfiles=await Task.Run(()=>Directory.EnumerateFiles(ParentForm.Folder, "*.svg").ToList());
            var allnewfilesnames = new List<SvgFile>();
            foreach (var allnewfile in allnewfiles)
            {
                var file = Path.GetFileNameWithoutExtension(allnewfile);
                var ajfile= AjustarNombre(file);
                if (!allfiles.Contains(ajfile))
                {
                    allnewfilesnames.Add(new SvgFile(allnewfile, ajfile));
                }
            }
            ParentForm.listado1.BeginUpdate();
            ParentForm.listado1.Items.Clear();
            ParentForm.listado1.Items.AddRange(allnewfilesnames.OrderBy(f=>f.Name).ToArray<object>());
            ParentForm.listado1.EndUpdate();
            ParentForm.listado2.BeginUpdate();
            ParentForm.listado2.Items.Clear();
            ParentForm.listado2.EndUpdate();
            ParentForm.barra.Visible = false;
            ParentForm.Enabled = true;
            ParentForm.listado1.SelectedIndexChanged += Listado1_SelectedIndexChanged;
            ParentForm.pb.SizeMode = PictureBoxSizeMode.Zoom;

            ParentForm.btnadd.Click += Btnadd_Click;
            ParentForm.btndel.Click += Btnremove_Click;
            ParentForm.btnaddall.Click += Btnaddall_Click;
            ParentForm.btncancel.Click += BtnCancel_Click;
            ParentForm.btnok.Click += BtnOk_Click;
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
           ParentForm.DialogResult= DialogResult.Cancel;
        }

        private void Btnaddall_Click(object? sender, EventArgs e)
        {
            ParentForm.listado2.Items.AddRange(ParentForm.listado1.Items);
            ParentForm.listado1.Items.Clear();
        }

        private void Btnremove_Click(object? sender, EventArgs e)
        {
            var f=ParentForm.listado2.SelectedItem as SvgFile;
            if (f== null) return;
            ParentForm.listado1.Items.Add(f);
            ParentForm.listado2.Items.Remove(f);
        }

        private void Btnadd_Click(object? sender, EventArgs e)
        {
            var f=ParentForm.listado1.SelectedItem as SvgFile;
            if (f== null) return;
            ParentForm.listado2.Items.Add(f);
            ParentForm.listado1.Items.Remove(f);
        }

        private void Listado1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var i=ParentForm.listado1.SelectedIndex;
            if (i < 0)
            {
                ParentForm.pb.Image = null;
                return;
            }
            var file = ParentForm.listado1.Items[i] as SvgFile;
            if (file == null)
            {
                ParentForm.pb.Image = null;
                return;
            }
            ParentForm.pb.Image = file.GetImage();
        }

        private string AjustarNombre(string file)
        {
            var s = new List<Char>();
            var pre = '-';
            foreach (var c in file)
            {
                if (pre == '-')
                {
                    s.Add(Char.ToUpper(c));
                }
                else
                {
                    if (c!='-') s.Add(c);
                }

                pre = c;
            }
            return new string(s.ToArray());
        }
    }

    public record SvgFile(string Path, string Name)
    {
        private Bitmap? _image;
        public Bitmap? GetImage()
        {
            if (_image != null) return _image;
            _image = SvgHelper.LoadSvg(Path);
            return _image;
        }
    }
}
