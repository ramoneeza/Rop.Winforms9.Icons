using System.ComponentModel;
using System.Diagnostics;
using Rop.Winforms9.GraphicsEx;
using Rop.Winforms9.GraphicsEx.Geom;

namespace Rop.Winforms9.DropControls;

internal partial class Dummy
{ }

public partial class DropImage : Control
{
    public event EventHandler? ValueChanged;

    private Size _allowedsize;
    private BorderStyle _borderStyle;
    private bool _showsize;
    private Bitmap? _value;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap? Value
    {
        get => _value;
        set
        {
            _value = value;
            _status = value == null ?
                (_originalValue == null ? DropControlStatus.Empty : DropControlStatus.Clear)
                 : DropControlStatus.New;
            Invalidate();
            OnValueChanged();
        }
    }
    private Bitmap? _originalValue;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap? OriginalValue
    {
        get => _originalValue;
        set
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            _originalValue = value;
            _value = value;
            _status = value == null ? DropControlStatus.Empty : DropControlStatus.Original;
            Invalidate();
        }
    }

    public DropImage()
    {
        base.AllowDrop = true;
        // ReSharper disable once VirtualMemberCallInConstructor
        AllowedExtensions = ["png", "jpg", "bmp", "tif", "gif", "jpeg", "tiff"];
        BorderStyle = BorderStyle.FixedSingle;
        _value = null;
        _originalValue = null;
        AllowedSize = new Size(60, 60);
    }

    [DefaultValue(typeof(Size), "60;60")]
    public Size AllowedSize
    {
        get => _allowedsize;
        set => _setReSize(ref _allowedsize, value);
    }
    private int _iconSize = 20;
    [DefaultValue(20)]
    public int IconSize
    {
        get => _iconSize;
        set => this._setReSize(ref _iconSize, value);
    }
    [DefaultValue(false)]
    public bool AllowReSize { get; set; }
    [DefaultValue(false)]
    public bool AllowPaste { get => _iconMenu[(int)MenuIcons.Paste].Enabled; set => _seticonmenu(MenuIcons.Paste, value); }
    [DefaultValue(false)]
    public bool AllowCopy { get => _iconMenu[(int)MenuIcons.Copy].Enabled; set => _seticonmenu(MenuIcons.Copy, value); }

    [DefaultValue(false)]
    public bool AllowDelete { get => _iconMenu[(int)MenuIcons.Delete].Enabled; set => _seticonmenu(MenuIcons.Delete, value); }
    [DefaultValue(false)]
    public bool AllowOpen { get => _iconMenu[(int)MenuIcons.Open].Enabled; set => _seticonmenu(MenuIcons.Open, value); }
    private bool _showcopypaste;
    [DefaultValue(false)]
    public bool ShowCopyPaste
    {
        get => _showcopypaste;
        set
        {
            if (_showcopypaste != value)
            {
                _showcopypaste = value;
                _ajSize();
            }
        }
    }
    [DefaultValue(false)]
    public bool ShowSize
    {
        get => _showsize;
        set
        {
            if (_showsize != value)
            {
                _showsize = value;
                _ajSize();
            }
        }
    }
    [DefaultValue(BorderStyle.None)]
    public BorderStyle BorderStyle
    {
        get => _borderStyle;

        set
        {
            if (_borderStyle != value)
            {
                if (!Enum.IsDefined(typeof(BorderStyle), value))
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(BorderStyle));

                _borderStyle = value;
                Invalidate();
            }
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        if (Size.Height != _desiredSize().Height)
            Size = new Size(Size.Width, _desiredSize().Height);
        base.OnSizeChanged(e);
    }
    protected Bitmap? AjustaImagen(Bitmap imagen)
    {
        var sizemenos = imagen.Size.Height <= AllowedSize.Height &&
                        imagen.Size.Width <= AllowedSize.Width;
        if (imagen.Size != AllowedSize && !sizemenos && !AllowReSize) return null;
        using (var formimagen = new DropImageForm())
        {
            formimagen.DesiredSize = AllowedSize;
            formimagen.OriginalBitmap = (Bitmap)imagen;
            if (formimagen.ShowDialog() == DialogResult.OK)
                return formimagen.FotoFinal;
            return null;
        }
        return imagen;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (ShowSize)
        {
            var szrect = new Rectangle(0, AllowedSize.Height, AllowedSize.Width, IconSize);
            var bg = new SolidBrush(Color.Gray);
            e.Graphics.FillRectangle(bg, szrect);
            var br = new SolidBrush(Color.White);
            var f = new Font("Verdana", 10, GraphicsUnit.Pixel);
            var cad = $"{AllowedSize.Width}x{AllowedSize.Height}";
            e.Graphics.DrawCenterMiddleString(cad, f, br, szrect);
        }
        if (ShowCopyPaste)
        {
            var szrect = _iconZone;
            var bg = new SolidBrush(Color.Gray);
            e.Graphics.FillRectangle(bg, szrect);
            foreach (var menuIcon in _iconMenu)
            {
                if (menuIcon.Enabled) _putIcon(e.Graphics, menuIcon);
            }
            // ReSharper disable once UnusedVariable
            var br = new SolidBrush(Color.White);

            // var f = new Font("Verdana", 14, GraphicsUnit.Pixel);
            // var cad = string.Format("{0}x{1}", AllowedSize.Width, AllowedSize.Height);
            // e.Graphics.DrawCenterMiddleString(cad, f, br, szrect);
        }

        var rect = new Rectangle(new Point(0, 0), AllowedSize);
        var img = Value;
        if (img == null)
        {
            using (var p = new Pen(Color.Black))
            {
                e.Graphics.DrawLine(p, 0, 0, AllowedSize.Width, AllowedSize.Height);
                e.Graphics.DrawLine(p, AllowedSize.Width, 0, 0, AllowedSize.Height);
                e.Graphics.DrawRectangle(new Pen(Color.Black, 1), rect.DeltaSize(-1, -1));
            }
            return;
        }

        try
        {
            e.Graphics.DrawImage(img, rect);
        }
        catch (Exception ex)
        {
            Debug.Print("Imagen no valida: " + ex.Message);
        }
        if (_value != _originalValue) e.Graphics.DrawRectangle(new Pen(Color.Yellow, 1), rect.DeltaSize(-1, -1));
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool AllowAnySize { get; set; }
    
    public virtual void PutFile(string file, byte[]? ms = null, bool iserror = false)
    {
        if (iserror)
        {
            Value = null;
            _status = DropControlStatus.Error;
            Invalidate();
            return;
        }
        try
        {
            if (ms == null)
            {
                ms = File.ReadAllBytes(file);
            }
            var bitmap = new Bitmap(new MemoryStream(ms));
            if (AllowedSize.Width <= 8) AllowedSize = bitmap.Size;
            if (!AllowAnySize && bitmap.Size != AllowedSize)
            {
                if (!AllowReSize) return;
                var img = AjustaImagen(bitmap);
                if (img == null) return;
                bitmap = img;
            }
            Value = bitmap;
        }
        catch (Exception ex)
        {
            Debug.Print("Error al cargar imagen: " + ex.Message);
        }
    }
    protected virtual byte[]? CanConvertFromSvg(byte[] data)
    {
        var sz = AllowedSize.Height;
        var bmp = SvgHelper.SvgToPng(data, sz);
        return bmp;
    }
}