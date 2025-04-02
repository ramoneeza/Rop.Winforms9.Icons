using System.ComponentModel;

namespace Rop.Winforms9.DropControls;
internal partial class Dummy{}
public class FileContentDropControl : FileDropControl{
    
    protected override bool NeedsContent => true;
    protected override void DefaultCopy()
    {
        var file = Value;
        if (string.IsNullOrEmpty(file)) return;
        var ext = Path.GetExtension(file).ToLower();
        switch (ext)
        {
            case ".png":
            case ".bmp":
            case ".jpg":
                Clipboard.SetImage(Image.FromFile(file));
                break;
            default:
               Clipboard.SetText(file);
                break;
        }
    }

   

    public FileContentDropControl()
    {
    }
    public override void PutFile(string file, byte[]? ms,bool iserror=false)
    {
        if (ms == null)
        {
            SetValue(file,DropControlStatus.Error);
        }
        else
        {
            var st= (iserror) ? DropControlStatus.Error : DropControlStatus.New;
            SetValue(file,st);
            NewData = ms;
        }
        Invalidate();
        OnValueChanged();
    }
    
}