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