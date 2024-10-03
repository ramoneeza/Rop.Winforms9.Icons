using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;
using System.ComponentModel;

namespace Rop.Winforms9.DuotoneIcons.PartialControls;
internal partial class Dummy{}
[DummyPartial]
[AlsoInclude(typeof(PartialIHasBoolIcons),"GetText")]
[AlsoInclude(typeof(PartialIHasText),"PartialIHasIcon")]
internal partial class PartialIHasBoolIconsText:Control,IHasBoolIconsText
{
    private string _defaultIconText = "";
    [DefaultValue("")]
    public virtual string DefaultIconText
    {
        get => _defaultIconText;
        set
        {
            _defaultIconText= value;
            LaunchTextChanged();
        }
    }
    private string _textIconDisabled = "";
    [DefaultValue("")]
    public virtual string TextIconDisabled
    {
        get => _textIconDisabled;
        set
        {
            _textIconDisabled = value;
            LaunchTextChanged();
        }
    }
    private string _textIconOff = "";
    [DefaultValue("")]
    public virtual string TextIconOff
    {
        get => _textIconOff;
        set
        {
            _textIconOff= value;
            LaunchTextChanged();
        }
    }
    private string _textIconOn = "";
    [DefaultValue("")]
    public virtual string TextIconOn
    {
        get => _textIconOn;
        set
        {
            _textIconOn= value;
            LaunchTextChanged();
        }
    }
    public virtual string GetText()
    {
        if (Disabled) return !string.IsNullOrEmpty(TextIconDisabled) ? TextIconDisabled : DefaultIconText;
        var r = SelectedIcon ? TextIconOn : TextIconOff;
        return !string.IsNullOrEmpty(r) ? r : DefaultIconText;
    }
}