using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.ColumnsListBox;
internal partial class Dummy{}
public partial class ColumnPanel
{
    private BindingList<ColumnDefinition> _columnDefinitions=new();
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BindingList<ColumnDefinition> ColumnDefinitions
    {
        get => _columnDefinitions;
        set
        {
            if (value == _columnDefinitions) return;
            _columnDefinitions.ListChanged -= _columnDefinitions_ListChanged;
            _columnDefinitions = value;
            _columnDefinitions.ListChanged += _columnDefinitions_ListChanged;
            foreach (var columnDefinition in _columnDefinitions)
                columnDefinition.PropertyChanged += (_,_)=> _ajColumns();
            _ajColumns();
        }
    }
    private void _columnDefinitions_ListChanged(object? sender, ListChangedEventArgs e) => _ajColumns();
    private void _ajColumns()
    {
        while (_columnDefinitions.Count < _columns.Count)
        {
            _columns.RemoveAt(_columns.Count - 1);
        }
        for (var f = 0; f < _columnDefinitions.Count; f++)
        {
            if (f < _columns.Count)
            {
                _columns[f].SetColumnDefinition(_columnDefinitions[f]);
            }
            else
            {
                var c = _columnDefinitions[f];
                var oi = new ColumnPanelColumn(this,c,_columns.Count);
                _columns.Add(oi);
            }
        }
        _doLayout();
        if (_selectedcolumn >= _columns.Count)
        {
            if (_columns.Count>0)
                _setSelected(0,null);
            else
                _setSelected(-1,SortOrder.None);
        }
        Invalidate();
    }
    private int _selectedcolumn = -1;

    private void _setSelected(int selectedColumn, SortOrder? selectedOrder)
    {
        var ori = _columns.FirstOrDefault(c => c.SortOrder != SortOrder.None);
        if (selectedOrder == null)
        {
            if (ori == null || selectedColumn!=ori.ColumnIndex)
            {
                selectedOrder = SortOrder.Ascending;
            }
            else
            {
                selectedOrder = (ori.SortOrder == SortOrder.Ascending)
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
            }
        }
        if (selectedColumn== ori?.ColumnIndex && selectedOrder==ori?.SortOrder) return;
        _selectedcolumn= selectedColumn;
        SelectedOrder = selectedOrder.Value;
        foreach (var columnPanelColumn in _columns)
        {
            if (columnPanelColumn.ColumnIndex == selectedColumn)
            {
                columnPanelColumn.SortOrder = selectedOrder.Value;
            }
            else
            {
                columnPanelColumn.SortOrder = SortOrder.None;
            }
        }
        Invalidate();
        OrderChanged?.Invoke(this,new ColumnPanelOrderArgs(SelectedColumn,SelectedOrder));
    }
    private readonly List<ColumnPanelColumn> _columns = new();
    public int NumberOfColumns=>_columns.Count;
    public ColumnPanelColumn? GetColumn(int index)
    {
        if (index < 0 || index >= _columns.Count) return null;
        return _columns[index];
    }
    public ColumnPanelColumn[] Columns => _columns.ToArray();
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle ColumnBounds { get; private set; }

    public (int,string)[] ActiveFilter=> _columns.Where(c=>c.ActiveFilter!="").Select(c =>(c.ColumnIndex,c.ActiveFilter)).ToArray();

    private void _doLayout()
    {
        var ap = Point.Empty;
        var h=this.Height;
        var w = this.Width;
        if (BorderStyle!= BorderStyle.None)
        {
            ap.X += 1;
            ap.Y += 1;
            h -= 2;
            w -= 2;
        }
        ColumnBounds = new Rectangle(ap, new Size(w, h));
        var x = ap.X;
        var y = ap.Y;
        using var gr = this.CreateGraphics();
        foreach (var c in _columns)
        {

            c.Location = new Point(x, y);
            x += c.Width;
        }
        Invalidate();
    }
}