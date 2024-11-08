using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.ColumnsListBox;

public class SortItemsArg:EventArgs{
    public int SelectedColumn { get; }
    public SortOrder SelectedOrder { get; }
    public List<object> Items { get; set; }

    public SortItemsArg(int selectedColumn, SortOrder selectedOrder, CompatibleItems items)
    {
        SelectedColumn = selectedColumn;
        SelectedOrder = selectedOrder;
        Items = items.ToList<object>();
    }
}
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