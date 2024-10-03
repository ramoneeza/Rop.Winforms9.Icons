using System.Drawing;
using System.Runtime.CompilerServices;

namespace Rop.Winforms9.DuoToneIcons.MaterialDesign
{
    public class MaterialDesignIcons : BaseEmbeddedIcons
    {
        public static readonly string IconsResourceName = "MD.bin";
        public MaterialDesignIcons() : base(IconsResourceName)
        {
        }

        public static void Register()
        {
            IconRepository.GetEmbeddedIcons<MaterialDesignIcons>();
        }
    }
   
}