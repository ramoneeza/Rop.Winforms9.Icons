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
    internal partial class PartialIHasBoolIcons:Control,IHasBoolIcons
    {
        public event EventHandler? SelectedIconChanged;
        private DuoToneColor _iconColorOff = DuoToneColor.DefaultOneTone;
        [DefaultValue(typeof(DuoToneColor), "DefaultOneTone")]
        public virtual DuoToneColor IconColorOff
        {
            get => _iconColorOff;
            set=> _setPropInv(ref _iconColorOff, value);
        }
        private DuoToneColor _iconColorOn=DuoToneColor.DefaultOneTone;
        [DefaultValue(typeof(DuoToneColor), "DefaultOneTone")]
        public virtual DuoToneColor IconColorOn
        {
            get => _iconColorOn;
            set=>_setPropInv(ref _iconColorOn,value);
        }
        private string _defaultIcon="";
        [DefaultValue("")]
        public virtual string DefaultIcon
        {
            get => _defaultIcon;
            set=>_setPropTch(ref _defaultIcon,value);
        }
        private string _iconDisabled="";
        [DefaultValue("")]
        public virtual string IconDisabled
        {
            get => _iconDisabled;
            set=> _setPropTch(ref _iconDisabled, value);
        }
        private string _iconOff = "";
        [DefaultValue("")]
        public virtual string IconOff
        {
            get => _iconOff;
            set=> _setPropTch(ref _iconOff, value);
        }
        private string _iconOn = "";
        [DefaultValue("")]
        public virtual string IconOn
        {
            get => _iconOn;
            set => _setPropTch(ref _iconOn, value);
        }
        private bool _selectedIcon;
        [DefaultValue(false)]
        public bool SelectedIcon
        {
            get => _selectedIcon;
            set
            {
                if (_setPropTch(ref _selectedIcon,value))
                    SelectedIconChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        [DefaultValue(false)]
        public virtual string DefaultToolTipText { get; set; } = "";
        [DefaultValue("")]
        public virtual string ToolTipOn { get; set; } = "";
        [DefaultValue("")]
        public virtual string ToolTipOff { get; set; } = "";
        [DefaultValue("")]
        public virtual string ToolTipDisabled { get; set; } = "";
        public virtual string GetToolTipText()
        {
            if (Disabled) return !string.IsNullOrEmpty(ToolTipDisabled) ? ToolTipDisabled : DefaultToolTipText;
            var r = SelectedIcon ? ToolTipOn : ToolTipOff;
            return !string.IsNullOrEmpty(r) ? r : DefaultToolTipText;
        }
        public virtual string GetIconCode()
        {
            var r = SelectedIcon ? IconOn : IconOff;
            if (Disabled && !string.IsNullOrEmpty(IconDisabled)) r=IconDisabled;
            return !string.IsNullOrEmpty(r) ? r : DefaultIcon;
        }
        public virtual DuoToneColor GetIconColor()
        {
            if (Disabled) return !DisabledColor.IsEmpty ? DisabledColor : DefaultIconColor;
            var r = SelectedIcon ? IconColorOn : IconColorOff;
            return r.FinalColor(DefaultIconColor);
        }
        private DuoToneColor _defaultIconColor=DuoToneColor.DefaultOneTone;
        [DefaultValue(typeof(DuoToneColor), "DefaultOneTone")]
        public virtual DuoToneColor DefaultIconColor
        {
            get => _defaultIconColor;
            set
            {
                _defaultIconColor= value;
                Invalidate();
            
            }
        }
        public virtual string GetText() => Text;
    }
}
