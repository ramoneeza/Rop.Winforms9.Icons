using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.PartialControls
{
    internal partial class Dummy{}

    [DummyPartial]
    [AlsoInclude(typeof(PartialIHasIcon))]
    internal partial class PartialIHasOneIcon:Control, IHasOneIcon
    {
        private string _iconCode="";
        [DefaultValue("")]
        public virtual string IconCode
        {
            get => _iconCode;
            set=>_setPropTch(ref _iconCode,value);
        }

        private string _iconDisabled="";
        [DefaultValue("")]
        public virtual string IconDisabled
        {
            get => _iconDisabled;
            set => _setPropTch(ref _iconDisabled, value);
        }

        private DuoToneColor _iconColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual DuoToneColor IconColor
        {
            get => _iconColor;
            set=> _setPropInv(ref _iconColor, value);
        }
        [DefaultValue("")]
        public virtual string ToolTipText { get; set; } = "";
        public virtual string GetToolTipText() => ToolTipText;
        public virtual string GetIconCode() =>(Disabled && IconDisabled!="")?IconDisabled:IconCode;
        public virtual DuoToneColor GetIconColor()=>(Disabled?DisabledColor:IconColor).FinalColor(this.ForeColor);
    }
    //Abstract part
    internal partial class PartialIHasOneIcon
    {
        [ExcludeThis]
        public string GetText() => "";
        
    }
}
