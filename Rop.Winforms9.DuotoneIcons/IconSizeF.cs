namespace Rop.Winforms9.DuotoneIcons;

public record IconSizeF
{
    public Font TextFont { get; }
    public int IconScale { get; }
    public FontSizeF Size { get; }
    public IconSizeF(Graphics gr, DuoToneIcon icon, Font textfont, int iconScale)
    {
        TextFont = textfont;
        IconScale = iconScale;
        float scale = iconScale / 100f;
        var dpi = gr.DpiY;
        var ascent = TextFont.GetAscentPixels(dpi);
        Size = icon.MeasureIconWithAscent(gr, textfont, scale);
    }
}