using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.Winforms9.DuoToneIcons.MaterialDesign;
using Rop.Winforms9.DuotoneIcons.Controls;

namespace Rop.Winforms9.DuotoneIcons.MaterialDesign
{
    internal partial class Dummy { }
    public class ColumnPanelMd: ColumnPanel
    {
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IBankIcon? BankIcon => null;
        public ColumnPanelMd()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Icons = IconRepository.GetEmbeddedIcons<MaterialDesignIcons>()!;
        }
    }
}
