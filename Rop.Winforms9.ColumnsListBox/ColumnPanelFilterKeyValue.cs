using System.ComponentModel;
using Rop.Helper;

namespace Rop.Winforms9.ColumnsListBox;

[DesignerCategory("Code")]
public class ColumnPanelFilterKeyValue : AbsColumnPanelFilterBox<IKeyValue>
{
    protected override int IndexOf(IKeyValue item)
    {
        for(var f=0;f<ListBox.Items.Count;f++)
        {
            if (ListBox.Items[f] is not IKeyValue keyValue) continue;
            if (keyValue.GetKey() == item.GetKey()) return f;
        }
        return -1;
    }

    public ColumnPanelFilterKeyValue(Control parent, Rectangle columnbounds, IEnumerable<IKeyValue> items,
        bool selectmultiple, IEnumerable<IKeyValue>? selecteditems = null) : base(parent, columnbounds, items,
        selectmultiple, selecteditems)
    {
        ListBox.FormattingEnabled= true;
        ListBox.Format += ListBox_Format;
    }

    private void ListBox_Format(object? sender, ListControlConvertEventArgs e)
    {
        if (e.ListItem is not IKeyValue keyValue) return;
        e.Value=keyValue.GetValue();
    }
}