using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.ColumnsListBox;

public partial class ColumnListBox
{
    protected ToolTip ToolTip = new();
    private static readonly Point InvalidCell = new(-1, -1);
    private Point _lastMouseCell = InvalidCell;
    [Browsable(false)]
    public Point LastMouseCell=> _lastMouseCell;
    private void ListBox_MouseMove(object? sender, MouseEventArgs e)
    {
        var cell = GetItemCell(e.Location);
        if (cell!=_lastMouseCell)
        {
            _lastMouseCell = cell;
            ToolTip.Hide(ListBox);
            OnMouseCellOver();
        }
    }

    private void ListBox_MouseLeave(object? sender, EventArgs e)
    {
        _lastMouseCell= InvalidCell;
        ToolTip.Hide(ListBox);
        OnMouseCellOver();
    }
    public Point GetItemCell(Point p)
    {
        var index = ListBox.IndexFromPoint(p);
        if (index < 0) return InvalidCell;
        var x = p.X;
        var col = Header.HitTest(x);
        if (col == null) return InvalidCell;
        return new Point(col.ColumnIndex, index);
    }
    protected virtual void OnMouseCellOver()
    {
        if (_lastMouseCell==InvalidCell) 
            Cursor= Cursors.Default;
        else
        {
            var a = new MouseCellOverArgs(_lastMouseCell, Items[_lastMouseCell.Y]);
            MouseCellOver?.Invoke(this, a);
            Cursor=a.CanClick?Cursors.Hand : Cursors.Default;
            if (a.ToolTip != "")
            {
                var r = GetCellBounds(_lastMouseCell);
                if (a.ToolTipRight)
                    r.X += r.Width;
                else
                    r.Y += r.Height;
                ToolTip.Show(a.ToolTip,ListBox,r.X,r.Y);
            }
            else
                ToolTip.Hide(ListBox);
        }
    }
    private Rectangle GetCellBounds(Point cell)
    {
        if (cell == InvalidCell) return Rectangle.Empty;
        var itemBounds = ListBox.GetItemRectangle(cell.Y);
        var x = itemBounds.X;
        var cs = Header.HitTest(x);
        if (cs== null) return Rectangle.Empty;
        return new Rectangle(cs.Location.X, itemBounds.Y, cs.Width, itemBounds.Height);
    }
        
}