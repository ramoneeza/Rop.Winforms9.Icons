namespace Rop.Winforms9.ColumnsListBox;

public class MouseOverArgs : EventArgs
{
    public int Index { get; }
    public int Column { get; }
    public object? Item { get; }
    public bool CanClick { get; set; }
    public string ToolTip { get; set; }
    public bool ToolTipRight { get; set; }
    public MouseOverArgs(int index,int column,object? item)
    {
        Index = index;
        Column = column;
        Item = item;
    }
}