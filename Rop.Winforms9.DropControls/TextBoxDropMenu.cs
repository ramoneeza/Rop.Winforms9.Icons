using System.ComponentModel;

namespace Rop.Winforms9.DropControls;
internal partial class Dummy{}
public class TextBoxDropMenu : ContextMenuStrip
{
    public event EventHandler? CmdRename;
    public event EventHandler? CmdClear;
    public event EventHandler? CmdPaste;
    public event EventHandler? CmdCopy;
    public event EventHandler? CmdEdit;
    public readonly ToolStripMenuItem ToolEdit = new();
    private readonly ToolStripSeparator _barraEdit = new();
    public readonly ToolStripMenuItem ToolCopiar = new();
    public readonly ToolStripMenuItem ToolPegar = new();
    private readonly ToolStripSeparator _toolStripSeparator1 = new();
    public readonly ToolStripMenuItem ToolClear = new();
    public readonly ToolStripMenuItem ToolRename = new();
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Disabled => !Enabled;

    [DefaultValue(false)]
    public bool CanRename
    {
        get => ToolRename.Visible;
        set=> ToolRename.Visible = value;
    }
    [DefaultValue(false)]
    public bool CanEdit
    {
        get => ToolEdit.Visible;
        set
        {
            ToolEdit.Visible = value; 
            _barraEdit.Visible= value;
        }
    }
    [DefaultValue(false)]
    public bool CanCopy
    {
        get => ToolCopiar.Enabled;
        set=> ToolCopiar.Enabled = value;
    }
    [DefaultValue(false)]
    public bool CanPaste
    {
        get => ToolPegar.Visible;
        set=> ToolPegar.Visible = value;
    }
    [DefaultValue(false)]
    public bool CanClear
    {
        get => ToolClear.Enabled;
        set=> ToolClear.Enabled = value;
    }
    private void _setwinv<T>(ref T field, T value)
    {
        if (object.Equals(field,value)) return;
        field = value;
        Invalidate();
    }
    private const string _copyicon= "ContentCopy";
    private const string _pasteicon = "ContentPaste";
    private const string _clearicon = "Crossthin";
    private const string _renameicon = "FormTextbox";
    private const string _editicon = "ImageEditOutline";

    private void AjIcons()
    {
        var color = Color.Black;
        //ToolEdit.Image =ArtClass.  Icons?.GetImage(_editicon,color,16);
        ToolCopiar.Image = ArtClass.ClipboardCopyIconBlack;
        ToolPegar.Image = ArtClass.ClipboardPastIconBlack;
        ToolClear.Image = ArtClass.DeleteIconBlack;
        ToolRename.Image = ArtClass.Rename;
    }

    public TextBoxDropMenu()
    {
        
        // ReSharper disable once VirtualMemberCallInConstructor
        Items.AddRange(new ToolStripItem[] {ToolEdit,_barraEdit, ToolCopiar, ToolPegar, ToolRename, _toolStripSeparator1, ToolClear });
        Size = new Size(153, 120);
        ToolEdit.Name = "toolEdit";
        ToolEdit.Size = new Size(152, 22);
        ToolEdit.Text = "Editar";
        ToolEdit.Click += (o, e) => CmdEdit?.Invoke(o, e);
        // 
        // toolCopiar
        // 
        ToolCopiar.Name = "toolCopiar";
        ToolCopiar.Size = new Size(152, 22);
        ToolCopiar.Text = "Copiar";
        ToolCopiar.Click += (o, e) => CmdCopy?.Invoke(o, e);
        //ToolCopiar.Image = Rop.Winforms8.Eeza.Properties.Resources.clipboard_copy_icon_16_black;
        // 
        // toolPegar
        // 
        ToolPegar.Name = "toolPegar";
        ToolPegar.Size = new Size(152, 22);
        ToolPegar.Text = "Pegar";
        ToolPegar.Click += (o, e) => CmdPaste?.Invoke(o, e);
        //ToolPegar.Image = Rop.Winforms8.Eeza.Properties.Resources.clipboard_past_icon16_black;
        // 
        // toolStripSeparator1
        // 
        _toolStripSeparator1.Name = "toolStripSeparator1";
        _toolStripSeparator1.Size = new Size(149, 6);
        // 
        // toolClear
        // 
        ToolClear.Name = "toolClear";
        ToolClear.Size = new Size(152, 22);
        ToolClear.Text = "Borrar";
        ToolClear.Click += (o, e) => CmdClear?.Invoke(o, e);
        //ToolClear.Image = Rop.Winforms8.Eeza.Properties.Resources.delete_icon16_black;

        ToolRename.Name = "toolRenombrar";
        ToolRename.Size = new Size(152, 22);
        ToolRename.Text = "Renombrar";
        ToolRename.Click += (o, e) => CmdRename?.Invoke(o, e);
        //ToolRename.Image = Rop.Winforms8.Eeza.Properties.Resources.rename_16;
        AjIcons();
    }
}