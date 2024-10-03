namespace Rop.Winforms9.DuotoneIcons.PartialControls;

public interface IHasOneIcon : IHasIcon
{
    string IconCode { get; }
    string IconDisabled { get; }
    DuoToneColor IconColor { get; }
    string ToolTipText { get; }
}

// ReSharper disable once PossibleInterfaceMemberAmbiguity
public interface IHasOneIconText : IHasText, IHasOneIcon
{

}