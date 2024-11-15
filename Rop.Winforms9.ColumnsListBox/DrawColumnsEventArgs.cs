using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.ListComboBox;
using System.Drawing;

namespace Rop.Winforms9.ColumnsListBox;

public class DrawColumnsEventArgs
{
    public DrawItemEventArgsEx DrawItemEventArgs { get; }
    public Rectangle Bounds { get; }
    public object? Item { get; }
    public int ColumnIndex { get; }
    public ColumnPanelColumn OrderIcon { get; }
    public CompatibleListBox ListBox { get; }
    public Graphics Graphics => DrawItemEventArgs.Graphics;
    public DrawColumnsEventArgs(CompatibleListBox that, DrawItemEventArgsEx drawItemEventArgs, Rectangle bounds, object? item,ColumnPanelColumn column)
    {
        DrawItemEventArgs = drawItemEventArgs;
        Bounds = bounds;
        Item = item;
        ColumnIndex = column.ColumnIndex;
        OrderIcon = column;
        ListBox = that;
    }

    public void DrawText(string txt, HorizontalAlignment alignment=HorizontalAlignment.Left,Font? font=null,Color? forecolor=null)
    {
        font ??= DrawItemEventArgs.Font ?? ListBox.Font;
        var c =forecolor?? DrawItemEventArgs.FinalForeColor;
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

    public void DrawLink(string txt, HorizontalAlignment alignment = HorizontalAlignment.Left, Color? forecolor = null)
    {
        if (forecolor == null)
        {
            forecolor = DrawItemEventArgs.State.HasFlag(DrawItemState.Selected)?Color.Red: Color.CornflowerBlue;
        }
        var f = new Font(DrawItemEventArgs.Font ?? ListBox.Font, FontStyle.Underline);
        DrawText(txt,alignment,f,forecolor);
    }
}