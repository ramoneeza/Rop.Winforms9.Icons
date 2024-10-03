using System.ComponentModel;
using System.Drawing.Text;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls
{
    internal partial class Dummy{}
    
    [IncludeFrom(typeof(PartialIHasText),"PartialIHasBank","GetIcon","IsSuffix","UseIconColor","IconMarginLeft","IconMarginRight","OffsetText","TextRenderingHint","ShowToolTip")]
    [IncludeFrom(typeof(PartialIShowHidden))]
    public partial class OrderIcon : Label,IHasText
    {
        public ColumnPanel? ColumnPanel => Parent as ColumnPanel;
        public bool CheckChanges()
        {
            var columnDefinition = ColumnPanel?.GetColumnDefinition(ColumnIndex)??new();
            var awidth=this.Width;
            this.Width = columnDefinition.Width;
            this.TextAlign = columnDefinition.TextAlign;
            this.Selectable = columnDefinition.Selectable;
            this.ToolTipText=columnDefinition.ToolTipText;
            this.Text= columnDefinition.Text;
            AjCursor();
            Invalidate();
            return awidth != this.Width;
        }
        public event EventHandler? SortOrderChanged;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ColumnIndex { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selectable { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selected
        {
            get => SortOrder != SortOrder.None;
            set
            {
                if (!Selectable)
                {
                    SortOrder = SortOrder.None;
                    return;
                }

                if (Selected == value) return;
                SortOrder = (value) ? SortOrder.Ascending : SortOrder.None;
            }
        }
        public DuoToneColor IconColorSelected => ColumnPanel?.IconColorSelected ?? Color.Black;
        public DuoToneColor IconColorUnSelected=>ColumnPanel?.IconColorUnSelected ?? Color.Silver;
        public string IconUnselected=> ColumnPanel?.IconUnselected ?? "";
        public string IconAscending => ColumnPanel?.IconAscending ?? "";
        public string IconDescending => ColumnPanel?.IconDescending ?? "";
        
        public bool IsSuffix => ColumnPanel?.IsSuffix ?? true;

        public bool UseIconColor => ColumnPanel?.UseIconColor ?? false;
        public float OffsetText=>ColumnPanel?.OffsetText ?? 0;
        public float IconMarginLeft=>ColumnPanel?.IconMarginLeft ?? 0;
        public float IconMarginRight=> ColumnPanel?.IconMarginRight ?? 0;
        public TextRenderingHint TextRenderingHint=> ColumnPanel?.TextRenderingHint ?? TextRenderingHint.SystemDefault;


        private SortOrder _sortOrder = SortOrder.None;
        [DefaultValue(SortOrder.None)]
        public SortOrder SortOrder
        {
            get => _sortOrder;
            set
            {
                _sortOrder = value;
                LaunchTextChanged();
                LaunchSortOrderChanged();
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Ascending
        {
            get => _sortOrder == SortOrder.Ascending;
            set => SortOrder = value ? SortOrder.Ascending : SortOrder.Descending;
        }
        public float OffsetIcon=> ColumnPanel?.OffsetIcon ?? 0;
        public int MinAscent => ColumnPanel?.MinAscent ?? 0;
        public int MinHeight=> ColumnPanel?.MinHeight ?? 0;
        public int IconScale => ColumnPanel?.IconScale ?? 125;
        public bool InteriorBorder => ColumnPanel?.InteriorBorder ?? false;
        public DuoToneColor DisabledColor => ColumnPanel?.DisabledColor ?? Color.Gray;
        
        public bool ShowToolTip => ColumnPanel?.ShowToolTip ?? false;

        public bool Disabled=>!Enabled;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ToolTipText { get; private set; } = "";
        public string GetToolTipText() => ToolTipText;
        public void LaunchSortOrderChanged()
        {
            SortOrderChanged?.Invoke(this,EventArgs.Empty);
        }
        public OrderIcon() : base()
        {
           InitIHasToolTip();
           InitShowHidden();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var bc = ColumnPanel?.ColumnsBackColor ?? SystemColors.Control;
            e.Graphics.FillRectangle(new SolidBrush(bc), e.ClipRectangle);
            IconTextPaint(e.Graphics);
            if (InteriorBorder)
            {
                e.Graphics.DrawLine(Pens.White, 0, 0, 0, Height);
                e.Graphics.DrawLine(new Pen(Color.Silver), Width - 1, 0, Width - 1, Height);
            }
        }
        public override Size GetPreferredSize(Size proposedSize)
        {
            if (!AutoSize) return base.GetPreferredSize(proposedSize);
            return GetPreferedSize();
        }
        public virtual void IconTextPaint(Graphics gr)
        {
            var args = IconArgs.Factory(this);
            var measure = MeasuredIconString.Factory(gr, args);
            var p = new Padding(Padding.Left + 1, Padding.Top + 1, Padding.Right + 1, Padding.Bottom + 1);
            if (ColumnPanel?.InteriorBorder== true)
            {
                p = new Padding(p.Left + 1, p.Top, p.Right + 1, p.Bottom);
            }

            var offset = this.AlignOffset(TextAlign, measure.Bounds,p);
            gr.DrawStringWithIcons(offset, measure);
        }
        public virtual string GetText()=>Text;
        public DuoToneIcon? GetIcon() =>ColumnPanel?.Icons?.GetIcon(GetIconCode());
        public string GetIconCode()
        {
            switch (SortOrder)
            {
                case SortOrder.Ascending:
                    return IconAscending;
                case SortOrder.Descending:
                    return IconDescending;
                default:
                    return IconUnselected;
            }
        }
        public DuoToneColor GetIconColor()
        {
            switch (SortOrder)
            {
                case SortOrder.Ascending:
                case SortOrder.Descending:
                    return IconColorSelected;
                default:
                    return IconColorUnSelected;
            }
        }
        public virtual bool DisableAndThereIsDisabledColor() => Disabled && DisabledColor != DuoToneColor.Empty;
        public virtual IBankIcon? BankIcon => ColumnPanel?.BankIcon;
        public virtual IEmbeddedIcons? Icons=>ColumnPanel?.Icons;

        public Cursor SelectableCursor=> ColumnPanel?.SelectableCursor ?? Cursors.Default;

        public void AjCursor()
        {
            var normal=ColumnPanel?.Cursor ?? Cursors.Default;
            var selectable = SelectableCursor;
            this.Cursor= Selectable ? selectable : normal;
        }
    }
}
