using System.Drawing.Imaging;
using Rop.Winforms8.DuotoneIcons;

namespace Rop.Winforms8._1.DoutoneIconBuilder;

public class BmpIcon
{
    public string Filename { get; private init; }
    public string Code { get; private init; }
    public Size Size { get; private init; }
    public int BaseLine { get; set; }
    public bool IsNew { get; set; }
    public bool IsExtra { get; private init; }
    private byte[] _data;
    public ReadOnlySpan<byte> Data
    {
        get => _data;
        set
        {
            _data = value.ToArray();
            Bitmap = Converter.ToBmp4(_data, Size);
        }
    }
    public byte GetPixel(int x,int y)
    {
        var i = y * Size.Width / 2 + x / 2;
        var b=_data[i];
        if (x % 2 != 0)
            return (byte)(b & 0x0F);
        else
            return (byte)(b >> 4);
    }

    public Bitmap Bitmap { get; private set; }

    private BmpIcon(string filename, int? baseline, bool isnew)
    {
        Filename = filename;
        var path = Path.GetDirectoryName(filename) ?? "";
        IsExtra = path.EndsWith(BankJson.ExtraFolder);
        Code = Path.GetFileNameWithoutExtension(filename);
        using var bmp = new Bitmap(filename);
        Size = bmp.Size;
        IsNew = isnew;
        BaseLine = baseline ?? bmp.Height;
        Bitmap = default!;
        _data = default!;
        Data = Converter.From32BTo4B(bmp);
    }

    public static BmpIcon? Create(string filename,bool isnew)
    {
        if (!File.Exists(filename)) return null;
        return new BmpIcon(filename, null,isnew);
    }

    public static BmpIcon? Create(string basepath, BmpIconDto dto)
    {
        var subpath = dto.IsExtra ? BankJson.ExtraFolder : BankJson.BankFolder;
        var path = Path.Combine(basepath, subpath, dto.Code + ".png");
        if (!File.Exists(path)) return null;
        return new BmpIcon(path, dto.BaseLine, dto.IsNew);
    }

    public void SaveBitmap()
    {
        Bitmap.Save(Filename, ImageFormat.Png);
    }

    public void SetPixel(int x, int y, int c)
    {
        var i = y * Size.Width / 2 + x / 2;
        var r = (x % 2)==0;
        var m=r? 0x0F : 0xF0;
        var p=r ? c << 4 : c;
        var b=_data[i];
        b = (byte)((b & m) | p);
        _data[i] = b;
        Bitmap = Converter.ToBmp4(_data, Size);
    }
    private BmpIcon()
    {
        Filename = "";
        Code = "";
        Size = new Size(0, 0);
        BaseLine = 0;
        IsNew = false;
        IsExtra = false;
        _data = Array.Empty<byte>();
        Bitmap = new Bitmap(1, 1);
    }
    public BmpIcon Clone()
    {
        return new BmpIcon()
        {
            Filename = Filename,
            Code = Code,
            Size = Size,
            BaseLine = BaseLine,
            IsNew = IsNew,
            IsExtra = IsExtra,
            Data = Data.ToArray()
        };
    }
}
