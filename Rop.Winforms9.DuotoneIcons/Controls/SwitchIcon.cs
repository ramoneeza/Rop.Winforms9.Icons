using System.Diagnostics.CodeAnalysis;

namespace Rop.Winforms9.DuotoneIcons.Controls;
internal partial class Dummy{}
public class SwitchIcon : IconBoolLabel
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public SwitchIcon():base()
    {
        IconOff = "_ToggleSwitchOff";
        IconOn = "_ToggleSwitchOn";
        IconScale = 150;
        DefaultIconColor = DuoToneColor.DefaultOneTone;
        DefaultIconText = "Switch";
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SwitchColorOn
    {
        get => IconColorOn.Color1;
        set => IconColorOn = IconColorOn.WithColor1(value);
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SwitchColorOff
    {
        get => IconColorOff.Color1;
        set => IconColorOff = IconColorOff.WithColor1(value);
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => base.Text;
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => base.Text = value??"";
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }
    [DefaultValue(false)]
    public bool AutoChange { get; set; }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (AutoChange && !Disabled)
        {
            Value = !Value;
        }
        base.OnMouseDown(e); 
    }
}