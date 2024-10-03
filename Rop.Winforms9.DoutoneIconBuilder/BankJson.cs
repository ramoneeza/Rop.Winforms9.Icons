using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    public record BankJson
    {
        public const string Jsonfilename= "_bank.json";
        public const string BankFolder = "Bank";
        public const string ExtraFolder = "Extra";
        public string BasePath { get; private init; } = "";
        public string BankPath=>Path.Combine(BasePath, BankFolder);
        public string ExtraPath => Path.Combine(BasePath, ExtraFolder);
        public string Name { get; init; } = "";
        public string Initials { get; init; } = "";
        public CustomSize BaseSize { get; init; } = new Size(0, 0);
        public Dictionary<string,BmpIcon> Icons { get; private init; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string,string> Alias { get; private init; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string,string[]> ReverseAlias { get; private init; }=new (StringComparer.OrdinalIgnoreCase);
        public static BankJson CreateNew(string name,string initials,string path, IProgress<(int, int)>? progress=null)
        {
            path= StripPath(path);
            var bankpath = Path.Combine(path, "Bank");
            var extrapath = Path.Combine(path, "Extra");
            var bmps=Directory.GetFiles(bankpath, "*.png").OrderBy(f=>f).ToList();
            var bmps2 = Directory.GetFiles(extrapath, "*.png").OrderBy(f => f).ToList();
            var icons = new Dictionary<string, BmpIcon>(StringComparer.OrdinalIgnoreCase);
            var max = bmps.Count+bmps2.Count;
            var cnt = 0;
            foreach (var f in bmps.Concat(bmps2))
            {
                var i=BmpIcon.Create(f,true);
                progress?.Report((cnt++, max));
                if (i == null)
                {
                    Debug.Print($"{f} doesn't exist");
                    continue;
                }
                icons.Add(i.Code,i);
            };
            var mi = icons.Values.MaxBy(i => i.Size.Height*i.Size.Width)?.Size??Size.Empty;
            return new BankJson
            {
                Name = name,
                Initials = initials,
                Icons = icons,
                BaseSize = mi,
                BasePath = path,
            };
        }

        public void AddNew()
        {
            var bmps=Directory.GetFiles(BankPath, "*.png").OrderBy(f=>f).ToList();
            var bmps2 = Directory.GetFiles(ExtraPath, "*.png").OrderBy(f => f).ToList();
            foreach (var f in bmps.Concat(bmps2))
            {
                var code= Path.GetFileNameWithoutExtension(f);
                if (Icons.ContainsKey(code)) continue;
                var i=BmpIcon.Create(f,true);
                if (i == null)
                {
                    Debug.Print($"{f} doesn't exist");
                    continue;
                }
                Icons.Add(i.Code,i);
            }
        }
        
        
        public static string StripPath(string path)
        {
            if (path.EndsWith("/")) path = path.Substring(0, path.Length - 1);
            if (path.EndsWith(BankFolder,StringComparison.OrdinalIgnoreCase)) path = path.Substring(0, path.Length - BankFolder.Length);
            if (path.EndsWith(ExtraFolder,StringComparison.OrdinalIgnoreCase)) path = path.Substring(0, path.Length - ExtraFolder.Length);
            return path;
        }
        
        public static BankJson? Load(string path,IProgress<(int,int)>? progress)
        {
            path= StripPath(path);
            var bankpath = Path.Combine(path, BankFolder);
            var extrapath = Path.Combine(path, ExtraFolder);
            var json = File.ReadAllText(Path.Combine(bankpath,Jsonfilename));
            if (json == "") return null;
            var dto=JsonSerializer.Deserialize<BankJsonDto>(json);
            if (dto == null) return null;
            var iconsdto = dto.Icons;
            var icons = new Dictionary<string, BmpIcon>(StringComparer.OrdinalIgnoreCase);
            var alias = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var max = iconsdto.Count;
            var cnt = 0;
            

            foreach (var icon in iconsdto)
            {
                var fn=(icon.IsExtra)?Path.Combine(extrapath, icon.Code + ".png"):Path.Combine(bankpath, icon.Code + ".png");
                if (!File.Exists(fn))
                {
                    Debug.Print($"{fn} doesn't exist");
                    continue;
                }
                var i= BmpIcon.Create(path, icon);
                progress?.Report((cnt++, max));
                if (i == null)
                {
                    Debug.Print($"{fn} can't be created");
                    continue;
                }
                icons.Add(i.Code,i);
                foreach (var iconAlias in icon.Alias)
                {
                    alias[iconAlias] = icon.Code;
                }
            }
            var reverseAlias = alias.GroupBy(kv => kv.Value).ToDictionary(g => g.Key, g => g.Select(kv => kv.Key).ToArray());

            return new BankJson
            {
                Name = dto.Name,
                Initials = dto.Initials,
                Icons = icons,
                BaseSize = dto.BaseSize,
                BasePath = path,
                Alias = alias,
                ReverseAlias = reverseAlias,
            };
        }
        public void Save()
        {
            var icons = Icons.Values.OrderBy(i => i.Code).ToList();
            var iconsdto = new List<BmpIconDto>();
            foreach (var bmpIcon in icons)
            {
                var icondto=new BmpIconDto()
                {
                    Code = bmpIcon.Code,
                    Size = bmpIcon.Size,
                    BaseLine = bmpIcon.BaseLine,
                    IsNew = bmpIcon.IsNew,
                    IsExtra = bmpIcon.IsExtra,
                    Alias = ReverseAlias.GetValueOrDefault(bmpIcon.Code)??Array.Empty<string>()
                };
                iconsdto.Add(icondto);
            }
            var dto= new BankJsonDto
            {
                Name = Name,
                Initials = Initials,
                Icons = iconsdto,
                BaseSize = BaseSize,
            };
            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path.Combine(BankPath, Jsonfilename), json);
        }
        private class BankData
        {
            public string Name { get; set; } = "";
            public string Initials { get; set; } = "";
            public CustomSize BaseSize { get; set; } = new Size(0, 0);
            public List<IconData> Icons { get; set; } = new();
        }
        private class IconData
        {
            public string Code { get; set; } = "";
            public int Width { get; set; }
            public int Height { get; set; }
            public int BaseLine { get; set; } = 0;
            public byte[] Data { get; set; } = Array.Empty<byte>();
            public string[] Alias { get; set; } = Array.Empty<string>();
        }

        public void SaveBinary(IProgress<(int, int)>? progress)
        {
            var path = System.IO.Path.Combine(BankPath, $"_{Initials}.json");
            var pathzip = System.IO.Path.Combine(BankPath, $"_{Initials}.bin");
            var bi = new BankData()
            {
                Name = Name,
                Initials = Initials,
                BaseSize = BaseSize,
            };
            var max = Icons.Count;
            foreach (var icon in Icons.Values.OrderBy(i => i.Code))
            {
                var data = icon.Data;
                var iconData = new IconData
                {
                    Code = icon.Code,
                    Width = icon.Size.Width,
                    Height = icon.Size.Height,
                    BaseLine = icon.BaseLine,
                    Data = data.ToArray(),
                    Alias = ReverseAlias.GetValueOrDefault(icon.Code) ?? Array.Empty<string>()
                };
                bi.Icons.Add(iconData);
                progress?.Report((bi.Icons.Count, max));
            }

            var json = JsonSerializer.Serialize(bi, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
            progress?.Report((-1, -1));
            //Compress string 'json' into zip format
            var zip = CompressString(json);
            File.WriteAllBytes(pathzip, zip);
            return;

            byte[] CompressString(string str)
            {
                byte[] stringBytes = Encoding.UTF8.GetBytes(str);
                using var outputStream = new MemoryStream();
                using var gzipStream = new GZipStream(outputStream, CompressionMode.Compress);
                gzipStream.Write(stringBytes, 0, stringBytes.Length);
                gzipStream.Flush();
                gzipStream.Close();
                return outputStream.ToArray();
            }

        }
        
        public void AddAlias(BmpIcon icon,params string[] alias)
        {
            foreach (var a in alias)
            {
                Alias[a] = icon.Code;
            }
            if (!ReverseAlias.TryGetValue(icon.Code, out var aliases))
            {
                aliases = new string[] { };
            }
            aliases = aliases.Concat(alias).Distinct().ToArray();
            ReverseAlias[icon.Code] = aliases;
        }
        public void RemoveAlias(string alias)
        {
            // ReSharper disable once CanSimplifyDictionaryRemovingWithSingleCall
            if (Alias.TryGetValue(alias, out var code))
            {
                Alias.Remove(alias);
                if (ReverseAlias.TryGetValue(code, out var aliases))
                {
                    aliases = aliases.Where(a => a != alias).ToArray();
                    ReverseAlias[code] = aliases;
                }
            }
        }

        public string[] GetAlias(BmpIcon selectedIcon)
        {
            if (ReverseAlias.TryGetValue(selectedIcon.Code, out var aliases)) return aliases;
            return Array.Empty<string>();
        }

        public void ChangeAlias(BmpIcon icon, string[] newalias)
        {
            var oldalias = GetAlias(icon);
            foreach (var old in oldalias)
            {
                RemoveAlias(old);
            }
            AddAlias(icon,newalias);
        }

        public bool IsAliased(BmpIcon bmpIcon)
        {
            var a = GetAlias(bmpIcon);
            return a.Length > 0;
        }
    }
    
}
