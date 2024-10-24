using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.ColumnsListBox;

public class DrawColumnsEventArgs
{
    public DrawItemEventArgs DrawItemEventArgs { get; }
    public Rectangle Bounds { get; }
    public object? Item { get; }
    public int ColumnIndex { get; }
    public OrderIcon OrderIcon { get; }
    public CompatibleListBox ListBox { get; }
    public Graphics Graphics => DrawItemEventArgs.Graphics;
    public DrawColumnsEventArgs(CompatibleListBox that, DrawItemEventArgs drawItemEventArgs, Rectangle bounds, object? item, int i, OrderIcon orderIcon)
    {
        DrawItemEventArgs= drawItemEventArgs;
        Bounds = bounds;
        Item = item;
        ColumnIndex = i;
        OrderIcon = orderIcon;
        ListBox = that;
    }

    public void DrawText(string txt, HorizontalAlignment alignment=HorizontalAlignment.Left)
    {
        var font = DrawItemEventArgs.Font ?? ListBox.Font;
        var c = DrawItemEventArgs.ForeColor;
        var g = Graphics;
        StringAlignment sa = alignment switch
        {
            HorizontalAlignment.Left => StringAlignment.Near,
            HorizontalAlignment.Center => StringAlignment.Center,
            HorizontalAlignment.Right => StringAlignment.Far,
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };
        g.DrawString(txt, font, new SolidBrush(c), Bounds, new StringFormat
        {
            Alignment = sa,
            LineAlignment = StringAlignment.Center
        });
    }
}