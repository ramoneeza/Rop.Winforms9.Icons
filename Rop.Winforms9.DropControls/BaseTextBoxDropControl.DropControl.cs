using System.Collections.Frozen;
using System.ComponentModel;
using Rop.Results9;

namespace Rop.Winforms9.DropControls;

internal partial class Dummy{}
public abstract partial class BaseTextBoxDropControl
{
    
    protected abstract bool NeedsContent { get; }
    private string _value = "";
    private DropControlStatus _status = DropControlStatus.Empty;
    protected FrozenSet<string> AllowedExtensionsSet { get; private set; } = ["pdf"];

   
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Value
    {
        get => _value;
        set => _value = value;
    }
    protected void SetValue(string newvalue, DropControlStatus newstatus)
    {
        _value = newvalue;
        _status = newstatus;
        Invalidate();
    }
    
    public abstract void PutFile(string file, byte[]? ms = null, bool iserror = false);
    protected virtual bool IsEmpty => Value == "";
    protected virtual string GetExtension() => GetExtension(Value);
    protected virtual string GetExtension(string value)
    {
        var ext = Path.GetExtension(value).ToLower();
        if (ext.StartsWith('.')) ext = ext[1..];
        return ext;
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // ReSharper disable once InconsistentNaming
    private new bool AllowDrop => base.AllowDrop;


    public virtual Result<DropItem> GetDropItem(IDataObject data)
    {
        var service = new DropItemsService(AllowedDropTypes);
        var files = service.CreateDropItemsFromDropData(data);
        var res = FileDropped(files);
        if (res.IsFailed) return res;
        if (!IsDropAllowed(res.Value!)) return Error.Fail("No es un tipo permitido");
        return res;
    }

    protected override void OnDragEnter(DragEventArgs drgevent)
    {
        if (drgevent.Data is null)
        {
            drgevent.Effect = DragDropEffects.None;
            return;
        }
        var data = drgevent.Data;
        var formats = data.GetFormats().Select(f => (f, data.GetDataPresent(f, false))).ToArray();
        var strformat = data.GetData("System.String", true);

        var rdropitem = GetDropItem(data);
        if (rdropitem.IsFailed)
        {
            drgevent.Effect = DragDropEffects.None;
            return;
        }
        drgevent.Effect = DragDropEffects.Copy;
    }

    // ReSharper disable once AsyncVoidMethod
    protected override async void OnDragDrop(DragEventArgs drgevent)
    {
        var data = drgevent.Data;
        if (data == null) return;

        var rdropitem = GetDropItem(data);
        if (rdropitem.IsFailed) return;
        var file = rdropitem.Value!;

        var finalfile = GetFinalFileName(file);
        if (string.IsNullOrEmpty(finalfile)) return;
        if (NeedsContent)
        {
            var finaldata = await GetFinalData(file, finalfile);
            if (finaldata != null)
                PutFile(finalfile, finaldata);
            else
                PutFile(finalfile, null, true);
        }
        else
        {
            PutFile(finalfile, null);
        }
    }
    public string GetFinalFileName(DropItem file)
    {
        var finalfile = file.FileName;
        if (finalfile.Contains(@"\")) finalfile = Path.GetFileName(finalfile);
        if (finalfile.Contains(@"/")) finalfile = Path.GetFileName(finalfile);
        if (finalfile.Contains(":")) finalfile = Path.GetFileName(finalfile);
        return finalfile;
    }
    public async Task<byte[]?> GetFinalData(DropItem file, string finalfile)
    {
        byte[]? data = null;
        switch (file.DropType)
        {
            case DropTypes.FileDrop:
                data = File.ReadAllBytes(file.FileName);
                break;

            case DropTypes.FileGroupDescriptor:
                data = File.ReadAllBytes(file.FileName);
                break;

            case DropTypes.HasUrlDrop:
                data = file.FileName.StartsWith("http")
                    ? await CaptureWeb.GetInternetFile(file.FileName)
                    : await CaptureWeb.GetChromeUrl(Path.GetFileName(file.FileName));
                break;

            case DropTypes.HtmlImage:
                data = file.FileName.StartsWith("http")
                    ? await CaptureWeb.GetInternetFile(file.FileName)
                    : await CaptureWeb.GetChromeUrl(Path.GetFileName(file.FileName));
                if (data != null && file.FileName.EndsWith(".svg"))
                {
                    var finaldata = CanConvertFromSvg(data);
                    if (finaldata != null) data = finaldata;
                }
                break;
        }
        return data;
    }
    protected virtual bool IsDropAllowed(DropItem item)
    {
        if (item.DropType == DropTypes.HasUrlDrop) return true;
        var ext = GetExtension(item.FileName);
        var allowed = IsExtensionAllowed(ext);
        return allowed;
    }
    protected virtual int DefaultImageHeight { get; } = 100;
    protected virtual byte[]? CanConvertFromSvg(byte[] data)
    {
        var sz = DefaultImageHeight;
        var bmp = SvgHelper.SvgToPng(data, sz);
        return bmp;
    }

    protected virtual Result<DropItem> FileDropped(List<DropItem> files)
    {
        foreach (var allowedDropType in AllowedDropTypes)
        {
            var fd = files.Where(f => f.DropType == allowedDropType).ToList();
            if (fd.Count == 1) return fd[0];
            if (fd.Count > 1) return Error.Fail("Demasiados candidatos");
        }
        return Error.Null("No hay candidatos");
    }
    protected virtual void OnValueChanged()
    {
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }
}