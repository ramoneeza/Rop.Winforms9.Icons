using System.Runtime.CompilerServices;

namespace Rop.Winforms9.DuotoneIcons.GoogleMaterial
{
    public class GoogleMaterialIcons : BaseEmbeddedIcons
    {
        public static readonly string IconsResourceName = "GM.bin";
        public GoogleMaterialIcons():base(IconsResourceName)
        {
        }
        public static void Register()
        {
            IconRepository.GetEmbeddedIcons<GoogleMaterialIcons>();
        }
    }
}