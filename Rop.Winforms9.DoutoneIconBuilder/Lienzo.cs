using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    public class Lienzo:Control
    {
        public event EventHandler<(Point,MouseButtons)>? PixelClick;
        public event EventHandler<(Point, MouseButtons)>? PixelMove;
        private Point _cursorPosition= Point.Empty;
        private BmpIcon? _bmpIcon;
        public Point CursorPosition => _cursorPosition;
        public event EventHandler<Point>? CursorPositionChanged;
        public Color Color2 { get; } = Color.FromArgb(128, 128, 128);
        public BmpIcon? BmpIcon
        {
            get => _bmpIcon;
            set
            {
                if (value == null)
                {
                    _bmpIcon= null;
                    Zoom = 1;
                    Invalidate();
                    return;
                }
                _bmpIcon = value.Clone();
                AjZoom();
            }
        }
        public void AjZoom()
        {
            Zoom = (int)MaxScaleFactor(_bmpIcon?.Size??Size.Empty, Size);
            if (Zoom == 0) Zoom = 1;
            _cursorPosition = new Point(-1, -1);
            CursorPositionChanged?.Invoke(this, _cursorPosition);
            Invalidate();
            return;
            float MaxScaleFactor(Size r1,Size r2)
            {
                float scaleX = (float)r2.Width / r1.Width;
                float scaleY = (float)r2.Height / r1.Height;
                return Math.Min(scaleX, scaleY);
            }
        }
        public int Baseline
        {
            get => _bmpIcon?.BaseLine??0;
            set
            {
                if (_bmpIcon==null) return;
                _bmpIcon.BaseLine = value;
                Invalidate();
            }
        }
        public bool Editing { get; set; }

        public Cursor EditingCursor { get; set; }

        public Cursor TransParentCursor { get; }
        
        private Cursor _createTransparentCursor()
        {
            Bitmap bitmap = new Bitmap(32, 32);
            IntPtr ptr = bitmap.GetHicon();
            return new Cursor(ptr);
        }
        
        public Lienzo()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered= true;
            TransParentCursor = _createTransparentCursor();
            EditingCursor = TransParentCursor;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            TransParentCursor.Dispose();
        }

        private Point _positionToPixel(Point position)
        {
            return new Point((position.X / Zoom)-1, (position.Y / Zoom)-1);
        }
        private Point _pixelToPosition(Point pixel)
        {
            return new Point((pixel.X + 1) * Zoom, (pixel.Y + 1) * Zoom);
        }



        
        public int Zoom { get; private set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_bmpIcon == null)
            {
                base.OnPaint(e);
                return;
            }
            e.Graphics.Clear(Color.LightSeaGreen);
            var rectbmp = new Rectangle(Zoom, Zoom, (int)(_bmpIcon.Size.Width * Zoom), (int)(_bmpIcon.Size.Height * Zoom));
            e.Graphics.FillRectangle(Brushes.White,rectbmp);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            e.Graphics.DrawImage(_bmpIcon.Bitmap,rectbmp);
            e.Graphics.DrawLine(Pens.Red,Zoom,(Baseline+1)*Zoom,(_bmpIcon.Size.Width+1)*Zoom,(Baseline+1)*Zoom);
            if (Editing && _cursorPosition.X>=0)
            {
                var pos = _cursorPosition;
                var r= new Rectangle(_pixelToPosition(pos),new Size(Zoom, Zoom));
                e.Graphics.DrawRectangle(Pens.Yellow, r);
                var r2 = new Rectangle(r.X - 1, r.Y - 1, r.Width + 2, r.Height + 2);
                e.Graphics.DrawRectangle(Pens.Black, r2);
            }
        }

        private Point GetLienzoPosition(Point mouse)
        {
            var pos = _positionToPixel(mouse);
            if (pos.X == -1) pos.X = 0;
            if (pos.Y == -1) pos.Y = 0;
            if (pos.X== _bmpIcon?.Size.Width) pos.X--;
            if (pos.Y == _bmpIcon?.Size.Height) pos.Y--;
            var valid=pos.X>= 0 && pos.X < _bmpIcon?.Size.Width && pos.Y >= 0 && pos.Y < _bmpIcon?.Size.Height;
            _cursorPosition = valid ? pos : new Point(-1, -1);
            CursorPositionChanged?.Invoke(this, _cursorPosition);
            return _cursorPosition;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var poslienzo = GetLienzoPosition(e.Location);
            var valid=poslienzo is { X: >= 0, Y: >= 0 };
            if (!valid)
            {
                Cursor = Cursors.No;
                
            }
            else
            {
                if (!Editing)
                {
                    Cursor = Cursors.No;
                }
                else
                {
                    Cursor = EditingCursor;
                    if (e.Button != MouseButtons.None)
                    {
                        _doPixelMove(poslienzo, e.Button);
                    }
                }
            }

            Invalidate();
        }

        private void _doPixelMove(Point poslienzo, MouseButtons eButton)
        {
            PixelMove?.Invoke(this, (poslienzo, eButton));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _updateCursor();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Default;
            _cursorPosition = new Point(-1, -1);
            CursorPositionChanged?.Invoke(this, _cursorPosition);
            Invalidate();
        }

        private void _updateCursor()
        {
            if (!Editing)
            {
                Cursor = Cursors.No;
            }
            else
            {
                Cursor = EditingCursor;
            }
        }
       
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            var poslienzo = GetLienzoPosition(e.Location);
            var valid = poslienzo.X >= 0 && poslienzo.Y >= 0;
            if (!valid) return;
            if (!Editing) return;
            _doPixelClick(poslienzo,e.Button);
        }

        private void _doPixelClick(Point poslienzo,MouseButtons b)
        {
            PixelClick?.Invoke(this, (poslienzo,b));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AjZoom();
        }

        public int GetPixel(int x, int y)
        {
            return _bmpIcon?.GetPixel(x, y) ?? 0;
        }
        public void SetPixel(int x, int y, int c)
        {
            _bmpIcon?.SetPixel(x, y, c);
            Invalidate();
        }

        public void Fill(int eX, int eY, int np)
        {
            var tofp= GetPixel(eX, eY);
            var queue = new Queue<Point>();
            queue.Enqueue(new Point(eX, eY));
            while (queue.Any())
            {
                var p = queue.Dequeue();
                var c = GetPixel(p.X, p.Y);
                if (c!=tofp) continue;
                SetPixel(p.X, p.Y, np);
                if (p.X > 0) queue.Enqueue(new Point(p.X - 1, p.Y));
                if (p.X < Size.Width - 1) queue.Enqueue(new Point(p.X + 1, p.Y));
                if (p.Y > 0) queue.Enqueue(new Point(p.X, p.Y - 1));
                if (p.Y < Size.Height - 1) queue.Enqueue(new Point(p.X, p.Y + 1));
            }
        }
    }
}
