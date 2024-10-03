using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    internal class IbConfiguration
    {
        public List<BankPath> Banks { get; set; }= new();
    }

    public record BankPath(string Name = "", string Path = "")
    {
        public override string ToString()
        {
            return $"{Name} - {Path}";
        }

        public bool IsValid()
        {
            var js=System.IO.Path.Combine(Path, BankJson.Jsonfilename);
            return File.Exists(js);
        }
    }
}
