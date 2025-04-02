namespace Rop.Winforms9.ColumnsListBox;

public class MouseCellOverArgs : EventArgs
{
    public Point Cell { get; }
    public int Index => Cell.Y;
    public int Column=> Cell.X;
    public object? Item { get; }
    public bool CanClick { get; set; }
    public string ToolTip { get; set; } = "";
    public bool ToolTipRight { get; set; }
    public MouseCellOverArgs(Point cell,object? item)
    {
        Cell = cell;
        Item = item;
    }
}