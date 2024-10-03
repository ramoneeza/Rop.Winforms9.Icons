using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Rop.Winforms9.DuotoneIcons;

public static class IconRepository
{
    private static readonly ConcurrentDictionary<string, IEmbeddedIcons> _loadedIconsByName = new(StringComparer.OrdinalIgnoreCase);
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEmbeddedIcons> _loadedIconsByType = new();
    public static IEmbeddedIcons? DefaultBank { get; private set; }
    public static IEmbeddedIcons GetEmbeddedIcons<T>() where T : IEmbeddedIcons
    {
        var t = typeof(T);
        return GetEmbeddedIcons(t);
    }
    public static IEmbeddedIcons GetEmbeddedIcons(Type t)
    {
        if (_loadedIconsByType.TryGetValue(t.TypeHandle, out var bank)) return bank;
        bank = (IEmbeddedIcons)(Activator.CreateInstance(t) ?? throw new NullReferenceException());
        _loadedIconsByType[t.TypeHandle] = bank;
        _loadedIconsByName[bank.FontName] = bank;
        if (DefaultBank is not { FontName: "MaterialDesign" }) DefaultBank = bank;
        return bank;
    }
    public static IEmbeddedIcons? GetEmbeddedIcons(string name)
    {
        _loadedIconsByName.TryGetValue(name, out var bank);
        return bank;
    }
    public static bool IsLoaded(string name)
    {
        return _loadedIconsByName.ContainsKey(name);
    }

    public static string[] GetRegisteredIcons()
    {
        return _loadedIconsByName.Keys.ToArray();
    }
}
