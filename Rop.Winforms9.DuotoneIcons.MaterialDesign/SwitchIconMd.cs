using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rop.Winforms9.DuoToneIcons.MaterialDesign;

namespace Rop.Winforms9.DuotoneIcons.MaterialDesign;
internal partial class Dummy{}
public class SwitchIconMd : SwitchIcon
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IBankIcon? BankIcon => null;
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public SwitchIconMd():base()
    {
        Icons = IconRepository.GetEmbeddedIcons<MaterialDesignIcons>();
    }
}