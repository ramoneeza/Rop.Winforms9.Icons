using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.Windows9.Bitmaps;

namespace Rop.Winforms9.DropControls;

public abstract partial class BaseTextBoxDropControl
{
    protected virtual void OnCmdEdit(CancelEventArgs e)
    {
        CmdEdit?.Invoke(this,e);
    }
    protected virtual void DefaultEdit()
    {
    }
    protected void OnCmdPaste(CancelEventArgs e)
    {
        CmdPaste?.Invoke(this, e);
        if (e.Cancel) return;
        DefaultPaste();
    }

    protected virtual void DefaultPaste()
    {
        if (IsImageFile)
        {
            _pasteImage();
        }
        else
        {
            _pasteFile();
        }
        return;
        // Local function
        void _pasteFile()
        {
            if (!Clipboard.ContainsFileDropList()) return;
            var lst = Clipboard.GetFileDropList();
            if (lst.Count != 1) return;
            var file = lst[0] ?? "";
            if (NeedsContent)
            {
                try
                {
                    var content = File.ReadAllBytes(file);
                    PutFile(file, content);
                }
                catch (Exception ex)
                {
                    PutFile(file, null, true);
                }
            }
            else
            {
                PutFile(file, null);
            }
        }
        void _pasteImage()
        {
            if (!Clipboard.ContainsImage()) return;
            var img = Clipboard.GetImage();
            if (img == null) return;
            var ext = img.RawFormat.ToExtension();
            if (ext == "Mbmp" || ext == "") ext = "png";
            var data = img.ImageToBytes(ImageHelper.GetImageFormat(ext));
            var filename = $"{DateTime.Now:yyyyMMdd-HHmm}-Captura.{ext}";
            PutFile(filename, data);
        }
    }

    protected virtual void DefaultCopy(){}

    protected virtual void OnCmdCopy(CancelEventArgs e)
    {
        CmdCopy?.Invoke(this, e);
        if (e.Cancel) return;
        DefaultCopy();
    }
    
    
    protected virtual void OnCmdClear(CancelEventArgs e)
    {
        CmdClear?.Invoke(this, e);
        if (e.Cancel) return;
        ClearDrop();
    }    
    protected virtual void OnCmdRename(CancelEventArgs e) {
        CmdRename?.Invoke(this,e);
        if (e.Cancel) return;
        DefaultRename();
    }
    protected virtual void DefaultRename()
    {
        
    }
    protected readonly TextBoxDropMenu Cms;
    protected abstract void OnCmdOpening(CancelEventArgs e);
    
    
    
}