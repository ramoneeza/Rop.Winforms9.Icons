using Rop.Winforms9.FontsEx;

namespace Rop.Winforms9.DuotoneIcons;

public interface IMeasuredIcon
{
    IconArgs Args { get; }
    FontRectangleF Bounds { get; }
    RectangleF IconBounds { get; }
    RectangleF TextBounds { get; }

    public DuoToneIcon? Icon => Args.Icon;
    public string Text => Args.Text;
    public bool HasText => Args.HasText;

    public RectangleF OffsetBounds(float x, float y)
    {
        var b = Bounds;
        return new RectangleF(b.X + x, b.Y + y, b.Width, b.Height);
    }
    public RectangleF OffsetBoundsAscent(float x, float ascent)
    {
        var b = Bounds;
        var dif = ascent - b.Y;
        return OffsetBounds(x, dif);
    }
    public RectangleF OffsetBoundsIcon(float x, float y)
    {
        var b = IconBounds;
        return new RectangleF(b.X + x, b.Y + y, b.Width, b.Height);
    }

    public RectangleF OffsetBoundsText(float x, float y)
    {
        var b = TextBounds;
        return new RectangleF(b.X + x, b.Y + y, b.Width, b.Height);
    }
}

public static class MeasuredIconString
{
    public static IMeasuredIcon Factory(Graphics gr, IconArgs args)
    {
        if (args.HasText)
            return _factoryText(gr, args);
        else
            return _factorySolo(gr, args);
    }
    private static IMeasuredIcon _factorySolo(Graphics gr, IconArgs args)
    {
        float scale = args.IconScale / 100f;
        var dpi = gr.DpiY;
        var ascent = args.Font.GetAscentPixels(dpi);
        var icon = args.Icon;
        if (icon == null) return new MeasuredSoloIconString(null, args);
        var r = icon.MeasureIconWithAscent(gr, args.Font, scale);
        return new MeasuredSoloIconString(r, args);
    }
    private static IMeasuredIcon _factoryText(Graphics gr, IconArgs args)
    {
        float scale = args.IconScale / 100f;
        var dpi = gr.DpiY;
        var icon = args.Icon;
        var text = args.Text;
        var ricon = icon?.MeasureIconWithAscent(gr, args.Font, scale) ?? FontSizeF.Empty;
        var rtext =gr.MeasureTextSizeWithAscent(args.Font, text);
        return new MeasuredTextIconString(ricon, rtext, args);
    }
    private record MeasuredSoloIconString : IMeasuredIcon
    {
        public FontRectangleF Bounds { get; }
        public IconArgs Args { get; }
        public RectangleF IconBounds => Bounds.ToRectangleF();
        public RectangleF TextBounds => RectangleF.Empty;
        public MeasuredSoloIconString(FontSizeF? bounds, IconArgs args)
        {
            Bounds = new FontRectangleF(PointF.Empty, bounds ?? FontSizeF.Empty);
            var a = Bounds.Ascent;
            var diff = args.MinAscent - a;
            if (diff > 0) Bounds = Bounds.WithOffset(0, diff);
            Bounds = Bounds.WithOffset(0, args.OffsetIcon);
            Args = args;
        }
    }
    private record MeasuredTextIconString : IMeasuredIcon
    {
        public RectangleF IconBounds { get; }
        public RectangleF TextBounds { get; }
        public FontRectangleF Bounds { get; }
        public IconArgs Args { get; set; }

        public MeasuredTextIconString(FontSizeF iconsBounds, FontSizeF textBounds, IconArgs args)
        {
            Args = args;
            var a = iconsBounds.Ascent;
            var b = textBounds.Ascent;
            var c = args.MinAscent;
            var ascent = new float[] { a, b, c }.Max();
            var yicon = args.OffsetIcon + ascent - iconsBounds.Ascent;
            var ytext = args.OffsetText + ascent - textBounds.Ascent;
            var issuffix = args.IsSuffix;
            var xicon = !issuffix ? args.IconMarginLeft : textBounds.Width + args.IconMarginLeft;
            var xtext = !issuffix ? iconsBounds.Width + args.IconMarginLeft + args.IconMarginRight : 0;
            IconBounds = new RectangleF(xicon, yicon, iconsBounds.Width, iconsBounds.Height);
            TextBounds = new RectangleF(xtext, ytext, textBounds.Width, textBounds.Height);
            var h = Math.Max(IconBounds.Bottom, TextBounds.Bottom);
            var w = Math.Max(IconBounds.Right, TextBounds.Right);
            if (issuffix) w += args.IconMarginRight;
            if (h < args.MinHeight) h = args.MinHeight;
            Bounds = new FontRectangleF(0, 0, w, h, ascent);
        }
    }
}