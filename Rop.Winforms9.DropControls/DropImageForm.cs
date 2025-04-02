using System.ComponentModel;
using System.Diagnostics;
using Rop.Winforms9.GraphicsEx.Geom;


namespace Rop.Winforms9.DropControls;

public class RectangleD
{
    private RectangleF _rectangle;
    private float _diagonal;
    private float _initdiagonal;
    /// <summary>
    /// Get or Set Scale of Rectangle
    /// </summary>
    public float Scale
    {
        get
        {
            if (_initdiagonal==0) _initdiagonal = _diagonal; // Avoid 0 div
            if (_initdiagonal==0) return 0; // Avoid 0 div
            return _diagonal / _initdiagonal;
        }
        set => Diagonal = _initdiagonal * value;
    }
    private bool _suspendchanges;
    /// <summary>
    /// Set Scale without change anchor position
    /// </summary>
    /// <param name="anchor"></param>
    /// <param name="scale"></param>
    public void SetScale(int anchor, float scale)
    {
        _suspendchanges = true;
        var p = this[anchor];
        Scale = scale;
        this[anchor] = p;
        _suspendchanges = false;
        OnChanged();
    }
    /// <summary>
    /// Set Diagonal Size without change anchor position
    /// </summary>
    /// <param name="anchor"></param>
    /// <param name="diagonal"></param>
    public void SetDiagonal(int anchor, float diagonal)
    {
        _suspendchanges = true;
        var p = this[anchor];
        Diagonal = diagonal;
        this[anchor] = p;
        _suspendchanges = false;
        OnChanged();
    }
    /// <summary>
    /// Get Rectangle
    /// </summary>
    public RectangleF Rect
    {
        get => _rectangle;
        set
        {
            _rectangle = value;
            _diagonal = _rectangle.Diagonal();
            OnChanged();
        }
    }
    /// <summary>
    /// Get or Set Diagonal
    /// </summary>
    public float Diagonal
    {
        get => _diagonal;
        set
        {
            _diagonal = value;
            _rectangle = _rectangle.SetDiagonal(_diagonal);
            OnChanged();
        }
    }
    /// <summary>
    /// Get or Set Location
    /// </summary>
    public PointF Location
    {
        get => _rectangle.Location;
        set
        {
            _rectangle.Location = value;
            OnChanged();
        }
    }
    /// <summary>
    /// Get Size
    /// </summary>
    public SizeF Size
    {
        get => _rectangle.Size;
        set
        {
            _rectangle.Size = value;
            OnChanged();
        }
    }

    /// <summary>
    /// Get or Set Center Point
    /// </summary>
    public PointF Center
    {
        get => _rectangle.GetCenter();
        set
        {
            _rectangle = _rectangle.SetCenter(value);
            OnChanged();
        }
    }
    /// <summary>
    /// Constructor Rectangle D
    /// </summary>
    /// <param name="location"></param>
    /// <param name="sz"></param>
    public RectangleD(PointF location, SizeF sz)
    {
        Rect = new RectangleF(location, sz);
        ResetScale();
    }
    /// <summary>
    /// Constructor RectangleD
    /// </summary>
    /// <param name="anchor"></param>
    /// <param name="punto"></param>
    /// <param name="sz"></param>
    public RectangleD(int anchor, PointF punto, SizeF sz)
    {
        var r = new RectangleF(PointF.Empty, sz);
        Rect = r.SetAnchor(anchor, punto);
        ResetScale();
    }
    /// <summary>
    /// Current Diagonal as Scale=1
    /// </summary>
    public void ResetScale() => _initdiagonal = Diagonal;

