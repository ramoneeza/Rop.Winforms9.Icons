using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.Winforms9.GraphicsEx.Geom;
using Rop.Winforms9.Helper;

namespace Rop.Winforms9.DropControls;

public partial class DropImage
{
    private class MenuIcon
    {
        public bool Enabled { get; set; }
        public bool Over { get; set; }
        public Bitmap Icon { get; set; }
        public Rectangle Bounds { get; set; }
        public MenuIcon(bool enabled, Bitmap icon){
            Enabled = enabled;
            Icon = icon;
        }
    }

    private void _setReSize<T>(ref T old, T value)
    {
        if (ReferenceEquals(old, value)) return;
        if (old is not null && old.Equals(value)) return;
        old = value;
        _ajSize();
    }
   
    private void _seticonmenu(MenuIcons index, bool value)
    {
        if (_iconMenu[(int)index].Enabled==value) return;
        _iconMenu[(int)index].Enabled = value;
        Invalidate();
    }
    private Rectangle _iconZone;
    private enum MenuIcons
    {
        Copy,
        Paste,
        Delete,
        Open
    }

    private static Bitmap _getMenuIcon(MenuIcons icon)
    {
        return icon switch
        {
            MenuIcons.Copy => ArtClass.ClipboardCopyIcon,
            MenuIcons.Paste => ArtClass.ClipboardPastIcon,
            MenuIcons.Delete => ArtClass.DeleteIcon,
            MenuIcons.Open => ArtClass.OpenFolderIconWhite,
            _ => ArtClass.EmptyIcon
        };
    }
    private readonly IReadOnlyList<MenuIcon> _iconMenu = Enum.GetValues<MenuIcons>().Select(m => new MenuIcon(false, _getMenuIcon(m))).ToArray();
     private void _putIcon(Graphics gr, MenuIcon m)
    {
        Brush bg = new SolidBrush(m.Over ? Color.Blue : Color.Gray);
        var bitmap =m.Icon;
        var rect = m.Bounds;
        gr.FillRectangle(bg, rect);
        var ricon = new Rectangle(rect.Location,new Size(IconSize-2,IconSize-2)).Center(rect);
        gr.DrawImage(bitmap, ricon,new Rectangle(Point.Empty, bitmap.Size),GraphicsUnit.Pixel);
    }
    private void _ajIcons()
    {
        var p = PointToClient(MousePosition);
        if (ShowCopyPaste)
        {
            foreach (var m in _iconMenu )
            {
                var newst = m.Bounds.Contains(p) && m.Enabled;
                if (newst== m.Over) continue;
                m.Over = newst;
                Invalidate(m.Bounds);
            }
        }
    }
    public void DoPaste()
    {
        var img = ClipboardEx.GetImageEx();
        if (img != null)
        {
            using (var ms2 = new MemoryStream())
            {
                try
                {
                    img.Save(ms2, ImageFormat.Png);
                }
                catch
                {
                    var img2 = (Image)img.Clone();
                    img2.Save(ms2, ImageFormat.Png);
                }
                var raw = ms2.ToArray();
                PutFile("",raw);
            }
        }
    }
    public void DoDelete()
    {
        Value =null;
    }
    public void DoCopy()
    {
        if (Value==null) return;
        Clipboard.SetImage(Value);
    }
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _ajIcons();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        _ajIcons();
    }

    protected override void OnClick(EventArgs e)
    {
        var p = PointToClient(MousePosition);
        if (ShowCopyPaste && _iconZone.Contains(p))
        {
            foreach (var (menuIcon,index) in _iconMenu.Select((m,i)=>(m,i)))
            {
                if (!menuIcon.Enabled || !menuIcon.Over) continue;
                var mi = (MenuIcons)index;
                switch (mi)
                {
                    case MenuIcons.Copy:
                        DoCopy();
                        break;
                    case MenuIcons.Paste:
                        DoPaste();
                        break;
                    case MenuIcons.Delete:
                        DoDelete();
                        break;
                    case MenuIcons.Open:
                        DoOpen();
                        break;
                }
            }
        }
        else
        {
            base.OnClick(e);
        }
    }

    private void DoOpen()
    {
       var filter=ExtDescription.GetFilterES(AllowedExtensions);
            using var f = new OpenFileDialog()
            {
                Filter = filter
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                var content= File.ReadAllBytes(f.FileName);
                PutFile(f.FileName,content);
            }
    }
    public void ClearDrop()
    {
        Value = null;
    }
    private Size _desiredSize()
    {
        var h = AllowedSize.Height;
        if (ShowCopyPaste && h < IconSize * 4) h = IconSize * 4;
        return new Size(_allowedsize.Width + (ShowCopyPaste ? IconSize : 0), h + (ShowSize ? IconSize : 0));
    }

    private void _ajSize()
    {
        var h = AllowedSize.Height;
        if (ShowSize && h < IconSize * 4) h = IconSize * 4;
        Size = _desiredSize();
        var locicons = new Point(AllowedSize.Width, 0);
        _iconZone = new Rectangle(locicons, new Size(IconSize, h));
        var sz = new Size(IconSize, IconSize);
        _iconMenu[0].Bounds = new Rectangle(locicons, sz);
        _iconMenu[1].Bounds = new Rectangle(locicons.AddY(IconSize), sz);
        _iconMenu[2].Bounds = new Rectangle(locicons.AddY(2 * IconSize), sz);
        _iconMenu[3].Bounds = new Rectangle(locicons.AddY(3 * IconSize), sz);
        Invalidate();
    }
    
}