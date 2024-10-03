namespace Rop.Winforms9.DuotoneIcons.PartialControls;

public interface IHasIcon : IHasBank, IHasTooltip, IShowHidden
{
    // Abstract part
    string GetIconCode();
    DuoToneIcon? GetIcon();
    DuoToneColor GetIconColor();
    string GetText();
    ContentAlignment GetTextAlign();
    void IconPaint(Graphics gr);
    Size GetPreferedSize();
}