using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DropControls
{
    internal static class ArtClass
    {
        public static Bitmap ArrowRightIcon=> GetArt("arrow_right_icon_16");
        public static Bitmap AttentionIcon => GetArt("attention_icon_16");
        public static Bitmap BurstIcon => GetArt("burst_icon_16");
        public static Bitmap CertIcon => GetArt("cert_icon_16");
        public static Bitmap Chess => GetArt("chess");
        public static Bitmap ClipboardCopyIcon => GetArt("clipboard_copy_icon_16");
        public static Bitmap ClipboardCopyIconBlack => GetArt("clipboard_copy_icon_16_black");
        public static Bitmap ClipboardPastIcon => GetArt("clipboard_past_icon_16");
        public static Bitmap ClipboardPastIconBlack => GetArt("clipboard_past_icon_16_black");
        public static Bitmap ClipboardPastIconBlackArrow => GetArt("clipboard_past_icon_16_black_arrow");
        public static Bitmap ClipboardPartIconRed => GetArt("clipboard_part_icon_16_red");
        public static Bitmap DeleteIcon => GetArt("delete_icon_16");
        public static Bitmap DeleteIconBlack => GetArt("delete_icon_16_black");
        public static Bitmap DesconnectedIcon => GetArt("desconnected_icon_16");
        public static Bitmap EmptyIcon => GetArt("empty_icon_16");
        public static Bitmap OpenFolderIcon => GetArt("open_folder_icon_16");
        public static Bitmap OpenFolderIconWhite => GetArt("open_folder_icon_16_white");
        public static Bitmap Rename => GetArt("rename_16");
        // Get embebbed resource
        private static readonly Dictionary<string, Bitmap> _bitmaps = new();
        private static Bitmap GetArt(string artName)
        {
            if (_bitmaps.TryGetValue(artName, out var bmp)) return bmp;
            var assembly = Assembly.GetExecutingAssembly();
            var ns=typeof(ArtClass).Namespace;
            var resourceStream = assembly.GetManifestResourceStream($"{ns}.Art.{artName}.png")
                                 ??throw new ArgumentException("Art not found");
            bmp = new Bitmap(resourceStream);
            _bitmaps.Add(artName, bmp);
            return bmp;
        }
    }
}
