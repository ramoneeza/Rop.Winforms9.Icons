using System.ComponentModel;

namespace Rop.Winforms9.DropControls;

public class ExtensionAllowedQueryArgs : EventArgs
{
    public string Extension { get; }
    public bool Allowed { get; set; }
    public ExtensionAllowedQueryArgs(string extension)
    {
        Extension = extension;
        Allowed = false;
    }
}

public class FileDropControl : BaseTextBoxDropControl{
    
    protected override bool NeedsContent => false;
    public event EventHandler<ExtensionAllowedQueryArgs>? ExtensionAllowedQuery;
    protected override bool IsExtensionAllowed(string ext)
    {
        var b=base.IsExtensionAllowed(ext);
        if (b) return true;
        var args=new ExtensionAllowedQueryArgs(ext);
        ExtensionAllowedQuery?.Invoke(this, args);
        return args.Allowed;
    }
    public override void PutFile(string file, byte[]? ms=null,bool iserror=false)
    {
        var st=(iserror)? DropControlStatus.Error : DropControlStatus.New;
        SetValue(file,st);
        OnValueChanged();
    }
    protected override void DefaultCopy()
    {
        Clipboard.SetText(Value);
    }

    protected override void OnCmdOpening(CancelEventArgs e)
    { 
        Cms.CanCopy= !IsEmpty;
        Cms.CanRename = (Status==DropControlStatus.New||Status==DropControlStatus.Original) && ShowRenameTool;
        Cms.CanClear = !IsEmpty;
        Cms.CanEdit = ShowEditTool && !IsEmpty;
    }
    protected override void DefaultRename()
    {
        //var fn=Value;
        //var ext = Path.GetExtension(fn);
        //var file= Path.GetFileNameWithoutExtension(fn);
        //var r = FormRenombrar.Execute(this, file);
        //if (r.IsFailed) return;
        //var v = r.Value!;
        //Value = r.Value + ext;
    }
}