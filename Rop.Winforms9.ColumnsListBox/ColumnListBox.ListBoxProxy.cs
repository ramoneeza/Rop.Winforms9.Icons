using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rop.Helper;
using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.GraphicsEx.Colors;
using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.ColumnsListBox;

public partial class ColumnListBox
{
    public event EventHandler SelectedIndexChanged;
    public event EventHandler<ItemClickEventArgs> ItemClick;
    public event EventHandler<ItemColumnClickEventArgs> ItemColumnsClick;
    public event EventHandler<DrawItemEventArgsEx> DrawItem;
    protected CompatibleListBox ListBox { get; }
    private List<object> _items;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<object> Items
    {
        get => _items;
        private set => _items = value.ToList();
    }
    public void SetItems(params IEnumerable<object> items)
    {
        _items = items.ToList();
        IntListOrOrderOrFilterChanged();
    }
    public void RefreshItems()
    {
        IntListOrOrderOrFilterChanged();
    }

    private Color _itemBackgroundColor=Color.White;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ItemBackgroundColor
    {
        get => _itemBackgroundColor;
        set
        {
            if (_itemBackgroundColor==value) return;
            _itemBackgroundColor = value;
            ListBox.Invalidate();
        }
    }

    private Color _itemBackgroundColorAlt=Color.Empty;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ItemBackgroundColorAlt
    {
        get => _itemBackgroundColorAlt;
        set
        {
            if (_itemBackgroundColorAlt==value) return;
            _itemBackgroundColorAlt = value;
            ListBox.Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem
    {
        get => ListBox.SelectedItem;
        set => ListBox.SelectedItem = value;
    }

    

    protected virtual void OnDrawItem(DrawItemEventArgs e)
    {
        var eex= new DrawItemEventArgsEx(e,BackColor,ItemBackgroundColor,ItemBackgroundColorAlt,ForeColor);
        eex.Empty = (e.Index < 0 || e.Index >= Items.Count);
        DrawItem?.Invoke(this, eex);
        if (eex.Handled) return;
        if (!eex.HandledBackground) eex.DrawBackground();
        if (eex.Empty) return;
        var item = Items[eex.Index];
        var rect = eex.Bounds;
        var x = eex.Bounds.X;
        var cs = Header.Columns;
        for (var f = 0; f < cs.Length; f++)
        {
            var c = cs[f];
            var r = new Rectangle(x, eex.Bounds.Y, c.Width, eex.Bounds.Height);
            eex.DrawBackground(r);
            var arg = new DrawColumnsEventArgs(ListBox,eex,r,item,c);
            OnDrawColumns(arg);
            x += c.Width;
        }
        eex.DrawFocusRectangle();
    }

    private void ListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        OnSelectedIndexChanged();
    }
    protected virtual void OnSelectedIndexChanged()
    {
        SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
    }

    private void ListBox_MouseClick(object? sender, MouseEventArgs e)
    {
        // obtain the index of the item clicked
        var index = ListBox.IndexFromPoint(e.Location);
        if (index >= 0 && index < Items.Count)
        {
            var item = Items[index];
            var boundsitem = ListBox.GetItemRectangle(index);
            var arg = new ItemClickEventArgs(index, item,boundsitem, e.Button,e.Location);
            OnItemClick(arg);
        }
    }

    protected virtual void OnItemClick(ItemClickEventArgs itemClickEventArgs)
    {
        ItemClick?.Invoke(this, itemClickEventArgs);
        if (itemClickEventArgs.Handled) return;
        var x=itemClickEventArgs.Location.X;
        var oi = Header.Columns.FirstOrDefault(o =>
        {
            var x0=o.Bounds.X;
            var x1 = x0 + o.Width;
            return x >= x0 && x < x1;
        });
        if (oi==null) return;
        // get the bounds of the column
        var bounds = new Rectangle(x - oi.Bounds.X, itemClickEventArgs.ItemBounds.Y, oi.Width,itemClickEventArgs.ItemBounds.Height);
        var args = new ItemColumnClickEventArgs(itemClickEventArgs.Index, itemClickEventArgs.Item,itemClickEventArgs.ItemBounds,bounds, itemClickEventArgs.MouseButtons, itemClickEventArgs.Location, oi);
        ItemColumnsClick?.Invoke(this, args);
    }
}

    
    

public class ItemClickEventArgs
{
    public int Index { get; }
    public object? Item { get; }
    public MouseButtons MouseButtons { get; }
    public Point Location { get; }
    public Rectangle ItemBounds { get; }
    public bool Handled { get; set; }
    public ItemClickEventArgs(int index, object? item,Rectangle itemBounds, MouseButtons buttons,Point location)
    {
        Index = index;
        Item = item;
        MouseButtons = buttons;
        Location = location;
        ItemBounds = itemBounds;
    }
}

public class ItemColumnClickEventArgs
{
    public int Index { get; }
    public object? Item { get; }
    public MouseButtons MouseButtons { get; }
    public Point Location { get; }
    public Rectangle ItemBounds { get; }
    public Rectangle ColumnBounds { get; }
    public int ColumnIndex { get; }
    public ColumnPanelColumn OrderIcon { get; }

    public ItemColumnClickEventArgs(int index, object? item, Rectangle itemBounds, Rectangle columnBounds,
        MouseButtons mouseButtons, Point location, ColumnPanelColumn orderIcon)
    {
        Index = index;
        Item = item;
        MouseButtons = mouseButtons;
        Location = location;
        ColumnIndex = orderIcon.ColumnIndex;
        OrderIcon = orderIcon;
        ItemBounds = itemBounds;
        ColumnBounds = columnBounds;
    }
}

