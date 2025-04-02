using System.Collections.Frozen;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Rop.Results9;

namespace Rop.Winforms9.DropControls;
internal partial class Dummy{}
public abstract class DropControl :Control
{
    public event EventHandler? ValueChanged;
    protected abstract bool NeedsContent { get;}

    protected DropControl() : base()
    {
        base.AllowDrop = true;
    }
    private string _value="";
    private DropControlStatus _status = DropControlStatus.Empty;

    protected FrozenSet<string> SetAllowedExtensions { get; private set; } = ["pdf"];
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual string[] AllowedExtensions
    {
        get => SetAllowedExtensions.ToArray();
        set => SetAllowedExtensions = value.ToFrozenSet(StringComparer.OrdinalIgnoreCase);
    }
    
    
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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DropControlStatus Status
    {
        get => _status;
        protected set => _status = value;
    }

    private string _originalValue="";
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string OriginalValue
    {
        get => _originalValue;
        set
        {
            _originalValue = value; 
            _value= value;
            _status=(value=="")?DropControlStatus.Empty:DropControlStatus.Original;
            Invalidate();
        }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public byte[]? NewData { get; protected set; }

    private byte[]? _originalData;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public byte[]? OriginalData
    {
        get => _originalData;
        set
        {
            _originalData = value; 
            NewData = value;
            Invalidate();
        }
    }
    public abstract void ClearDrop();
    public abstract void PutFile(string file, byte[]? ms=null,bool iserror=false);
    
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
    // ReSharper disable once ValueParameterNotUsed
    public override bool AllowDrop { get => base.AllowDrop; set => base.AllowDrop = true; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DropTypes[] AllowedDropTypes { get; set; } = [DropTypes.FileDrop,DropTypes.FileGroupDescriptor];
    protected override void OnDragEnter(DragEventArgs drgevent)
    {
        if (drgevent.Data is null)
        {
            drgevent.Effect= DragDropEffects.None;
            return;
        }
        var data =drgevent.Data;
        var formats = data.GetFormats().Select(f=>(f,data.GetDataPresent(f,false))).ToArray();
        var strformat = data.GetData("System.String", true);

        var files = CreateDropItemsFromDropData(data);
        var res = FileDropped(files);
        if (res.IsFailed)
        {
            drgevent.Effect = DragDropEffects.None;
            return;
        }
        if (!IsDropAllowed(res.Value!))
        {
            drgevent.Effect = DragDropEffects.None;
            return;
        }
        drgevent.Effect = DragDropEffects.Copy;
    }

    protected override async void OnDragDrop(DragEventArgs drgevent)
    {
        var data=drgevent.Data;
        if (data==null) return;
        var files = CreateDropItemsFromDropData(data);
        var rfile = FileDropped(files);
        if (rfile.IsFailed) return;
        var file = rfile.Value!;
        if (!IsDropAllowed(file)) return;
        switch (file.DropType)
        {
            case DropTypes.FileDrop:
                if (NeedsContent)
                {
                    try
                    {
                        var finaldata = File.ReadAllBytes(file.FileName);
                        PutFile(file.FileName, finaldata);
                    }
                    catch (Exception ex)
                    {
                        PutFile(file.FileName, null, true);
                        Debug.Print(ex.Message);
                    }
                }
                else
                {
                    PutFile(file.FileName);
                }

                return;
            case DropTypes.FileGroupDescriptor:
                if (NeedsContent)
                {
                    try
                    {
                        var finaldata = File.ReadAllBytes(file.FileName);
                        PutFile(file.FileName, finaldata);
                    }
                    catch (Exception ex)
                    {
                        PutFile(file.FileName, null, true);
                        Debug.Print(ex.Message);
                    }
                }
                else
                {
                    PutFile(file.FileName);
                }

                break;
            case DropTypes.HasUrlDrop:
                if (NeedsContent)
                {
                    try
                    {
                        var filedata = file.FileName.StartsWith("http")
                            ? await CaptureWeb.GetInternetFile(file.FileName)
                            : await CaptureWeb.GetChromeUrl(Path.GetFileName(file.FileName));
                        if (filedata != null) PutFile(file.FileName, filedata);
                    }
                    catch (Exception ex)
                    {
                        PutFile(file.FileName, null, true);
                        Debug.Print(ex.Message);
                    }
                }
                else
                {
                    PutFile(file.FileName);
                }

                break;
            case DropTypes.HtmlImage:
                try
                {
                    var filedata = file.FileName.StartsWith("http")
                        ? await CaptureWeb.GetInternetFile(file.FileName)
                        : await CaptureWeb.GetChromeUrl(Path.GetFileName(file.FileName));
                    var finalfile = Path.GetFileName(file.FileName);
                    if (filedata != null)
                    {
                        if (!finalfile.EndsWith(".svg"))
                        {
                            PutFile(finalfile, filedata);
                        }
                        else
                        {
                            finalfile = finalfile.Replace(".svg", ".png");
                            var finaldata = CanConvertFromSvg(filedata);
                            if (finaldata!= null)
                            {
                                PutFile(finalfile, finaldata);
                            }
                            else
                            {
                                PutFile(finalfile, null, true);
                            }
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    PutFile(file.FileName, null, true);
                    Debug.Print(ex.Message);
                }

                break;
        }
    }
    protected virtual byte[]? CanConvertFromSvg(byte[] data)
    {
        return null;
    }
    protected virtual bool IsDropAllowed(DropItem item)
    {
        if (item.DropType == DropTypes.HasUrlDrop) return true;
        var ext = GetExtension(item.FileName);
        var allowed= IsExtensionAllowed(ext);
        return allowed;
    }
    
    protected virtual bool IsExtensionAllowed(string ext)
    {
        if (SetAllowedExtensions.Contains(ext)) return true;
        return false;
    }
    protected virtual List<DropItem> CreateDropItemsFromDropData(IDataObject data)
    {
        var res = new List<DropItem>();
        foreach (var allowedDropType in AllowedDropTypes)
        {
            res.AddRange(Get(allowedDropType).Where(IsDropAllowed));
        }
        return res;
        List<DropItem> Get(DropTypes type)
        {
            try
            {
                object? content = null;
                switch (type)
                {
                    case DropTypes.FileDrop:
                        var files = data.GetDataAsStrings(DataFormats.FileDrop);
                        if (files == null) return [];
                        return files.Select(f => new DropItem(f, DropTypes.FileDrop)).ToList();
                    case DropTypes.FileGroupDescriptor:
                        var fgd = data.GetDataAsStructure<InternalOleDataObject.FILEGROUPDESCRIPTORW>("FileGroupDescriptorW");
                        if (fgd == null) return [];
                        return fgd.Value.fgd.Select(f => new DropItem(f.cFileName, DropTypes.FileGroupDescriptor)).ToList();
                    case DropTypes.HasUrlDrop:
                        var onefile= data.GetDataAsString("System.String");
                        if (string.IsNullOrEmpty(onefile)|| !onefile.StartsWith("http")) return [];
                        return [new DropItem(onefile, DropTypes.HasUrlDrop)];
                     case DropTypes.HtmlImage:
                        var htmlstr = data.GetDataAsString("text/HTML");
                        if (htmlstr!=null && htmlstr.Contains("<img"))
                        {
                            var onefile2 = GetUrlFromHtmlImg(htmlstr);
                            if (string.IsNullOrEmpty(onefile2)) return [];
                            return [new DropItem(onefile2, DropTypes.HtmlImage)];
                        }
                        else
                        {
                            return [];
                        }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return [];
            }
            return [];
            string GetUrlFromHtmlImg(string html)
            {
                var i1 = html.IndexOf("src=\"", StringComparison.OrdinalIgnoreCase);
                if (i1 < 0) return "";
                i1 += 5;
                var i2 = html.IndexOf('"', i1);
                if (i2 < 0) return "";
                return html[i1..i2];
            }

        }
        bool IsRootedOrStripped(string f)
        {
            if (f.Contains("//")||f.Contains(@"\\") || f.Contains(@":\")) return true; // Marcas de Root vale
            if (f.Contains("/") || f.Contains(@"\")) return false; // Marcas de directorio sin root no vale;
            if (f.EndsWith(".url")) return false; // url files are not allowed
            if (f.Contains((char)0)) return false; // No nulls
            return true;
        }
    }
    protected virtual Result<DropItem> FileDropped(List<DropItem> files)
    {
        foreach (var allowedDropType in AllowedDropTypes)
        {
            var fd = files.Where(f=>f.DropType==allowedDropType).ToList();
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