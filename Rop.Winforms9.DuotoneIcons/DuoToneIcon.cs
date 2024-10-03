using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.Intrinsics.X86;
using Rop.Winforms9.FontsEx;

namespace Rop.Winforms9.DuotoneIcons;

public class DuoToneIcon
{
    public string Name { get; }
    private readonly byte[] _data;
    public IReadOnlyCollection<byte> Data => _data;
    public Size Size { get; }
    public int BaseLine { get; }
    public FontSizeF FontSizeUnit => new FontSizeF(WidthUnit, 1, AscentUnit);
    public FontSizeF FontSizeUnitExtended => new FontSizeF(WidthUnit, 1, 1);
    public float WidthUnit { get; }
    public float AscentUnit { get; }
    public float DescentUnit => 1 - AscentUnit;
    private Bitmap? _bitmap;
    private readonly Lock _lockbasebitmap = new Lock();
    public Bitmap GetBaseBitmap()
    {
        lock (_lockbasebitmap)
        {
            return _bitmap ??= Converter.ToBmp4(_data, Size);
        }
    }
    public Bitmap GetBitmap(DuoToneColor color)
    {
        var bmp = GetBaseBitmap();
        var colorbitmap = (Bitmap)bmp.Clone();
        var p = colorbitmap.Palette;
        p.Entries[1] = color.Color1;
        p.Entries[2] = color.Color2;
        colorbitmap.Palette = p;
        return colorbitmap;
    }
    public FontSizeF MeasureIcon(float height)
    {
        return FontSizeUnit * height;
    }
    public FontRectangleF MeasureIconRect(PointF location, float height)
    {
        var m = MeasureIcon(height);
        return new FontRectangleF(location.X, location.Y, m.Width, m.Height, m.Ascent);
    }
    public FontRectangleF MeasureIconRectBaseline(PointF location, float height)
    {
        var m = MeasureIcon(height);
        return FontRectangleF.FromBaseLine(location, m);
    }
    public FontSizeF MeasureIconWithAscent(Graphics g, Font font, float scale)
    {
        var f = g.MeasureTextSizeWithAscent(font, "A");
        var m = MeasureIcon(f.Ascent * scale);
        return m;
    }

    
    public DuoToneIcon(string name, Size size, int baseline, byte[] data)
    {
        //Container = container;
        Name = name;
        _data = data;
        Size = size;
        BaseLine = baseline;
        WidthUnit = size.Width / (float)size.Height;
        AscentUnit = baseline / (float)size.Height;
        _bitmap = null;
    }
}