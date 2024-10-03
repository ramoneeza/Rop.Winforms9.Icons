namespace Rop.Winforms9.DuotoneIcons.PartialControls;

public interface IHasBank
{
    Font Font { get; }
    bool Disabled { get; }
    DuoToneColor DisabledColor { get; }
    IBankIcon? BankIcon { get; }
    IEmbeddedIcons? Icons { get; }
    int IconScale { get; }
    float OffsetIcon { get; }
    int MinAscent { get; }
    int MinHeight { get; }
    bool DisableAndThereIsDisabledColor();
}