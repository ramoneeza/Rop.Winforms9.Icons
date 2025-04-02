using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Rop.Winforms9.DropControls;

public record DropItem(string FileName, DropTypes DropType);
public enum DropTypes
{
    FileDrop,
    FileGroupDescriptor,
    HasUrlDrop,
    HtmlImage
}

public class DropItemsService
{
    public IReadOnlyList<DropTypes> AllowedDropTypes { get; init; }
    public DropItemsService(params IReadOnlyList<DropTypes> allowedDropTypes)
    {
        AllowedDropTypes = allowedDropTypes;
    }
    public List<DropItem> CreateDropItemsFromDropData(IDataObject data)
    {
        var res = new List<DropItem>();
        foreach (var allowedDropType in AllowedDropTypes)
        {
            switch (allowedDropType)
            {
                case DropTypes.FileDrop:
                    res.AddRange(GetFileDropItems(data));
                    break;
                case DropTypes.FileGroupDescriptor:
                    res.AddRange(GetFileGroupDescriptorItems(data));
                    break;
                case DropTypes.HasUrlDrop:
                    var item1 = GetHasUrlDropItems(data);
                    if (item1!=null) res.Add(item1);
                    break;
                case DropTypes.HtmlImage:
                    var item2= GetHtmlImageItem(data);
                    if (item2!= null) res.Add(item2);
                    break;
            }
        }
        return res;
    }
    public List<DropItem> GetFileDropItems(IDataObject data)
    {
        var files = data.GetDataAsStrings(DataFormats.FileDrop);
        if (files == null) return [];
        return files.Select(f => new DropItem(f, DropTypes.FileDrop)).ToList();
    }
    public List<DropItem> GetFileGroupDescriptorItems(IDataObject data)
    {
        var res = new List<DropItem>();
        var sm = data.GetData("FileGroupDescriptorW", true) as MemoryStream;
        if (sm == null) return [];
        using var br = new BinaryReader(sm);
        var numitems = br.ReadInt32();
        var msize = Marshal.SizeOf<InternalOleDataObject.FILEDESCRIPTORW>();
        for (var f = 0; f < numitems; f++)
        {
            try
            {
                var byteitems = br.ReadBytes(msize);
                var fd = InternalOleDataObject.ByteArrayToStructure<InternalOleDataObject.FILEDESCRIPTORW>(byteitems);
                var item = new DropItem(fd.cFileName, DropTypes.FileGroupDescriptor);
                res.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        return res;
    }

    public DropItem? GetHasUrlDropItems(IDataObject data)
    {
        var onefile= data.GetDataAsString("System.String");
        if (string.IsNullOrEmpty(onefile)|| !onefile.StartsWith("http")) return null;
        return new DropItem(onefile, DropTypes.HasUrlDrop);
    }
    public DropItem? GetHtmlImageItem(IDataObject data)
    {
        var htmlstr = data.GetDataAsString("text/HTML");
        if (htmlstr != null && htmlstr.Contains("<img"))
        {
            var onefile2 = GetUrlFromHtmlImg(htmlstr);
            if (string.IsNullOrEmpty(onefile2)) return null;
            return new DropItem(onefile2, DropTypes.HtmlImage);
        }
        else
        {
            return null;
        }
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
    public bool IsRootedOrStripped(string f)
    {
        if (f.Contains("//")||f.Contains(@"\\") || f.Contains(@":\")) return true; // Marcas de Root vale
        if (f.Contains("/") || f.Contains(@"\")) return false; // Marcas de directorio sin root no vale;
        if (f.EndsWith(".url")) return false; // url files are not allowed
        if (f.Contains((char)0)) return false; // No nulls
        return true;
    }
}