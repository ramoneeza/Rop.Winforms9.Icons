using System.ComponentModel;
using System.Drawing.Drawing2D;
using Rop.Winforms9.GraphicsEx;
using Rop.Winforms9.GraphicsEx.Geom;

namespace Rop.Winforms9.DropControls;

public class ModifyBitmap : Control
{
    private readonly Brush _backGroundBrush;
    private readonly Pen _lapiz;
    private Size _desiredSize;
    private Bitmap? _originalBitmap;
    private int pickmode;
    private RectangleF pickorirect;
    private Point pickpoint;

    public ModifyBitmap()
    {
        DoubleBuffered = true;
        Text = "";
        BmpPadding = 10;
        _lapiz = new Pen(Brushes.Black);
        _lapiz.DashStyle = DashStyle.Dash;
        _backGroundBrush = new TextureBrush(ArtClass.Chess);
        DesiredSize = new Size(320, 200);
        OriginalBitmap = null;
        pickmode = -1;
    }
    public event EventHandler Changed;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap? OriginalBitmap
    {
        get => _originalBitmap;
        set
        {
            _originalBitmap = value;
            if (DesiredSize == Size.Empty) _desiredSize = new Size(320, 200);
            var sz = OriginalBitmap == null
                ? new SizeF(DesiredSize)
                : new SizeF(OriginalBitmap.Size);
            var e1 = DesiredSize.Width / sz.Width;
            var e2 = DesiredSize.Height / sz.Height;
            var scale = Math.Max(e1, e2);
            Original = new RectangleD(4, new PointF(sz.Width, sz.Height).Multiply(0.5f), sz);
            Original.Changed += Original_Changed;
            Original.SetScale(4, scale);
        }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Size DesiredSize
    {
        get => _desiredSize;
        set
        {
            _desiredSize = value;
            OnResize(null);
        }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int BmpPadding { get; set; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RectangleD Display { get; private set; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RectangleD Original { get; private set; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap FinalBitmap { get; private set; }

    public void OnChanged()
    {
        Changed?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        SizeF newsz = Size;
        var bmpsize = new SizeF(newsz.Width - BmpPadding * 2, newsz.Height - BmpPadding * 2);
        if (DesiredSize == Size.Empty) _desiredSize = Size.Round(bmpsize);
        Display = new RectangleD(4, newsz.ToPointF().Multiply(0.5f), DesiredSize);
           

        // var DisplayScale = Math.Min(e1, e2);
        var displayScale = 1.0f;
        Display.SetScale(4, displayScale);
        AjFinalBitmap();
        Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.FillRectangle(_backGroundBrush, e.ClipRectangle);
        g.InterpolationMode = InterpolationMode.HighQualityBilinear;
        if (FinalBitmap == null) AjFinalBitmap();
        g.DrawImage(FinalBitmap, Display.Rect);
        var r = Rectangle.Round(RectToDisplay(Original.Rect));
        g.DrawRectangle(Pens.White, r);
        g.DrawRectangle(_lapiz, r);
        if (OriginalBitmap == null)
        {
            var rb = Rectangle.Round(Display.Rect);
            g.DrawLine(Pens.Silver, new Point(rb.Left, rb.Top), new Point(rb.Right, rb.Bottom));
            g.DrawLine(Pens.Silver, new Point(rb.Right, rb.Top), new Point(rb.Left, rb.Bottom));
            g.DrawCenterMiddleString(Text, Font, Brushes.Black, rb);
        }
        else
        {
            for (var f = 0; f < 4; f++)
            {
                var ra = GetRectAnchorDisplay(f, 3);
                var r2 = GetRectAnchorDisplay(f, 4);
                g.DrawRectangle(Pens.White, ra);
                g.DrawRectangle(Pens.Black, r2);
            }
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            pickmode = GetPick(e.Location);
            if (pickmode >= 0)
            {
                pickpoint = e.Location;
                pickorirect = Original.Rect;
                Cursor = pickmode == 4 ? Cursors.Hand : Cursors.Cross;
            }
        }
        base.OnMouseDown(e);
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        pickmode = -1;
        Cursor = Cursors.Default;
        base.OnMouseUp(e);

    }
    protected override void OnLeave(EventArgs e)
    {
        pickmode = -1;
        Cursor = Cursors.Default;
        base.OnLeave(e);

    }


    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (pickmode >= 0)
            Manipula(e.Location);
        base.OnMouseMove(e);
    }

    private void Original_Changed(object? sender, EventArgs e)
    {
        AjFinalBitmap();
        Refresh();
        OnChanged();
    }

    private PointF GetAnchorOri(int n)
    {
        var b = Original[n];
        return b;
    }

    private Point GetAnchorDisplay(int n)
    {
        var p = GetAnchorOri(n);
        return Point.Round(PointToDisplay(p));
    }

    private Rectangle GetRectAnchorDisplay(int n, int r = 1)
    {
        var p = GetAnchorDisplay(n);
        var rc = new Rectangle(p.X - r, p.Y - r, r * 2 + 1, r * 2 + 1);
        return rc;
    }

    private int GetPick(Point p)
    {
        for (var f = 0; f < 4; f++)
        {
            var r = GetRectAnchorDisplay(f, 3);
            if (r.Contains(p)) return f;
        }
        var r2 = RectToDisplay(Original.Rect);
        if (r2.Contains(p)) return 4;
        return -1;
    }

    private void AjFinalBitmap()
    {
        FinalBitmap = new Bitmap(DesiredSize.Width, DesiredSize.Height);
        using (var g = Graphics.FromImage(FinalBitmap))
        {
            g.Clear(Color.White);
            if (OriginalBitmap == null) return;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImage(OriginalBitmap, Original.Rect);
        }
    }

    private PointF PointToDisplay(PointF p)
    {
        var x = p.X * Display.Scale + Display.Left;
        var y = p.Y * Display.Scale + Display.Top;
        return new PointF(x, y);
    }

    private SizeF SizeToDisplay(SizeF s)
    {
        return s.Multiply(Display.Scale);
    }

    private RectangleF RectToDisplay(RectangleF s)
    {
        return new RectangleF(PointToDisplay(s.Location), SizeToDisplay(s.Size));
    }

    private PointF DisplayToPoint(PointF p)
    {
        var x = (p.X - Display.Left) / Display.Scale;
        var y = (p.Y - Display.Top) / Display.Scale;
        return new PointF(x, y);
    }

    private SizeF DisplayToSize(SizeF s)
    {
        var w = s.Width / Display.Scale;
        var h = s.Height / Display.Scale;
        return new SizeF(w, h);
    }

    private RectangleF DisplayToRect(RectangleF s)
    {
        return new RectangleF(DisplayToPoint(s.Location), DisplayToSize(s.Size));
    }

    private void Manipula(Point point)
    {
        var difx = point.X - pickpoint.X;
        var dify = point.Y - pickpoint.Y;
        if (pickmode == 4)
        {
            var t = pickorirect.Location;
            var p = DisplayToSize(new SizeF(difx, dify));
            t = t.Add(p);
            Original.Location = t;
        }
        else
        {
            var p2 = pickorirect.GetAnchor(3 - pickmode);
            var p1 = pickorirect.GetAnchor(pickmode);
            var s = DisplayToSize(new SizeF(difx, dify));
            p1 = p1.Add(s);
            var d = Distance(p1, p2);
            if (d < 0) d = 1;
            Original.SetDiagonal(3 - pickmode, d);
        }
    }
    public static float Distance(PointF p, PointF p2)
    {
        var s = Diff(p, p2);
        return s.Diagonal();
    }
    public static SizeF Diff(PointF p, PointF p2)
    {
        return new SizeF(p2.Sub(p));
    }
}