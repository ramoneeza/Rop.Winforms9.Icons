using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.Versioning;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.PartialControls;
using Rop.Winforms9.GraphicsEx;

namespace Rop.Winforms9.ColumnsListBox;

internal partial class Dummy{}
[SupportedOSPlatform("windows6.1")]
public partial class ColumnPanel:Control, IHasBank
{
    public event EventHandler<ColumnPanelOrderArgs>? OrderChanged;
    public event EventHandler? FilterChanged;
    public event EventHandler<ColumnPanelFilterArgs>? ColumnFilterClick;
    public event EventHandler? ColumnWidthChanging;
    public event EventHandler? ColumnWidthChanged;

    public ColumnPanel()
    {
        _icons = IconRepository.DefaultBank;
        _columnDefinitions.ListChanged += _columnDefinitions_ListChanged;
    }
    private void _setwlayout<T>(ref T field, T value)
    {
        if (object.Equals(field,value)) return;
        field = value;
        _doLayout();
        Invalidate();
    }
    private void _setwinv<T>(ref T field, T value)
    {
        if (object.Equals(field,value)) return;
        field = value;
        _doLayout();
        Invalidate();
    }
    private BorderStyle _borderStyle = System.Windows.Forms.BorderStyle.None;
    [DefaultValue(BorderStyle.None)]
    public BorderStyle BorderStyle
    {
        get => _borderStyle;
        set=> _setwlayout(ref _borderStyle, value);
    }

    private bool _borderRaised;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool BorderRaised
    {
        get => _borderRaised;
        set => _setwlayout(ref _borderRaised, value);
    }
    [Browsable(false)]
    public ColumnPanelColumn? Selected=> GetColumn(_selectedcolumn);
    [DefaultValue(-1)]
    public int SelectedColumn
    {
        get => _selectedcolumn;
        set => _setSelected(value,null);
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SortOrder SelectedOrder { get; private set; } = SortOrder.None;
    public bool Ascending => (SelectedOrder!=SortOrder.Descending);
    private Padding _columnsPadding = new Padding(3);
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Padding ColumnsPadding
    {
        get => _columnsPadding;
        set => _setwinv(ref _columnsPadding, value);
    }
    private bool _interiorborder = false;
    [DefaultValue(false)]
    public bool InteriorBorder
    {
        get => _interiorborder;
        set => _setwinv(ref _interiorborder, value);
    }
    private Cursor _selectableCursor = Cursors.Hand;
    [DefaultValue(typeof(Cursors), "Hand")]
    public Cursor SelectableCursor
    {
        get => _selectableCursor;
        set => _selectableCursor = value;
    }
    
    private DuoToneColor _iconColorSelected=Color.Black;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DuoToneColor IconColorSelected
    {
        get => _iconColorSelected;
        set => _setwinv(ref _iconColorSelected, value);
    }
    private DuoToneColor _iconColorUnSelected=Color.Gray;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DuoToneColor IconColorUnSelected
    {
        get => _iconColorUnSelected;
        set => _setwinv(ref _iconColorUnSelected, value);
    }
    private DuoToneColor _iconColorActiveFilter = Color.Tomato;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DuoToneColor IconColorActiveFilter
    {
        get => _iconColorActiveFilter;
        set => _setwinv(ref _iconColorActiveFilter, value);
    }
    private string _iconFilter= "_Filter";
    [DefaultValue("_Filter")]
    public string IconFilter
    {
        get => _iconFilter;
        set => _setwinv(ref _iconFilter, value);
    }
    private string _iconUnselected = "_ChevronDown";
    [DefaultValue("_ChevronDown")]
    public string IconUnselected
    {
        get => _iconUnselected;
        set => _setwinv(ref _iconUnselected, value);
    }
    private string _iconAscending = "_ChevronDown";
    [DefaultValue("_ChevronDown")]
    public string IconAscending
    {
        get => _iconAscending;
        set => _setwinv(ref _iconAscending, value);
    }
    private string _iconDescending = "_ChevronUp";
    [DefaultValue("_ChevronDown")]
    public string IconDescending
    {
        get => _iconDescending;
        set => _setwinv(ref _iconDescending, value);
    }
    private int _offsetText;
    [DefaultValue(0)]
    public int OffsetText
    {
        get => _offsetText;
        set => _setwinv(ref _offsetText, value);
    }
    private int _iconMargin;
    [DefaultValue(0)]
    public int IconMargin
    {
        get => _iconMargin;
        set => _setwinv(ref _iconMargin, value);
    }
    private TextRenderingHint _textRenderingHint;
    [DefaultValue(TextRenderingHint.SystemDefault)]
    public TextRenderingHint TextRenderingHint
    {
        get => _textRenderingHint;
        set => _setwinv(ref _textRenderingHint, value);
    }
    public void SetActiveFilter(int columns, params IEnumerable<string> filter)
    {
        if (columns < 0 || columns >= NumberOfColumns) return;
        _columns[columns].ActiveFilter = filter.ToHashSet();
        OnFilterChanged();
    }
    protected virtual void OnFilterChanged()
    {
        FilterChanged?.Invoke(this, EventArgs.Empty);
    }
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        _doLayout();
    }
    public void ColumnPaint(PaintEventArgs e,ColumnPanelColumn column)
    {
        var icon= _GetIcon(column);
        var filtericon = column.Filterable ? _getFilterIcon() : null;
        var (bounds, textbounds, iconbounds, filterbounds) = column.GetBounds(e.Graphics, Font,ColumnsPadding, icon, filtericon, this.IconScale, this.IconMargin);
        // e.Graphics.FillRectangle(new SolidBrush(Color.GreenYellow), textbounds.Bounds);
        //e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), iconbounds.Bounds);
        //e.Graphics.FillRectangle(new SolidBrush(Color.CornflowerBlue), filterbounds.Bounds);
        e.Graphics.DrawOnBaseline(column.Text, Font, new SolidBrush(ForeColor), textbounds.BaseLineLocation);
        if (icon != null)
        {
            e.Graphics.DrawIconBaseline(icon, GetIconColor(column), iconbounds.X, iconbounds.BaseLine, iconbounds.Height);
        }

        if (filtericon != null)
        {
            e.Graphics.DrawIconBaseline(filtericon, _getFilterIconColor(column), filterbounds.X,
                filterbounds.BaseLine, filterbounds.Height);
        }
    }

