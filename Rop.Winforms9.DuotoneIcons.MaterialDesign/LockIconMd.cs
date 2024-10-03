using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rop.Winforms9.DuoToneIcons.MaterialDesign;

namespace Rop.Winforms9.DuotoneIcons.MaterialDesign;
internal partial class Dummy{}
public sealed class LockIconMd : IconBoolLabel
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

    public LockIconMd()
    {
        Icons=IconRepository.GetEmbeddedIcons<MaterialDesignIcons>();
        IconOff = "LockOpen";
        IconOn = "Lock";
        DefaultIconColor = DuoToneColor.DefaultOneTone;
        DefaultIconText = "Lock";
        IconColorOn = Color.Black;
        IconColorOff = Color.Black;
    }
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color LockColor
    {
        get => IconColorOn.Color1;
        set => IconColorOn = IconColorOn.WithColor1(value);
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color UnLockColor
    {
        get => IconColorOff.Color1;
        set => IconColorOff = IconColorOff.WithColor1(value);
    }
}