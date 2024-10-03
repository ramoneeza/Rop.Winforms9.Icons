

using Rop.Winforms9.FontsEx;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using Rop.Winforms9.GraphicsEx;

namespace Rop.Winforms9.DuotoneIcons;

public static class DuoToneIconHelper
{
    public static float DrawIcon(this Graphics gr,DuoToneIcon icon, DuoToneColor iconcolor, RectangleF rect)
    {
        using var bmp =icon.GetBitmap(iconcolor);
        gr.DrawSoftImage(bmp,rect);
        return rect.Width;
    }
    public static float DrawIcon(this Graphics gr,DuoToneIcon icon, DuoToneColor iconcolor, float x, float y, float height)
    {
        var m = icon.MeasureIcon(height);
        gr.DrawIcon(icon, iconcolor, new RectangleF(x, y, m.Width, m.Height));
        return m.Width;
    }
    public static float DrawIcon(this Graphics gr,DuoToneIcon icon, DuoToneColor iconcolor, float x, float y, FontSizeF iconsize)
    {
        gr.DrawIcon(icon, iconcolor, new FontRectangleF(new PointF(x, y), iconsize));
        return iconsize.Width;
    }
    public static float DrawIcon(this Graphics gr,DuoToneIcon icon,DuoToneColor iconcolor, FontRectangleF iconsize)
    {
        gr.DrawIcon(icon, iconcolor, new RectangleF(iconsize.X, iconsize.Y, iconsize.Width, iconsize.Height));
        return iconsize.Width;
    }
    public static float DrawIconBaseline(this Graphics gr, DuoToneIcon icon,DuoToneColor iconcolor, float x, float baseline, float height)
    {
        var m = icon.MeasureIcon(height);
        var y = baseline - m.Ascent;
        gr.DrawIcon(icon, iconcolor, new RectangleF(x, y, m.Width, m.Height));
        return m.Width;
    }
    public static float DrawIconBaseline(this Graphics gr,DuoToneIcon icon,DuoToneColor iconcolor, float x, float baseline, FontSizeF iconsize)
    {
        var y = baseline - iconsize.Ascent;
        gr.DrawIcon(icon, iconcolor, new RectangleF(x, y, iconsize.Width, iconsize.Height));
        return iconsize.Width;
    }
    public static float DrawIconBaseline(this Graphics gr,DuoToneIcon icon, DuoToneColor iconcolor, float x, float baseline, IconSizeF iconSize)
    {
        return gr.DrawIconBaseline(icon, iconcolor, x, baseline, iconSize.Size);
    }
    public static float DrawIconFit(this Graphics gr,DuoToneIcon icon,  DuoToneColor iconcolor, float x, float y, float size)
    {
        FontSizeF m;
        if (icon.WidthUnit > 1)
        {
            var height = size / icon.WidthUnit;
            m = icon.FontSizeUnitExtended * height;
        }
        else
        {
            m =icon.MeasureIcon(size);
        }
        y = y + (size - m.Ascent) / 2;
        gr.DrawIcon(icon, iconcolor, new RectangleF(x, y, m.Width, m.Height));
        return m.Width;
    }
    
    
    public static void DrawStringWithIcons(this Graphics gr, PointF offset, IMeasuredIcon measured, bool test = false)
    {
        if (!measured.Args.HasText)
        {
            _DrawSoloIcon(gr, offset, measured, test);
        }
        else
        {
            _DrawStringWithIcons(gr, offset, measured, test);
        }
    }
    private static void _DrawStringWithIcons(Graphics gr, PointF offset, IMeasuredIcon measured, bool test = false)
    {
        //test = true;
        var args = measured.Args;
        var maxh = measured.Bounds.Height;
        var oldtr = gr.TextRenderingHint;
        gr.TextRenderingHint = args.TextRenderingHint;
        var br = new SolidBrush(args.ForeColor);
        var iconcolor = args.FinalIconColor;
        var bounds = measured.OffsetBounds(offset.X, offset.Y);
        var iconbounds = measured.OffsetBoundsIcon(offset.X, offset.Y);
        var textbounds = measured.OffsetBoundsText(offset.X, offset.Y);
        if (measured.Icon is not null)
        {
            var r = iconbounds;
            if (test) gr.FillRectangle(new SolidBrush(Color.Chartreuse), r);
            gr.DrawIcon(measured.Icon,iconcolor, r);
        }
        if (measured.Text != "")
        {
            var r = textbounds;
            if (test) gr.FillRectangle(new SolidBrush(Color.BlueViolet), r);
            gr.DrawString(measured.Text, args.Font, br, r.X, r.Y, StringFormat.GenericTypographic);
        }
        gr.TextRenderingHint = oldtr;
    }

    private static void _DrawSoloIcon(Graphics gr, PointF offset, IMeasuredIcon measured, bool test = false)
    {
        if (measured.Icon==null) return;
        //test = true;
        var args = measured.Args;
        var oldtr = gr.TextRenderingHint;
        var bounds = measured.Bounds;
        var r = bounds.WithOffset(offset);
        if (test) gr.FillRectangle(new SolidBrush(Color.Chartreuse), r.ToRectangleF());
        gr.DrawIcon(measured.Icon,args.FinalIconColor, r.X, r.Y, r.Size);
    }
    public static PointF AlignOffset(this ContentAlignment alignment, RectangleF outerbounds, RectangleF textbounds)
    {
        var res = textbounds.Location;
        switch (alignment)
        {
            case ContentAlignment.TopCenter:
            case ContentAlignment.TopLeft:
            case ContentAlignment.TopRight:
                res = new PointF(textbounds.X, outerbounds.Top);
                break;
            case ContentAlignment.BottomCenter:
            case ContentAlignment.BottomLeft:
            case ContentAlignment.BottomRight:
                res = new PointF(textbounds.X, outerbounds.Bottom - textbounds.Height);
                break;
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.MiddleRight:
                res = new PointF(textbounds.X, outerbounds.Y + (outerbounds.Height - textbounds.Height) / 2);
                break;
        }
        switch (alignment)
        {
            case ContentAlignment.TopLeft:
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.BottomLeft:
                res = new PointF(outerbounds.Left, res.Y);
                break;
            case ContentAlignment.TopRight:
            case ContentAlignment.MiddleRight:
            case ContentAlignment.BottomRight:
                res = new PointF(outerbounds.Right - textbounds.Width, res.Y);
                break;
            case ContentAlignment.TopCenter:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.BottomCenter:
                res = new PointF(outerbounds.X + (outerbounds.Width - textbounds.Width) / 2, res.Y);
                break;
        }

        return res;
    }
    public static PointF AlignOffset(this Control c, ContentAlignment alignment, RectangleF textbounds,Padding? padding=null)
    {
        var p=padding ?? c.Padding;
        var controlbounds = new RectangleF(p.Left, p.Top, c.Width - p.Horizontal, c.Height - p.Vertical);
        return alignment.AlignOffset(controlbounds, textbounds);
    }
    public static PointF AlignOffset(this Control c, ContentAlignment alignment, FontRectangleF textbounds,Padding? padding=null)
    {
        return c.AlignOffset(alignment, textbounds.ToRectangleF(),padding);
    }
    
}