using System.Collections.Frozen;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms.Design;
using Rop.Windows9.Bitmaps;
using Rop.Winforms9.GraphicsEx;
using Rop.Winforms9.GraphicsEx.Geom;

namespace Rop.Winforms9.DropControls;

internal partial class Dummy{}

public abstract partial class BaseTextBoxDropControl:Control {
    public event CancelEventHandler? CmdRename;
    public event CancelEventHandler? CmdClear;
    public event CancelEventHandler? CmdPaste;
    public event CancelEventHandler? CmdCopy;
    public event CancelEventHandler? CmdEdit;
    public event EventHandler? ValueChanged;
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual string[] AllowedExtensions
    {
        get => AllowedExtensionsSet.ToArray();
        set => AllowedExtensionsSet = value.ToFrozenSet(StringComparer.OrdinalIgnoreCase);
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DropControlStatus Status
    {
        get => _status;
        protected set =>this.SetInv(ref _status, value);
    }
    private string _originalValue = "";
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string OriginalValue
    {
        get => _originalValue;
        set
        {
            _originalValue = value;
            _value = value;
            Status = (value == "") ? DropControlStatus.Empty : DropControlStatus.Original;
        }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public byte[]? NewData { get; protected set; }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DropTypes[] AllowedDropTypes { get; set; } =[DropTypes.FileDrop, DropTypes.FileGroupDescriptor];
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsImageFile { get; set; }

    private int _dropBoxWidth = 60;
    [DefaultValue(60)]
    public int DropBoxWidth
    {
        get => _dropBoxWidth;
        set => this.SetInv(ref _dropBoxWidth, value);
    }
    private bool _hideValidIcon;
    [DefaultValue(false)]
    public bool HideValidIcon
    {
        get => _hideValidIcon;
        set => this.SetInv(ref _hideValidIcon , value);
    }
    [DefaultValue(false)]
    public bool ShowRenameTool { get; set; }
    [DefaultValue(false)]
    public bool ShowEditTool { get; set; }
    private bool _showOpenFile;
    [DefaultValue(false)]
    public bool ShowOpenFile
    {
        get => _showOpenFile;
        set=>this.SetInv(ref _showOpenFile,value);
    }
    public BaseTextBoxDropControl()
    {
        base.AllowDrop = true;
        using (TextBox tempTextBox = new TextBox())
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            this.MinimumSize = new System.Drawing.Size(100, tempTextBox.Height);
        }
        ShowEditTool = false;
        ShowRenameTool = false;
        Cms = new TextBoxDropMenu();
        // ReSharper disable once VirtualMemberCallInConstructor
        ContextMenuStrip = Cms;
        Cms.CmdRename += (_,_)=>OnCmdRename(new CancelEventArgs());
        Cms.CmdClear += (_, _) => OnCmdClear(new CancelEventArgs());
        Cms.CmdCopy += (_, _) => OnCmdCopy(new CancelEventArgs());
        Cms.CmdPaste += (_, _) => OnCmdPaste(new CancelEventArgs());
        Cms.Opening += (_, _) => OnCmdOpening(new CancelEventArgs());
        
    }

    

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        if (_showOpenFile && _openRectangle.Contains(e.Location) && e.Button == MouseButtons.Left)
        {
            DoOpenFolder();
        }
        return;
        // Local function
        void DoOpenFolder()
            {
                var filter = ExtDescription.GetFilterES(AllowedExtensions);
                using var f = new OpenFileDialog();
                f.Filter = filter;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (NeedsContent)
                    {
                        var content = File.ReadAllBytes(f.FileName);
                        PutFile(f.FileName, content);
                    }
                    else
                        PutFile(f.FileName, null);
                }
            }
    }
    
    public void ClearDrop()
    {
        SetValue("", OriginalValue == "" ? DropControlStatus.Empty : DropControlStatus.Clear);
        OnValueChanged();
    }
    
    protected virtual Bitmap GetIcon()
    {
        return Status switch
        {
            DropControlStatus.Empty=>ArtClass.EmptyIcon,
            DropControlStatus.Original=>ArtClass.CertIcon,
            DropControlStatus.New=>ArtClass.BurstIcon,
            DropControlStatus.Error=>ArtClass.AttentionIcon,
            DropControlStatus.Clear=> ArtClass.DeleteIcon,
            _ => ArtClass.AttentionIcon
        };
    }
    protected virtual bool IsExtensionAllowed(string ext)
    {
        if (AllowedExtensionsSet.Contains(ext)) return true;
        return false;
    }
    protected virtual ColorExtension GetColorExtension()
    {
        if (Status==DropControlStatus.Empty) return new ColorExtension("Ø", Color.Gray, Color.White);
        if (Status==DropControlStatus.Original) return new ColorExtension("=", Color.Gray, Color.White);
        if (Status==DropControlStatus.Clear) return new ColorExtension("X", Color.Gray, Color.White);
        if (Status==DropControlStatus.Error) return new ColorExtension("!", Color.Red, Color.White);
        var ext = GetExtension().ToLower();
        if (ext.StartsWith(".")) ext= ext.Substring(1);
        if (!IsExtensionAllowed(ext)) return new ColorExtension("!"+ext,Color.Red, Color.Yellow);
        var ed = ExtDescription.GetDescriptionES(ext);
        return new ColorExtension(ed.Extension, ed.Color, Color.White);
    }

    
    private Rectangle _openRectangle;
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var r = new Rectangle(Point.Empty,Size);
        var r1 = (_showOpenFile)
            ? new Rectangle(r.Location, new Size(r.Width - DropBoxWidth - 19 - 1, r.Height))
            : new Rectangle(r.Location, new Size(r.Width - DropBoxWidth - 1, r.Height));
        var r2 = new Rectangle(r.Width - DropBoxWidth, 0, DropBoxWidth, r.Height);
        var r3= new Rectangle(r.Width - DropBoxWidth-19, 0, 18, r.Height - 1);
        _openRectangle = r3;
        e.Graphics.FillRectangle(new SolidBrush(BackColor), r1);
        var dpen = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dash };
        e.Graphics.DrawRectangle(dpen, r1.DeltaSize(-1, -1));
        var rs = r1.DeltaPos(2, 2).DeltaSize(-4, -4);
        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        var bs = FontsEx.FontHelper.MeasureTextSizeWithAscent(e.Graphics, Font, "X");
        var sf = new StringFormat(StringFormatFlags.NoWrap);
        e.Graphics.DrawString(Value, Font, new SolidBrush(ForeColor), rs.DeltaWidth(-16),sf);
        var ico=GetIcon();
        if (ico!=null && !HideValidIcon)
        {
            var p = new Point(rs.Right - 20, 2+(int)((bs.Height-16)/2));
            //e.Graphics.DrawIcon(ico,new DuoToneColor(Color.Black,Color.Tomato), p.X, p.Y, 16);
            e.Graphics.DrawImage(ico, new Rectangle(p,new Size(16,16)),new Rectangle(0,0,ico.Width,ico.Height),GraphicsUnit.Pixel);
        }
        if (_showOpenFile)
        {
            var ico2 = ArtClass.OpenFolderIcon;
            e.Graphics.DrawRectangle(dpen, r3.DeltaSize(-1, 0));
            var x=r3.Left+1;
            e.Graphics.DrawImage(ico2,x, r3.Y+1);
        }
        
        var c = GetColorExtension();
        e.Graphics.FillRectangle(new SolidBrush(c.Background), r2);
        e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
        e.Graphics.DrawCenterString(
            c.Extension,
            new Font(Font.FontFamily, Font.SizeInPoints + 1f, FontStyle.Bold, GraphicsUnit.Point),
            new SolidBrush(c.Foreground), r2.DeltaPos(0, 1).DeltaSize(0, 2));
        e.Graphics.DrawRectangle(new Pen(Color.Black), r2.DeltaSize(-1, -1));
    }
    public override Size GetPreferredSize(Size proposedSize)
    {
        var size = base.GetPreferredSize(proposedSize);
        size.Height =2+ (int)Font.GetHeight(this.CreateGraphics());
        return size;
    }
}