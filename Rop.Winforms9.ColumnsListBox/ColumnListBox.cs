using System.Collections;
using System.ComponentModel;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.DuotoneIcons.PartialControls;
using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.ColumnsListBox
{
    [DesignerCategory("code")]
    public partial class ColumnListBox:Control
    {
        public event EventHandler<DrawColumnsEventArgs>? DrawColumns;
        public event EventHandler<SortItemsArg>? SortItems; 
        
        

        private BorderStyle _borderStyle;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BorderStyle BorderStyle
        {
            get => _borderStyle;
            set
            {
                _borderStyle = value;
                PerformLayout();
            }
        }

        public ColumnListBox()
        {
            // Define Header
            Header = new ColumnPanel();
            Header.Height = 20;
            Header.BorderStyle = BorderStyle.Fixed3D;
            Header.BorderRaised = true;
            Header.InteriorBorder = true;
            Header.Dock= DockStyle.Top;
            Header.TabIndex = 0;
            //Define ListBox
            ListBox = new CompatibleListBox();
            ListBox.Dock = DockStyle.Fill;
            ListBox.TabIndex = 1;
            ListBox.BorderStyle= BorderStyle.None;
            ListBox.IntegralHeight = false;
            ListBox.DrawMode = DrawMode.OwnerDrawFixed;
            // Add controls
            SuspendLayout();
            this.Controls.Add(ListBox);
            this.Controls.Add(Header);
            ResumeLayout(true);

            // Add Events
            Header.SelectedChanged += Header_SelectedChanged;
            Header.OrderChanged += Header_OrderChanged;
            ListBox.DrawItem += ListBox_DrawItem;
            ListBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
            ListBox.MouseClick += ListBox_MouseClick;
        }

        private void ListBox_DrawItem(object? sender, DrawItemEventArgs e)
        {
            OnDrawItem(e);
        }

        private void Header_OrderChanged(object? sender, ColumnPanelOrderArgs e)
        {
            IntOrderChanged();
        }

        private void Header_SelectedChanged(object? sender, EventArgs e)
        {
            IntOrderChanged();
        }

        private void IntOrderChanged()
        {
            var a = ListBox.SelectedItem;
            var items = ListBox.Items;
            var args = new SortItemsArg(Header.SelectedColumn,Header.SelectedOrder,items);
            OnSortItems(args);
            ListBox.BeginUpdate();
            ListBox.Items.Clear();
            foreach (var item in args.Items)
            {
                ListBox.Items.Add(item);
            }
            ListBox.EndUpdate();
            if (a != null)
            {
                ListBox.SelectedItem = a;
            }
        }

        private void OnSortItems(SortItemsArg args)
        {
            SortItems?.Invoke(this,args);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            // Ajustar la posición y tamaño de los controles hijos según el Padding
            var padding = this.Padding;
            var clientRect = this.ClientRectangle;
            var p = padding;
            if (BorderStyle != BorderStyle.None)
            {
                p = new Padding(padding.Left + 1, padding.Top + 1, padding.Right + 1, padding.Bottom + 1);
            }
            Header.Location = new Point(p.Left, p.Top);
            Header.Width = clientRect.Width - p.Horizontal;

            ListBox.Location = new Point(p.Left, p.Top + Header.Height);
            ListBox.Size = new Size(clientRect.Width - p.Horizontal, clientRect.Height - p.Vertical - Header.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
           
                switch (BorderStyle)
                {
                    case BorderStyle.FixedSingle:
                        e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, Width - 1, Height - 1);
                        break;
                    case BorderStyle.Fixed3D:
                        e.Graphics.DrawLines(Pens.Gray,new PointF[]{new(0,Height-1),new(0,0),new(Width-1,0)});
                        e.Graphics.DrawLines(Pens.White,new PointF[]{new(0,Height-1),new(Width-1,Height-1),new(Width-1,0)});
                        break;
                    default:
                        break;
                }
           
        }
        protected virtual void OnDrawColumns(DrawColumnsEventArgs drawColumnsEventArgs)
        {
            DrawColumns?.Invoke(this, drawColumnsEventArgs);
        }
    }
}
