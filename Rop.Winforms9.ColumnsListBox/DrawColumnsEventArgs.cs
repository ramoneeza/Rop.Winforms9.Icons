using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.ListComboBox;
using System.Drawing;
using Rop.Winforms9.GraphicsEx.Colors;

namespace Rop.Winforms9.ColumnsListBox;

public class DrawItemEventArgsEx
  {
    public Rectangle Bounds { get; set; }
    public Color BackColor { get; set; }= SystemColors.Window;
    public Color BackColorAlt { get; set; } = Color.Empty;
    public Color EmptyBackColor { get; set; }=Color.Empty;
    public Color ForeColor { get; set; }=SystemColors.WindowText;
    public DrawItemState State { get; set; }
    public Graphics Graphics { get; set; }
    public Color SelectedBackColor { get; set; }=SystemColors.Highlight;
    public Color SelectedForeColor { get; set; } = SystemColors.HighlightText;
    public Font Font { get; set; }
    public int Index { get; set; }
    public bool Empty { get; set; }
    public bool IsAlt=>Index%2 == 1;
    public Color FinalForeColor => !State.HasFlag(DrawItemState.Selected) ? this.ForeColor : this.SelectedForeColor;
    public Color FinalNoSelBackColor=>IsAlt ? BackColorAlt.IfTOrEmpty(BackColor):BackColor;
    public Color FinalBackColor => !State.HasFlag(DrawItemState.Selected) ? FinalNoSelBackColor : this.SelectedBackColor;

    public bool HandledBackground { get; set; } = false;
    public bool Handled { get; set; } = false;
    
    public DrawItemEventArgsEx(Graphics graphics,Rectangle bounds, DrawItemState state, Font font, int index)
    {
        Bounds = bounds;
        State = state;
        Graphics = graphics;
        Font = font;
        Index = index;
    }

    public DrawItemEventArgsEx(DrawItemEventArgs arg,Color empty,Color backColor,Color backColorAlt,Color foreColor)
    {
        Bounds = arg.Bounds;
        State = arg.State;
        Graphics= arg.Graphics;
        Font = arg.Font??Control.DefaultFont;
        Index = arg.Index;
        EmptyBackColor = empty;
        BackColor = backColor;
        BackColorAlt = backColorAlt;
        ForeColor = foreColor;
    }
    public void DrawBackground()
    {
        DrawBackground(Bounds);
    }
    public void DrawBackground(Rectangle r)
    {
        var bg=Empty? EmptyBackColor.IfTOrEmpty(FinalBackColor) : FinalBackColor;
        Graphics.FillRectangle(new SolidBrush(bg), r);
    }
    
    /// <summary>Draws a focus rectangle within the bounds specified in the <see cref="DrawItemEventArgs.#ctor" /> constructor.</summary>
    public void DrawFocusRectangle()
    {
        if (this.State.HasFlag(DrawItemState.Focus) && !this.State.HasFlag(DrawItemState.NoFocusRect)) 
            ControlPaint.DrawFocusRectangle(this.Graphics, this.Bounds, this.ForeColor, this.FinalNoSelBackColor);
    }
  }



public class DrawColumnsEventArgs
{
    public DrawItemEventArgsEx DrawItemEventArgs { get; }
    public Rectangle Bounds { get; }
    public object? Item { get; }
    public int ColumnIndex { get; }
    public OrderIcon OrderIcon { get; }
    public CompatibleListBox ListBox { get; }
    public Graphics Graphics => DrawItemEventArgs.Graphics;
    public DrawColumnsEventArgs(CompatibleListBox that, DrawItemEventArgsEx drawItemEventArgs, Rectangle bounds, object? item, int i, OrderIcon orderIcon)
    {
        DrawItemEventArgs = drawItemEventArgs;
        Bounds = bounds;
        Item = item;
        ColumnIndex = i;
        OrderIcon = orderIcon;
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