    /// <summary>
    /// Get Point of Anchors
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public PointF this[int i]
    {
        get => Rect.GetAnchor(i);
        set { _rectangle = _rectangle.SetAnchor(i, value); OnChanged(); }
    }
    /// <summary>
    /// Get or Set LeftTop
    /// </summary>
    public PointF LeftTop
    {
        get => this[0];
        set => this[0] = value;
    }
    /// <summary>
    /// Get or Set RightTop
    /// </summary>
    public PointF RightTop
    {
        get => this[1];
        set => this[1] = value;
    }
    /// <summary>
    /// Get or Set LeftBottom
    /// </summary>
    public PointF LeftBottom
    {
        get => this[2];
        set => this[2] = value;
    }
    /// <summary>
    /// Get or Set RightBottom
    /// </summary>
    public PointF RightBottom
    {
        get => this[3];
        set => this[3] = value;
    }
    /// <summary>
    /// Left position
    /// </summary>
    public float Left => Rect.Left;

    /// <summary>
    /// Right Position
    /// </summary>
    public float Right => Rect.Right;

    /// <summary>
    /// Top Position
    /// </summary>
    public float Top => Rect.Top;

    /// <summary>
    /// Bottom Position
    /// </summary>
    public float Bottom => Rect.Bottom;

    /// <summary>
    /// Event Change Size or Position
    /// </summary>
    public event EventHandler? Changed;
    /// <summary>
    /// OnChanged Event
    /// </summary>
    protected virtual void OnChanged()
    {
        if (!_suspendchanges) Changed?.Invoke(this, EventArgs.Empty);
    }
}

