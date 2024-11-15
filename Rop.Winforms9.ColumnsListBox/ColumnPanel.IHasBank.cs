using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.Winforms9.DuotoneIcons;

namespace Rop.Winforms9.ColumnsListBox;

internal partial class Dummy{}
public partial class ColumnPanel
{
    private DuoToneColor _disabledColor;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual DuoToneColor DisabledColor
    {
        get => _disabledColor;
        set=> _setwinv(ref _disabledColor, value);
    }
    private IBankIcon? _bankIcon;
    [DefaultValue(null)]
    public virtual IBankIcon? BankIcon
    {
        get => _bankIcon;
        set => _setwinv(ref _bankIcon, value);
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
        set => _setwinv(ref _icons, value);
    }
    private int _iconScale = 125;
    [DefaultValue(125)]
    public virtual int IconScale
    {
        get => _iconScale;
        set => _setwinv(ref _iconScale, value);
    }
    private float _offsetIcon;
    [DefaultValue(0f)]
    public virtual float OffsetIcon
    {
        get => _offsetIcon;
        set => _setwinv(ref _offsetIcon, value);
    }

    private int _minAscent;
    [DefaultValue(0)]
    public int MinAscent
    {
        get => _minAscent;
        set => _setwinv(ref _minAscent,value);
    }

    private int _minHeight;
    [DefaultValue(0)]
    public int MinHeight
    {
        get => _minHeight;
        set => _setwinv(ref _minHeight, value);
    }
    public virtual bool DisableAndThereIsDisabledColor() => Disabled && DisabledColor != DuoToneColor.Empty;
}