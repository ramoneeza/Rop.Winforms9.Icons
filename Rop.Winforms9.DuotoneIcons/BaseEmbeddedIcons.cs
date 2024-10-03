using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rop.Winforms9.DuotoneIcons;

/// <summary>
/// Base class for embedded icons
/// </summary>
public abstract partial class BaseEmbeddedIcons : IEmbeddedIcons
{
    public IReadOnlyList<string> Codes { get; }
    public IReadOnlyList<string> Aliases { get; }
    public string FontName { get; }
    public Size BaseSize { get; }
    public int Count => _charSet.Count;
    public DuoToneIcon? GetIcon(string name) => _charSet.GetValueOrDefault(name);
    public float DrawIcon(Graphics gr, string code, DuoToneColor iconcolor, float x, float y, float height)
    {
        var icon = GetIcon(code);
        if (icon == null) return 0;
        return gr.DrawIcon(icon,iconcolor, x, y, height);
    }
    public void DrawIconFit(Graphics gr, string code, DuoToneColor iconcolor, float x, float y, float size)
    {
        var icon = GetIcon(code);
        if (icon == null) return;
        gr.DrawIconFit(icon,iconcolor, x, y, size);
    }
    public float DrawIconBaseLine(Graphics gr, string code, DuoToneColor iconcolor, float x, float baseline, float height)
    {
        var icon= GetIcon(code);
        if (icon == null) return 0;
        return gr.DrawIconBaseline(icon, iconcolor, x, baseline, height);
    }
    protected BaseEmbeddedIcons(string resourcename)
    {
        var jsonbankdata = GetZipResource(GetType(), resourcename);
        if (jsonbankdata == null) throw new ArgumentException($"Resource {resourcename} not found");
        // decodifica jsoncodes que es un string json que representa un array de baseicon
        var bankData = JsonSerializer.Deserialize<BankData>(jsonbankdata);
        if (bankData == null) throw new ArgumentException($"Resource {resourcename} not deserialized");
        FontName = bankData.Name;
        var charSet = new Dictionary<string, DuoToneIcon>(StringComparer.OrdinalIgnoreCase);
        var alias = new List<string>();
        var codes = new List<string>();
        BaseSize = bankData.BaseSize;
        foreach (var resourceIcon in bankData.Icons)
        {
            var sz = new Size(resourceIcon.Width, resourceIcon.Height);
            var icon = new DuoToneIcon(resourceIcon.Code, sz, resourceIcon.BaseLine, resourceIcon.Data);
            charSet[resourceIcon.Code] = icon;
            alias.AddRange(resourceIcon.Alias);
            codes.Add(resourceIcon.Code);
            foreach (var a in resourceIcon.Alias)
            {
                charSet[a] = icon;
            }
        }
        _charSet = charSet.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);
        Codes = codes.OrderBy(i => i).ToList();
        Aliases = alias.OrderBy(a => a).ToList();
    }
}