public partial class DropImageForm : Form
{
    private readonly Pen penesquina = new Pen(Brushes.Black, 3);
    private readonly Rectangle[] _picposa;
    private int aoverpicpos = -1;
    public DropImageForm()
    {
        InitializeComponent();
        var sz = picpos.Size;
        var szm = new Size(sz.Width / 2, sz.Height / 2);
        _picposa = new[]
        {
            new Rectangle(0, 0, szm.Width, szm.Height),
            new Rectangle(szm.Width, 0, szm.Width, szm.Height),
            new Rectangle(0, szm.Height, szm.Width, szm.Height),
            new Rectangle(szm.Width, szm.Height, szm.Width, szm.Height),
            new Rectangle(szm.Width / 2, szm.Height / 2, szm.Width, szm.Height)
        };
        modifyBitmap1.AllowDrop = true;
        Ajpicpos(-1);
        Ajposiciones();
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size DesiredSize { get; set; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap OriginalBitmap { get; set; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Bitmap FotoFinal { get; private set; }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Left:
            case Keys.Right:
            case Keys.Up:
            case Keys.Down:
                DoKeys(keyData);
                return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void DropImageForm_Load(object sender, EventArgs e)
    {
        if (DesiredSize.IsEmpty)
        {
            MessageBox.Show("Error, tamaño no especificado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Abort;
        }
        modifyBitmap1.DesiredSize = DesiredSize;
        if (OriginalBitmap != null)
            Ponfoto(new Bitmap(OriginalBitmap));
    }

    private void Picture_DragDrop(object sender, DragEventArgs e)
    {
        try
        {
            var a = e.Data.GetData(DataFormats.FileDrop) as string[];
            Debug.Assert(a != null, "a != null");
            if (a.Length != 1)
            {
                MessageBox.Show("Arrastre un solo fichero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap b;
            try
            {
                b = new Bitmap(a[0]);
            }
            catch
            {
                MessageBox.Show("Imposible leer imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Ponfoto(b);
        }
        catch
        {
            // ignored
        }
    }

    private void Picture_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void Ponfoto(Bitmap lafoto)
    {
        modifyBitmap1.OriginalBitmap = lafoto;
    }

    private void btnGrabarMano_Click(object sender, EventArgs e)
    {
        FotoFinal = modifyBitmap1.FinalBitmap;
        DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void Setpos(Label lb, int x, int y)
    {
        lb.Text = string.Format("{0},{1}", x, y);
    }

    private void Ajposiciones()
    {
        var r = Rectangle.Round(modifyBitmap1.Original.Rect);
        Setpos(lbtl, r.Left, r.Top);
        Setpos(lbtr, r.Right, r.Top);
        Setpos(lbbl, r.Bottom, r.Left);
        Setpos(lbbr, r.Bottom, r.Right);
    }

    private void DrawEsquina(Graphics g, int x, int y, int dx, int dy)
    {
        var p = new Point(x, y);
        g.DrawLine(penesquina, p, new Point(p.X + dx, p.Y));
        g.DrawLine(penesquina, p, new Point(p.X, p.Y + dy));
    }

    private void Ajpicpos(int a = -1)
    {
        var bmp = new Bitmap(picpos.Width, picpos.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Silver);
            if (a >= 0)
            {
                var r = _picposa[a];
                g.FillRectangle(Brushes.SkyBlue, r);
            }
            DrawEsquina(g, 0, 0, 8, 8);
            DrawEsquina(g, bmp.Width - 1, 0, -8, 8);
            DrawEsquina(g, 0, bmp.Height - 1, 8, -8);
            DrawEsquina(g, bmp.Width - 1, bmp.Height - 1, -8, -8);
            g.DrawEllipse(penesquina, _picposa[4]);
        }
        picpos.Image = bmp;
    }

    private void picpos_MouseMove(object sender, MouseEventArgs e)
    {
        for (var f = 4; f >= 0; f--)
        {
            var r = _picposa[f];
            if (r.Contains(e.Location))
            {
                if (aoverpicpos != f)
                {
                    aoverpicpos = f;
                    Ajpicpos(f);
                }
                return;
            }
        }
        if (aoverpicpos != -1) Ajpicpos(-1);
        aoverpicpos = -1;
    }

    private void picpos_MouseLeave(object sender, EventArgs e)
    {
        Ajpicpos(-1);
        aoverpicpos = -1;
    }

    private void modifyBitmap1_Changed(object sender, EventArgs e)
    {
        Ajposiciones();
    }

    private void picpos_MouseClick(object sender, MouseEventArgs e)
    {
        for (var f = 4; f >= 0; f--)
        {
            var r = _picposa[f];
            if (r.Contains(e.Location))
            {
                Picposclick(f);
                return;
            }
        }
    }

    private void Picposclick(int f)
    {
        switch (f)
        {
            case 0:
                modifyBitmap1.Original[f] = new PointF(0, 0);
                break;
            case 1:
                modifyBitmap1.Original[f] = new PointF(modifyBitmap1.DesiredSize.Width, 0);
                break;
            case 2:
                modifyBitmap1.Original[f] = new PointF(0, modifyBitmap1.DesiredSize.Height);
                break;
            case 3:
                modifyBitmap1.Original[f] = new PointF(
                    modifyBitmap1.DesiredSize.Width,
                    modifyBitmap1.DesiredSize.Height);
                break;
            case 4:
                modifyBitmap1.Original[f] = new PointF(
                    // ReSharper disable once PossibleLossOfFraction
                    modifyBitmap1.DesiredSize.Width / 2,
                    // ReSharper disable once PossibleLossOfFraction
                    modifyBitmap1.DesiredSize.Height / 2);
                break;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var sz = modifyBitmap1.Original.Size;
        var dsz = DesiredSize;
        var esc = sz.Width / dsz.Width;
        if (esc < 0.01f) esc = 1;
        var nsz = new SizeF(sz.Width / esc, sz.Height / esc);
        modifyBitmap1.Original.Size= nsz;
        modifyBitmap1.Original.Location = PointF.Empty;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        var sz = modifyBitmap1.Original.Size;
        var dsz = DesiredSize;
        var esc = sz.Height / dsz.Height;
        if (esc < 0.01f) esc = 1;
        var nsz = new SizeF(sz.Width / esc, sz.Height / esc);
        modifyBitmap1.Original.Size=nsz;
        modifyBitmap1.Original.Location=PointF.Empty;
    }

    private void MoveLocation(int dx, int dy)
    {
        var p = modifyBitmap1.Original.Location;
        modifyBitmap1.Original.Location = new PointF(p.X + dx, p.Y + dy);
    }

    private void DoKeys(Keys k)
    {
        switch (k)
        {
            case Keys.Up:
                MoveLocation(0, -1);
                break;
            case Keys.Down:
                MoveLocation(0, 1);
                break;
            case Keys.Left:
                MoveLocation(-1, 0);
                break;
            case Keys.Right:
                MoveLocation(1, 0);
                break;
        }
    }

}