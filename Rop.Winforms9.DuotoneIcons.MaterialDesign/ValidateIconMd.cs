using System.ComponentModel;
using Rop.Winforms9.DuoToneIcons.MaterialDesign;

namespace Rop.Winforms9.DuotoneIcons.MaterialDesign;
internal partial class Dummy{}
public sealed class ValidIconMd : IconBoolLabel
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override IBankIcon? BankIcon => null;

    
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => base.Text;
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => base.Text = value??"";
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }

    public ValidIconMd()
    {
        Icons=IconRepository.GetEmbeddedIcons<MaterialDesignIcons>();
        _iconInvalid = "CloseThick";
        _iconFixable = "Hammer";
        ValidIcon = "CheckBold";
        DefaultIconColor = DuoToneColor.DefaultOneTone;
        DefaultIconText = "";
        ValidColor = Color.Black;
        InvalidColor = Color.Tomato;
        FixableColor = Color.Purple;
        IsFixable = false;
    }
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color ValidColor
    {
        get => IconColorOn.Color1;
        set => IconColorOn = IconColorOn.WithColor1(value);
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ValidIcon
    {
        get => IconOn;
        set => IconOn = value;
    }

    private Color _invalidColor;
    [DefaultValue(typeof(Color), "Empty")]
    public Color InvalidColor
    {
        get => _invalidColor;
        set
        {
            _invalidColor = value;
            if (!_isFixable)
            {
                base.IconColorOff = value;
            }
            Invalidate();
        }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DuoToneColor IconColorOff
    {
        get => (_isFixable) ? FixableColor : InvalidColor;
        set => base.IconColorOff=(_isFixable)? FixableColor : InvalidColor;
    }

    private Color _fixableColor;
    [DefaultValue(typeof(Color), "Empty")]
    public Color FixableColor
    {
        get => _fixableColor;
        set
        {
            _fixableColor = value;
            if (_isFixable)
            {
                base.IconColorOff = value;
            }
            Invalidate();
        }
    }

    private bool _isFixable;
    [DefaultValue(false)]
    public bool IsFixable
    {
        get => _isFixable;
        set
        {
            _isFixable = value;
            base.IconColorOff = value ? FixableColor : InvalidColor;
            base.IconOff = value ? FixableIcon : InvalidIcon;
        }
    }

    private string _iconInvalid;
    [DefaultValue("")]
    public string InvalidIcon
    {
        get => _iconInvalid;
        set
        {
            _iconInvalid = value;
            if (!_isFixable)
            {
                base.IconOff = value;
            }
            Invalidate();
        }
    }

    private string _iconFixable;
    [DefaultValue("")]
    public string FixableIcon
    {
        get => _iconFixable;
        set
        {
            _iconFixable = value;
            if (_isFixable)
            {
                base.IconOff = value;
            }
            Invalidate();
        }
    }

}