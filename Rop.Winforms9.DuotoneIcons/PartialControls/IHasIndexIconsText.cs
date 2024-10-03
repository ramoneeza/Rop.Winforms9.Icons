namespace Rop.Winforms9.DuotoneIcons.PartialControls;

public interface IHasIndexIconsText : IHasIndexIcons, IHasText
{
    string DefaultIconText { get; set; }
    string[] TextItems { get; set; }
    string TextIconDisabled { get; set; }
}