using System.Collections.Frozen;
using System.IO.Compression;
using System.Text;

namespace Rop.Winforms9.DuotoneIcons;

public partial class BaseEmbeddedIcons
{
    private readonly FrozenDictionary<string, DuoToneIcon> _charSet;

    private static DuoToneIcon FactoryDuoToneIcon(string code, Size size, int baseline, byte[] data)
    {
        return new DuoToneIcon(code, size, baseline, data);
    }

    private static Stream? GetResourceStream(Type t, string name)
    {
        var assembly = t.Assembly;
        var resourceName = assembly.GetManifestResourceNames()
            .FirstOrDefault(str => str.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        return resourceName == null ? null : assembly.GetManifestResourceStream(resourceName);
    }

    private static string? GetZipResource(Type t, string name)
    {
        using var compressedstream = GetResourceStream(t, name);
        if (compressedstream is null) return null;
        using var gzipStream = new GZipStream(compressedstream, CompressionMode.Decompress);
        using var reader = new StreamReader(gzipStream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    private record BankData
    {
        public string Name { get; init; } = "";
        public string Initials { get; init; } = "";
        public Size BaseSize { get; init; } = new(0, 0);
        public IconData[] Icons { get; init; } = [];
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private record IconData
    {
        public string Code { get; init; } = "";
        public int Width { get; init; }
        public int Height { get; init; }
        public int BaseLine { get; init; } = 0;
        public byte[] Data { get; init; } = Array.Empty<byte>();
        public string[] Alias { get; init; } = Array.Empty<string>();
    }
}