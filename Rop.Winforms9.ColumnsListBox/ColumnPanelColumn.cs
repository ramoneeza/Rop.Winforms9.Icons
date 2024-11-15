using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.FontsEx;
using Rop.Winforms9.GraphicsEx.Geom;

namespace Rop.Winforms9.ColumnsListBox;

public enum ColumnHit
{
    None,
    Sort,
    Filter,
    Resize
}

public record ColumnPanelColumn
{
    public ColumnPanel Parent { get; }
    public ContentAlignment TextAlign { get;private set; }
    private int _width;

    public int Width
    {
        get => _width;
        internal set
        {
            if (value< MinWidth) value = MinWidth;
            _width = value;
            AjBounds();
        }
    }

    public string Text { get; private set; }
    public bool Selectable { get; private set; }
    public string ToolTipText { get; private set; }
    public bool Filterable { get; private set; }
    public bool Resizable { get; private set; }
    public int ColumnIndex { get; internal set; }
    public int MinWidth { get; internal set; }
    public Rectangle Bounds { get; private set; }
    private Point _location;
    public Point Location
    {
        get => _location;
        internal set
        {
            _location = value;
            AjBounds();
        }
    }
    public Rectangle TextBounds { get; internal set; }
    public Rectangle FilterBounds { get; internal set; }
    public SortOrder SortOrder { get; internal set; } = SortOrder.None;

    public string ActiveFilter { get; internal set; } = "";
    
    public ColumnPanelColumn(ColumnPanel parent, ColumnDefinition columnDefinition,int index)
    {
        Parent= parent;
        ColumnIndex = index;
        SetColumnDefinition(columnDefinition);
    }
    internal void SetColumnDefinition(ColumnDefinition columnDefinition)
    {
        TextAlign = columnDefinition.TextAlign;
        _width = columnDefinition.Width;
        Text = columnDefinition.Text;
        Selectable = columnDefinition.Selectable;
        ToolTipText = columnDefinition.ToolTipText;
        Filterable = columnDefinition.Filterable;
        Resizable = columnDefinition.Resizable;
        MinWidth = columnDefinition.MinWidth;
        AjBounds();
    }
    private void AjBounds()
    {
        Bounds = new Rectangle(Location.X, Location.Y, Width, Parent.Height);
        var bounds = Bounds.DeltaPosSize(Parent.ColumnsPadding.Left, Parent.ColumnsPadding.Top,-Parent.ColumnsPadding.Horizontal,-Parent.ColumnsPadding.Vertical);
        TextBounds=(Filterable)? bounds.DeltaWidth(-Bounds.Height-2) : bounds;
        FilterBounds = (Filterable) ? new Rectangle(bounds.Right - bounds.Height,bounds.Top, bounds.Height, bounds.Height):Rectangle.Empty;
    }
    public ColumnHit HitTest(Point p,bool resizing)
    {
        if (resizing)
        {
            var bounds = Bounds.DeltaWidth(4);
            if (bounds.Contains(p)) return ColumnHit.Resize;
        }
        if (!Bounds.Contains(p)) return ColumnHit.None;
        if (TextBounds.Contains(p)) return ColumnHit.Sort;
        if (FilterBounds!=Rectangle.Empty && FilterBounds.Contains(p)) return ColumnHit.Filter;
        var offset=Bounds.Right - p.X;
        if (offset>=0 && offset<4) 
            return ColumnHit.Resize;
        return ColumnHit.None;
    }
    public bool HitTest(int x)
    {
        var x0= Bounds.X;
        var x1 = Bounds.Right;
        return x>= x0 && x < x1;
    }
    

    public (Rectangle bounds, FontRectangleF textbounds,FontRectangleF iconbounds,FontRectangleF filterbounds) GetBounds(Graphics gr, Font font, Padding padding, DuoToneIcon? icon,DuoToneIcon? filtericon,int scale,int iconsep)
    {
        var iconsize=icon?.MeasureIconWithAscent(gr, font, scale/100.0f) ?? FontSizeF.Empty;
        var filtersize = filtericon?.MeasureIconWithAscent(gr, font, scale/100.0f) ?? FontSizeF.Empty;
        var bounds = TextBounds;
        var rightgap = iconsize.Width;
        if (icon!=null) rightgap += iconsep;
        var sztext= gr.MeasureTextSizeWithAscent(font, Text);
        var bounds2 = bounds.DeltaWidth(-(int)rightgap);
        var textoffset = TextAlign.AlignOffset(bounds2, new RectangleF(PointF.Empty, sztext.ToSizeHeight()));
        var textbounds = new FontRectangleF(textoffset.X, textoffset.Y, sztext.Width, sztext.Height, sztext.Ascent);
        var iconbounds = new FontRectangleF(textbounds.Right + iconsep, textbounds.Y, iconsize.Width, iconsize.Height, sztext.Ascent);
        var filterbounds = new FontRectangleF(FilterBounds.Left, textbounds.Y,filtersize.Width, filtersize.Height, sztext.Ascent);
        return (bounds,textbounds, iconbounds, filterbounds);
    }
}