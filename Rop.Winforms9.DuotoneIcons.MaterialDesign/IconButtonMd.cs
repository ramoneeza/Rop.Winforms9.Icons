using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rop.Winforms9.DuoToneIcons.MaterialDesign;
internal partial class Dummy{}
public class IconButtonMd : SoloIconButton
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IBankIcon? BankIcon => null;
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public IconButtonMd()
    {

        Icons = IconRepository.GetEmbeddedIcons<MaterialDesignIcons>()!;
        IconCode = "FieldEdit";
        IconDisabled = "FieldEditOff";
        DisabledColor = Color.Gray;
    }
}
