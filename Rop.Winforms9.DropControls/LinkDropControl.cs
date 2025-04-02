using System.ComponentModel;

namespace Rop.Winforms9.DropControls;
internal partial class Dummy{}
public class LinkDropControl : BaseTextBoxDropControl{
    [Browsable(false)]
    protected override bool IsExtensionAllowed(string ext)
    {
        return ext=="http";
    }
    public LinkDropControl()
    {
        AllowedDropTypes = [DropTypes.HasUrlDrop];
    }
    protected override bool NeedsContent => false;
    public override void PutFile(string file, byte[]? ms,bool iserror=false)
    {
        var ext= GetExtension(file);
        var allowed = IsExtensionAllowed(ext);
        var st = iserror ? DropControlStatus.Error : allowed ? DropControlStatus.New : DropControlStatus.Error;
        SetValue(file,st);
    }
    protected override void DefaultCopy()
    {
        Clipboard.SetText(Value);
    }
    protected override void DefaultPaste()
    {
        if (Clipboard.ContainsFileDropList())
        {
            var lst = Clipboard.GetFileDropList();
            if (lst.Count != 1) return;
            var file = lst[0] ?? "";
            var ext= GetExtension(file);
            if (IsExtensionAllowed(ext))
                PutFile(file, null);
        }
        if (Clipboard.ContainsText())
        {
            var txt = Clipboard.GetText();
            // Check if it is a valid URL
            if (Uri.TryCreate(txt, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                PutFile(txt, null);
            }
        }
    }
    protected override void OnCmdOpening(CancelEventArgs e)
    { 
        Cms.CanCopy = !IsEmpty;
        Cms.CanClear = !IsEmpty;
        Cms.CanPaste = true;
        Cms.CanRename = false;
        Cms.CanEdit = false;
    }
}