using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.ColumnsListBox;
internal partial class Dummy{}
public partial class ColumnPanel
{
    private ColumnPanelColumn? _resizingcolumn = null;

    protected ColumnPanelColumn? ResizingColumn
    {
        get=> _resizingcolumn;
        set{
            if (_resizingcolumn==value) return;
                _resizingcolumn = value;
                Cursor = _resizingcolumn != null ? Cursors.SizeWE : Cursors.Default;
                if (_resizingcolumn==null) ColumnWidthChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    protected override void OnMouseEnter(EventArgs e)
    {
        ResizingColumn= null;
        var p = PointToClient(MousePosition);
        OnMouseOver(p,MouseButtons);
    }
    protected override void OnMouseLeave(EventArgs e)
    {
        ResizingColumn = null;
        var p = PointToClient(MousePosition);
        OnMouseOver(p,MouseButtons);
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (ResizingColumn!= null)
        {
            _columnResize(e.Location);
        }
        else
            OnMouseOver(e.Location, e.Button);
    }
    private void _columnResize(Point location)
    {
        if (ResizingColumn==null) return;
        var x = location.X;
        var xc=ResizingColumn.Bounds.X;
        var w= x - xc;
        if (w < 10)
        {
            w = 10;
        }
        ResizingColumn.Width = w;
        _doLayout();
        Refresh();
        ColumnWidthChanging?.Invoke(this, EventArgs.Empty);
    }
    protected ToolTip ToolTip { get; } = new ToolTip();
    private void OnMouseOver(Point loc, MouseButtons button)
    {
        var (hit,col) = HitTest(loc);
        if (hit == ColumnHit.None || col == null)
        {
            ToolTip.Hide(this);
            return;
        }
        switch (hit)
        {
            case ColumnHit.Sort:
                if (col.ToolTipText=="") ToolTip.Show(col.ToolTipText, this, loc);
                Cursor = col.Selectable ? SelectableCursor : Cursors.Default;
                break;
            case ColumnHit.Filter:
                Cursor = col.Filterable ? SelectableCursor : Cursors.Default;
                break;
            case ColumnHit.Resize:
                Cursor = col.Resizable ? Cursors.SizeWE : Cursors.Default;
                break;
        }
    }

    public ColumnPanelColumn? HitTest(int x)
    {
        return _columns.FirstOrDefault(c=>c.HitTest(x));
    }
    
    private (ColumnHit,ColumnPanelColumn?) HitTest(Point loc)
    {
        var resizingflag = ResizingColumn != null;
        foreach (var columnPanelColumn in _columns)
        {
            var h=columnPanelColumn.HitTest(loc,resizingflag);
            if (h!= ColumnHit.None) 
                return (h, columnPanelColumn);
        }
        return (ColumnHit.None, null);
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (ResizingColumn!=null)
        {
            _columnResize(e.Location);
            ResizingColumn = null;
        }
        base.OnMouseUp(e);
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        var (hit,col) = HitTest(e.Location);
        if (hit == ColumnHit.None || col == null) return;
        switch (hit)
        {
            case ColumnHit.Sort:
                if (col.Selectable && e.Button==MouseButtons.Left)
                {
                  _setSelected(col.ColumnIndex,null);
                }
                break;
            case ColumnHit.Filter:
                if (col.Filterable && e.Button==MouseButtons.Left)
                {
                    var args=new ColumnPanelFilterArgs(col);
                    ColumnFilterClick?.Invoke(this, args);
                    col.ActiveFilter = args.ActiveFilter.ToHashSet();
                }
                break;
            case ColumnHit.Resize:
                if (col.Resizable) ResizingColumn = col;
                break;
        }
    }
}