    private DuoToneColor _getFilterIconColor(ColumnPanelColumn column)
    {
        if (column.ActiveFilter.Count>0)
        {
            return IconColorActiveFilter;
        }
        else
        {
            return IconColorUnSelected;
        }
    }

    private DuoToneIcon? _getFilterIcon()
    {
        return Icons?.GetIcon(IconFilter);
    }

    private DuoToneIcon? _GetIcon(ColumnPanelColumn column) =>Icons?.GetIcon(_GetIconCode(column));
    private string _GetIconCode(ColumnPanelColumn column)
    {
        switch (column.SortOrder)
        {
            case SortOrder.Ascending:
                return IconAscending;
            case SortOrder.Descending:
                return IconDescending;
            default:
                return IconUnselected;
        }
    }
    private DuoToneColor GetIconColor(ColumnPanelColumn column)
    {
        switch (column.SortOrder)
        {
            case SortOrder.Ascending:
            case SortOrder.Descending:
                return IconColorSelected;
            default:
                return IconColorUnSelected;
        }
    }

    
}

public readonly record struct ColumnPanelOrder
{
    public int ColumnIndex { get; init; }
    public SortOrder SortOrder { get; init; }
    public bool Ascending=> SortOrder != SortOrder.Descending;
}

public class ColumnPanelOrderArgs : EventArgs
{
    public ColumnPanelOrder Order { get; }

    public ColumnPanelOrderArgs(ColumnPanelOrder order)
    {
        Order = order;
    }
    public ColumnPanelOrderArgs(int columnIndex, SortOrder sortOrder)
    {
        Order = new ColumnPanelOrder() { ColumnIndex = columnIndex, SortOrder = sortOrder };
    }
}
public class ColumnPanelFilterArgs: EventArgs
{
    public ColumnPanelColumn Column { get; }
    public string[] ActiveFilter { get; set; }
    public ColumnPanelFilterArgs(ColumnPanelColumn column)
    {
        Column = column;
        ActiveFilter = column.ActiveFilter.ToArray();
    }
}

public class ColumnFilterClickArgs{
    private ColumnPanelFilterArgs _columnPanelFilter{ get; }
    public ColumnPanelColumn Column=> _columnPanelFilter.Column;
    public int ColumnIndex => Column.ColumnIndex;
    public string[] ActiveFilter { get=>_columnPanelFilter.ActiveFilter; set=> _columnPanelFilter.ActiveFilter = value; }
    public IReadOnlyList<object> Items { get; }
    public ColumnFilterClickArgs(ColumnPanelFilterArgs args, IReadOnlyList<object> items)
    {
        _columnPanelFilter = args;
        Items = items;
    }
}