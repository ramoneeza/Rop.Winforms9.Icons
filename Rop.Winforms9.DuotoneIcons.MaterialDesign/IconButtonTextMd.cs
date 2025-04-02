using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Rop.Winforms9.DuoToneIcons.MaterialDesign;
internal partial class Dummy{}
public class IconButtonTextMd : IconButton
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IBankIcon? BankIcon => null;
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public IconButtonTextMd()
    {

        Icons = IconRepository.GetEmbeddedIcons<MaterialDesignIcons>()!;
        IconCode = "ExitApp";
    }
}