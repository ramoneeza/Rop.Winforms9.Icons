using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.ColumnsListBox
{
    public partial class ColumnListBox
    {
        public event EventHandler SelectedIndexChanged;
        public event EventHandler<ItemClickEventArgs> ItemClick;
        public event EventHandler<ItemColumnClickEventArgs> ItemColumnsClick;

        protected CompatibleListBox ListBox { get; }
        public CompatibleItems Items=> ListBox.Items;
        
        protected virtual void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0 || e.Index >= Items.Count)
            {
                return;
            }
            var item = Items[e.Index];
            var rect = e.Bounds;
            var x = e.Bounds.X;
            var cs = Header.Columns;
            for (var f = 0; f < cs.Length; f++)
            {
                var c = cs[f];
                var r = new Rectangle(x, e.Bounds.Y, c.Width, e.Bounds.Height);
                var arg = new DrawColumnsEventArgs(ListBox,e,r,item,f, c);
                OnDrawColumns(arg);
                x += c.Width;
            }
            e.DrawFocusRectangle();
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
                var x0=o.Location.X;
                var x1 = x0 + o.Width;
                return x >= x0 && x < x1;
            });
            if (oi==null) return;
            // get the bounds of the column
            var bounds = new Rectangle(x - oi.Location.X, itemClickEventArgs.ItemBounds.Y, oi.Width,itemClickEventArgs.ItemBounds.Height);
            var args = new ItemColumnClickEventArgs(itemClickEventArgs.Index, itemClickEventArgs.Item,itemClickEventArgs.ItemBounds,bounds, itemClickEventArgs.MouseButtons, itemClickEventArgs.Location, oi.ColumnIndex, oi);
            ItemColumnsClick?.Invoke(this, args);
        }
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
    public OrderIcon OrderIcon { get; }

    public ItemColumnClickEventArgs(int index, object? item,Rectangle itemBounds,Rectangle columnBounds, MouseButtons mouseButtons, Point location, int columnIndex, OrderIcon orderIcon)
    {
        Index = index;
        Item = item;
        MouseButtons = mouseButtons;
        Location = location;
        ColumnIndex = columnIndex;
        OrderIcon = orderIcon;
        ItemBounds = itemBounds;
        ColumnBounds = columnBounds;
    }
}