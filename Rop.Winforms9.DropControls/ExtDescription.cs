using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DropControls
{
    public static class ExtDescription
    {
       private static readonly IReadOnlyList<(string descripcionES, string descripcionEN, string[] extensiones, Color colorDominante)> _archivetypes =
       [
    // Imágenes de mapa de bits
    ("Imágenes de mapa de bits", "Bitmap Images", ["bmp", "gif", "jpg", "jpeg", "png", "tiff", "webp"], Color.Orange),
    // Imágenes vectoriales
    ("Imágenes vectoriales", "Vector Images", ["svg", "eps", "ai"], Color.Purple),
    // Documentos de texto simples
    ("Documentos de texto", "Plain Text Documents", ["txt", "log", "md", "csv", "json", "xml", "yaml", "yml"], Color.Gray),
    // Documentos de Word
    ("Documentos de Word", "Word Documents", ["doc", "docx", "dot", "dotx", "rtf"], Color.Blue),
    // Hojas de cálculo de Excel
    ("Hojas de cálculo de Excel", "Excel Spreadsheets", ["xls", "xlsx", "xlsm"], Color.Green),
    // Presentaciones de PowerPoint
    ("Presentaciones de PowerPoint", "PowerPoint Presentations", ["ppt", "pptx", "pptm", "pps", "ppsx"], Color.OrangeRed),
    // Documentos PDF
    ("Documentos PDF", "PDF Documents", ["pdf"], Color.Red),
    // Archivos comprimidos
    ("Archivos comprimidos", "Compressed Files", ["zip", "rar", "7z", "tar", "gz", "bz2"], Color.Yellow),
    // Archivos de audio
    ("Archivos de audio", "Audio Files", ["mp3", "wav", "aac", "flac", "ogg", "m4a", "wma"], Color.LightGreen),
    // Archivos de vídeo
    ("Archivos de vídeo", "Video Files", ["mp4", "avi", "mkv", "mov", "wmv", "flv", "webm"], Color.LightBlue),
    // Archivos ejecutables
    ("Archivos ejecutables", "Executable Files", ["exe", "bat", "cmd", "msi", "sh"], Color.DarkGray),
    // Archivos de código fuente
    ("Archivos de código fuente", "Source Code Files", ["cs", "java", "py", "cpp", "c", "js", "ts", "html", "css", "php", "rb"
    ], Color.DarkGreen),
    // Archivos de diseño 3D
    ("Archivos de diseño 3D", "3D Design Files", ["obj", "stl", "fbx", "gltf", "glb"], Color.LightGray),
    // Archivos CAD
    ("Archivos CAD", "CAD Files", ["dxf", "dwg"], Color.DarkBlue),
    // Archivos Uri
    ("URI", "URI", ["uri","http","https"], Color.DarkBlue),
    // Todos los archivos
    ("Todos los archivos", "All Files", ["*"], Color.Black)];

    private static readonly IReadOnlyList<GroupExtensionDescription> _groupExtensionDescriptionsES =
           _archivetypes.Select(a => new GroupExtensionDescription(a.descripcionES, a.colorDominante, a.extensiones)).ToList();   
    private static readonly IReadOnlyList<GroupExtensionDescription> _groupExtensionDescriptionsEN =
        _archivetypes.Select(a => new GroupExtensionDescription(a.descripcionEN, a.colorDominante, a.extensiones)).ToList();   
    private static readonly FrozenDictionary<string,ExtensionDescription> _extensionDescriptionsES =
            _groupExtensionDescriptionsES.SelectMany(t => t.Extensions.Select(ext => new ExtensionDescription(ext, t.Description, t.Color)))
                .ToFrozenDictionary(t => t.Extension, StringComparer.OrdinalIgnoreCase);
    private static readonly FrozenDictionary<string,ExtensionDescription> _extensionDescriptionsEN =
        _groupExtensionDescriptionsEN.SelectMany(t => t.Extensions.Select(ext => new ExtensionDescription(ext, t.Description, t.Color)))
            .ToFrozenDictionary(t => t.Extension, StringComparer.OrdinalIgnoreCase);
    
    public static ExtensionDescription GetDescriptionES(string extension)
        {
            if (_extensionDescriptionsES.TryGetValue(extension, out var desc)) return desc;
            return ExtensionDescription.GetDefaultES(extension);
        }
        public static ExtensionDescription GetDescriptionEN(string extension)
        {
            if (_extensionDescriptionsEN.TryGetValue(extension, out var desc)) return desc;
            return ExtensionDescription.GetDefaultEN(extension);
        }

        public static IEnumerable<GroupExtensionDescription> GetGroupExtensionDescriptionsES(params IReadOnlyList<string> extension)
        {
            foreach (var groupExtensionDescription in _groupExtensionDescriptionsES)
            {
                var m=groupExtensionDescription.Matches(extension);
                if (m != null) yield return m;
            }
        }
        public static IEnumerable<GroupExtensionDescription> GetGroupExtensionDescriptionsEN(params IReadOnlyList<string> extension)
        {
            foreach (var groupExtensionDescription in _groupExtensionDescriptionsEN)
            {
                var m=groupExtensionDescription.Matches(extension);
                if (m != null) yield return m;
            }
        }

        public static string GetFilter(params IReadOnlyList<GroupExtensionDescription> groupextensiondescriptions)
        {
            return string.Join("|", groupextensiondescriptions);
        }
        public static string GetFilterES(params IReadOnlyList<string> extensions)
        {
            var groupExtensionDescriptions = GetGroupExtensionDescriptionsES(extensions).ToList();
            return GetFilter(groupExtensionDescriptions);
        }
        public static string GetFilterEN(params IReadOnlyList<string> extensions)
        {
            var groupExtensionDescriptions = GetGroupExtensionDescriptionsEN(extensions).ToList();
            return GetFilter(groupExtensionDescriptions);
        }
    }
    public record ExtensionDescription
    {
        public ExtensionDescription(string Extension, string Description, Color Color)
        {
            this.Extension = Extension;
            this.Description = Description;
            this.Color = Color;
        }

        public string Extension { get; init; }
        public string Description { get; init; }
        public Color Color { get; init; }

        public void Deconstruct(out string extension, out string description, out Color color)
        {
            extension = this.Extension;
            description = this.Description;
            color = this.Color;
        }

        public static ExtensionDescription GetDefaultES(string extension)
        {
            return new ExtensionDescription(extension, $"Archivo {extension}", Color.DarkBlue);
        }
        public static ExtensionDescription GetDefaultEN(string extension)
        {
            return new ExtensionDescription(extension, $"File {extension}", Color.DarkBlue);
        }
    }
    public record GroupExtensionDescription
    {
        public GroupExtensionDescription(string Description, Color Color, params IEnumerable<string> Extensions)
        {
            this.Description = Description;
            this.Color = Color;
            this.Extensions = Extensions.ToArray();
        }
        public string Description { get; init; }
        public Color Color { get; init; }
        public string[] Extensions { get; init; }
        public void Deconstruct(out string description, out Color color, out IReadOnlyList<string> extensions)
        {
            description = this.Description;
            color = this.Color;
            extensions = this.Extensions;
        }

        public GroupExtensionDescription? Matches(IReadOnlyList<string> extensions)
        {
            var res = Extensions.Intersect(extensions, StringComparer.OrdinalIgnoreCase).ToArray();
            if (!res.Any()) return null;
            return new GroupExtensionDescription(Description, Color, res);
        }

        public override string ToString()
        {
            var exts = string.Join(";", Extensions.Select(e=>$"*.{e}"));
            var ext2=string.Join(",", Extensions.Select(e=>$"*.{e}"));
            return $"{Description} ({ext2})|{exts}";
        }
    }
}
