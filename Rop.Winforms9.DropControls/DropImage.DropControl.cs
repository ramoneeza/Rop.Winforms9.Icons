using System.Collections.Frozen;
using System.ComponentModel;
using System.Diagnostics;
using Rop.Results9;

namespace Rop.Winforms9.DropControls;

public partial class DropImage
{
    private DropControlStatus _status = DropControlStatus.Empty;
    protected FrozenSet<string> SetAllowedExtensions { get; private set; } = [];
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual string[] AllowedExtensions
    {
        get => SetAllowedExtensions.ToArray();
        set => SetAllowedExtensions = value.ToFrozenSet(StringComparer.OrdinalIgnoreCase);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DropControlStatus Status => _status;
    protected virtual string GetExtension(string value)
    {
        var ext = Path.GetExtension(value).ToLower();
        if (ext.StartsWith('.')) ext = ext[1..];
        return ext;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private new bool AllowDrop => base.AllowDrop;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DropTypes[] AllowedDropTypes { get; set; } = [DropTypes.FileDrop, DropTypes.FileGroupDescriptor, DropTypes.HasUrlDrop, DropTypes.HtmlImage];
    
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
        
        var finaldata = await GetFinalData(file, finalfile);
        if (finaldata != null)
            PutFile(finalfile, finaldata);
        else
            PutFile(finalfile, null, true);
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
                        var onefile = data.GetDataAsString("System.String");
                        if (string.IsNullOrEmpty(onefile) || !onefile.StartsWith("http")) return [];
                        return [new DropItem(onefile, DropTypes.HasUrlDrop)];

                    case DropTypes.HtmlImage:
                        var htmlstr = data.GetDataAsString("text/HTML");
                        if (htmlstr != null && htmlstr.Contains("<img"))
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
            if (f.Contains("//") || f.Contains(@"\\") || f.Contains(@":\")) return true; // Marcas de Root vale
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