using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DropControls
{
    public record ColorExtension
    {
        public string Extension { get; init; } = "";
        public Color Background { get; init; }
        public Color Foreground { get; init; }

        public ColorExtension(string extension, Color background, Color foreground)
        {
            Extension = extension;
            Background = background;
            Foreground = foreground;
        }
        public ColorExtension()
        {
        }

        public static ColorExtension Empty { get; } = new ColorExtension();
    }
   
}
