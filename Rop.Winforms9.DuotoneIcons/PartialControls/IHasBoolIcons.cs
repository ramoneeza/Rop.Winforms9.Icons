namespace Rop.Winforms9.DuotoneIcons.PartialControls;

public interface IHasBoolIcons : IHasIcon
{
    DuoToneColor DefaultIconColor { get; set; }
    DuoToneColor IconColorOff { get; set; }
    DuoToneColor IconColorOn { get; set; }
    string DefaultIcon { get; set; }
    string IconDisabled { get; set; }
    string IconOff { get; set; }
    string IconOn { get; set; }
    bool SelectedIcon { get; set; }
    string DefaultToolTipText { get; set; }
    string ToolTipOn { get; set; }
    string ToolTipOff { get; set; }
    string ToolTipDisabled { get; set; }
}

public interface IHasBoolIconsText : IHasBoolIcons, IHasText
{
    string DefaultIconText { get; set; }
    string TextIconDisabled { get; set; }
    string TextIconOff { get; set; }
    string TextIconOn { get; set; }
}
