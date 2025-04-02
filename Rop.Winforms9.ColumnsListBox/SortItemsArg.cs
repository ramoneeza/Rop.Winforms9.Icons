using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.ColumnsListBox;

public class SortItemsArg:EventArgs{
    public int SelectedColumn { get; }
    public SortOrder SelectedOrder { get; }
    public (int,IReadOnlySet<string>)[] ActiveFilter { get;}
    public List<object> Items { get; set; }

    public SortItemsArg(int selectedColumn, SortOrder selectedOrder,(int,IReadOnlySet<string>)[] activefilter, List<object> items)
    {
        SelectedColumn = selectedColumn;
        SelectedOrder = selectedOrder;
        ActiveFilter= activefilter;
        Items = items;
    }
}