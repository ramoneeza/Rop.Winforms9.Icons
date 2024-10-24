using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls
{
    internal partial class Dummy{}
    [IncludeFrom(typeof(PartialIHasBank))]
    public partial class ColumnPanel:Control,IShowHidden, IHasBank
    {
        public ColumnPanel()
        {
            InitShowHidden();
            _icons = IconRepository.DefaultBank;
            _columnDefinitions.ListChanged += _columnDefinitions_ListChanged;
        }
        private BindingList<ColumnDefinition> _columnDefinitions=new();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<ColumnDefinition> ColumnDefinitions
        {
            get => _columnDefinitions;
            set
            {
                if (value == _columnDefinitions) return;
                _columnDefinitions.ListChanged-= _columnDefinitions_ListChanged;
                _columnDefinitions = value;
                _columnDefinitions.ListChanged += _columnDefinitions_ListChanged;
                foreach (var columnDefinition in _columnDefinitions)
                {
                    columnDefinition.PropertyChanged += (_,_)=> _ajColumns();
                }
                _ajColumns();
            }
        }
        private void _columnDefinitions_ListChanged(object? sender, ListChangedEventArgs e)
        {
            _ajColumns();
        }
        private void _ajColumns()
        {
            var number = _columnDefinitions.Count;
            if (number < _columns.Count)
            {
                while (_columns.Count > number)
                {
                    var c = _columns.Last();
                    _columns.Remove(c);
                    Controls.Remove(c);
                    c.Dispose();
                }
            }
            else
            {
                while (_columns.Count<number)
                {
                    _addcolumn();
                }
            }

            var flag = false;
            foreach (var orderIcon in _columns)
            {
                var r=orderIcon.CheckChanges();
                flag=flag || r;
            }
            if (flag) _doLayout();
            Invalidate();
        }
        private readonly List<OrderIcon> _columns = new();
        public event EventHandler? SelectedChanged;
        public event EventHandler<ColumnPanelOrderArgs>? OrderChanged;
        public int NumberOfColumns=>_columns.Count;
        private void _addcolumn()
        {
            var acolumn=_columns.LastOrDefault();
            var column = new OrderIcon();
            column.ColumnIndex = _columns.Count;
            column.SortOrder = SortOrder.None;
            column.AutoSize = false;
            column.TextAlign = ContentAlignment.MiddleLeft;
            column.Font = Font;
            column.Left= acolumn?.Right ?? 0;
            column.Top = 0;
            column.Height = Height;
            column.Text = $"Col {column.ColumnIndex}";
            _columns.Add(column);
            this.Controls.Add(column);
            column.MouseDown += C_Click;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            //Draw Border con BorderStyle sin usar ControlPaint
            switch (BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, Width - 1, Height - 1);
                    break;
                case BorderStyle.Fixed3D:
                    var p1=BorderRaised? Pens.White : Pens.Gray;
                    var p2=BorderRaised? Pens.Gray : Pens.White;
                    e.Graphics.DrawLines(p1,new PointF[]{new(0,Height-1),new(0,0),new(Width-1,0)});
                    e.Graphics.DrawLines(p2,new PointF[]{new(0,Height-1),new(Width-1,Height-1),new(Width-1,0)});
                    break;
                default:
                    break;
            }
        }

        private BorderStyle _borderStyle = System.Windows.Forms.BorderStyle.None;
        [DefaultValue(BorderStyle.None)]
        public BorderStyle BorderStyle
        {
            get => _borderStyle;
            set
            {
                if (value == _borderStyle) return;
                _borderStyle = value;
                _doLayout();
                Invalidate();
            }
        }

        private bool _borderRaised;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool BorderRaised
        {
            get => _borderRaised;
            set
            {
                _borderRaised = value;
                _doLayout();
                Invalidate();
            }
        }

        private OrderIcon? _selected = null;
        [Browsable(false)]
        public OrderIcon? Selected=> _selected;
        
        [DefaultValue(-1)]
        public int SelectedColumn
        {
            get => _selected?.ColumnIndex ?? -1;
            set
            {
                if (_selected?.ColumnIndex==value) return;
                _selected = _getcolumn(value);
                foreach (var c in _columns)
                    c.Selected = c == _selected;
                SelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public SortOrder SelectedOrder=> _selected?.SortOrder ?? SortOrder.None;
        public bool Ascending => _selected?.Ascending ?? true;
        private void C_Click(object? sender, EventArgs e)
        {
            if (sender is not OrderIcon { Selectable: true } oi) return;
            if (oi==Selected)
            {
                oi.Ascending = !oi.Ascending;
            }
            else
            {
                SelectedColumn = oi.ColumnIndex;
            }
            OrderChanged?.Invoke(this, new ColumnPanelOrderArgs(new ColumnPanelOrder { ColumnIndex = oi.ColumnIndex, SortOrder = oi.SortOrder }));
        }

        private OrderIcon? _getcolumn(int index)
        {
            if (index < 0 || index >= _columns.Count) return null;
            return _columns[index];
        }
        [Browsable(false)]
        public Rectangle[] ColumnsBounds => _columns.Select(c => c.Bounds).ToArray();
        
        private void _doaction(Action<OrderIcon> action)
        {
            foreach (var c in _columns)
            {
                action(c);
            }
        }

        private void _updateproperties()
        {
            _doaction(c =>
            {
                c.Font = Font;
                c.Padding = ColumnsPadding;
                c.BackColor = ColumnsBackColor;
            });
        }
        public int[] ColumnsWidth=>_columns.Select(c => c.Width).ToArray();
        public string[] ColumnsNames=> _columns.Select(c => c.Text).ToArray();
        public bool[] EnabledColumns => _columns.Select(c => c.Enabled).ToArray();
        public bool[] SelectablesColumns => _columns.Select(c => c.Enabled).ToArray();
        public OrderIcon[] Columns => _columns.ToArray();
        private Padding _columnsPadding = new Padding(3);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Padding ColumnsPadding
        {
            get => _columnsPadding;
            set
            {
                _columnsPadding = value;
                _updateproperties();
            }
        }
        private bool _interiorborder = false;
        [DefaultValue(false)]
        public bool InteriorBorder
        {
            get => _interiorborder;
            set
            {
                _interiorborder = value;
                Invalidate();
            }
        }
        private Color _columnsBackColor = SystemColors.Control;
        [DefaultValue(typeof(SystemColors), "Control")]
        public Color ColumnsBackColor
        {
            get => _columnsBackColor;
            set
            {
                _columnsBackColor = value;
                _updateproperties();
            }
        }
        private Cursor _selectableCursor = Cursors.Hand;
        [DefaultValue(typeof(Cursors), "Hand")]
        public Cursor SelectableCursor
        {
            get => _selectableCursor;
            set
            {
                _selectableCursor = value;
                _doaction(c => c.AjCursor());
            }
        }

        private void _doLayout()
        {
            var ap = Point.Empty;
            var h=this.Height;
            if (BorderStyle!= BorderStyle.None)
            {
                ap.X += 1;
                ap.Y += 1;
                h -= 2;
            }
            foreach (var c in _columns)
            {
                c.Location = ap;
                ap.X += c.Width;
                c.Size = new Size(c.Width,h);
                c.AjCursor();
            }
        }
        
        
        #region ColumnPanel Properties

        private DuoToneColor _iconColorSelected=Color.Black;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DuoToneColor IconColorSelected
        {
            get => _iconColorSelected;
            set
            {
                _iconColorSelected = value;
                Invalidate();
            }
        }

        private DuoToneColor _iconColorUnSelected=Color.Gray;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DuoToneColor IconColorUnSelected
        {
            get => _iconColorUnSelected;
            set
            {
                _iconColorUnSelected = value;
                Invalidate();
            }
        }
        
        //IconAscending = "ChevronDown";
        //IconDescending = "ChevronUp";
        //IconUnselected = "ChevronDown";

        private string _iconUnselected = "_ChevronDown";
        [DefaultValue("_ChevronDown")]
        public string IconUnselected
        {
            get => _iconUnselected;
            set
            {
                _iconUnselected = value;
                Invalidate();
            }
        }

        private string _iconAscending = "_ChevronDown";
        [DefaultValue("_ChevronDown")]
        public string IconAscending
        {
            get => _iconAscending;
            set
            {
                _iconAscending = value;
                Invalidate();
            }
        }

        private string _iconDescending = "_ChevronUp";
        [DefaultValue("_ChevronDown")]
        public string IconDescending
        {
            get => _iconDescending;
            set
            {
                _iconDescending = value;
                Invalidate();
            }
        }

        private bool _issuffix = true;
        [DefaultValue(true)]
        public bool IsSuffix
        {
            get => _issuffix;
            set
            {
                _issuffix = value;
                Invalidate();
            }
        }
        private bool _useIconColor;
        [DefaultValue(false)]
        public bool UseIconColor
        {
            get => _useIconColor;
            set
            {
                _useIconColor = value;
                Invalidate();
            }
        }

        private int _offsetText;
        [DefaultValue(0)]
        public int OffsetText
        {
            get => _offsetText;
            set
            {
                _offsetText = value;
                Invalidate();
            }
        }
        private int _iconMarginLeft;
        [DefaultValue(0)]
        public int IconMarginLeft
        {
            get => _iconMarginLeft;
            set
            {
                _iconMarginLeft = value;
                Invalidate();
            }
        }
        private int _iconMarginRight;
        [DefaultValue(0)]
        public int IconMarginRight
        {
            get => _iconMarginRight;
            set
            {
                _iconMarginRight = value;
                Invalidate();
            }
        }
        private TextRenderingHint _textRenderingHint;
        [DefaultValue(TextRenderingHint.SystemDefault)]
        public TextRenderingHint TextRenderingHint
        {
            get => _textRenderingHint;
            set
            {
                _textRenderingHint = value;
                Invalidate();
            }
        }

        private bool _showToolTip = false;
        [DefaultValue(false)]
        public bool ShowToolTip
        {
            get => _showToolTip;
            set
            {
                _showToolTip = value;
                Invalidate();
            }
        }


        #endregion
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _doLayout();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public ColumnDefinition? GetColumnDefinition(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= _columnDefinitions.Count) return null;
            return _columnDefinitions[columnIndex];
        }
        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            foreach (var orderIcon in _columns)
            {
                orderIcon.Invalidate();
            }
        }
    }

    public readonly struct ColumnPanelOrder
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
    }
}
