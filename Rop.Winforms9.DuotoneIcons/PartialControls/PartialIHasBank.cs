using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.PartialControls;

internal partial class Dummy{}

[DummyPartial]
[AlsoInclude(typeof(PartialIShowHidden))]
internal partial class PartialIHasBank:Control,IShowHidden,IHasBank
{
    private DuoToneColor _disabledColor;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual DuoToneColor DisabledColor
    {
        get => _disabledColor;
        set=> _setPropInv(ref _disabledColor, value);
    }
    private IBankIcon? _bankIcon;
    [DefaultValue(null)]
    public virtual IBankIcon? BankIcon
    {
        get => _bankIcon;
        set => _setPropTch(ref _bankIcon, value);
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Disabled => !Enabled;
    
    private IEmbeddedIcons? _icons;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IEmbeddedIcons? Icons
    {
        get => BankIcon?.Bank??_icons;
        set => _setPropFch(ref _icons, value);
    }
    private int _iconScale = 125;
    [DefaultValue(125)]
    public virtual int IconScale
    {
        get => _iconScale;
        set => _setPropFch(ref _iconScale, value);
    }
    private float _offsetIcon;
    [DefaultValue(0f)]
    public virtual float OffsetIcon
    {
        get => _offsetIcon;
        set => _setPropFch(ref _offsetIcon, value);
    }

    private int _minAscent;
    [DefaultValue(0)]
    public int MinAscent
    {
        get => _minAscent;
        set => _setPropFch(ref _minAscent,value);
    }

    private int _minHeight;
    [DefaultValue(0)]
    public int MinHeight
    {
        get => _minHeight;
        set => _setPropFch(ref _minHeight, value);
    }
    
    public virtual bool DisableAndThereIsDisabledColor() => Disabled && DisabledColor != DuoToneColor.Empty;
}