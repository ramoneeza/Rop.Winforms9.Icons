using System.Drawing.Text;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons;

public record IconArgs
{
    public Font Font { get; init; } = SystemFonts.DefaultFont;
    public int IconScale { get; init; } = 100;
    public int MinAscent { get; init; } = 0;
    public int MinHeight { get; init; } = 0;
    public float OffsetIcon { get; init; } = 0;
    public float OffsetText { get; init; } = 0;
    public float IconMarginLeft { get; init; } = 0;
    public float IconMarginRight { get; init; } = 0;
    public bool IsSuffix { get; init; }
    public DuoToneColor FinalIconColor { get; init; } = DuoToneColor.Default;
    public TextRenderingHint TextRenderingHint { get; init; } = TextRenderingHint.SystemDefault;
    public Color ForeColor { get; init; } = Color.Black;
    public DuoToneIcon? Icon { get; init; }
    public string Text { get; init; } = "";
    public bool HasText { get; init; }
    public static IconArgs Factory(IHasIcon ihasicon)
    {
        var control = ihasicon as Control;
        if (control == null) throw new ArgumentException("Control is null");
        var ihastext = control as IHasText;
        var finalcolor = ihasicon.GetIconColor();
        var forecolor = ihastext?.GetForeColor() ?? control.ForeColor;
        var icon = ihasicon.GetIcon();
        var text = ihastext?.GetText() ?? "";
        return new IconArgs()
        {
            Font = control.Font,
            IconScale = ihasicon.IconScale,
            OffsetIcon = ihasicon.OffsetIcon,
            FinalIconColor = finalcolor,
            ForeColor = forecolor,
            OffsetText = ihastext?.OffsetText ?? 0,
            IconMarginLeft = ihastext?.IconMarginLeft ?? 0,
            IconMarginRight = ihastext?.IconMarginRight ?? 0,
            TextRenderingHint = ihastext?.TextRenderingHint ?? TextRenderingHint.SystemDefault,
            IsSuffix = ihastext?.IsSuffix ?? false,
            HasText = ihastext != null,
            Icon = icon,
            Text = text,
            MinAscent = ihasicon.MinAscent,
            MinHeight = ihasicon.MinHeight
        };
    }
}