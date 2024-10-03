using System.ComponentModel;

namespace Rop.Winforms9.DuotoneIcons.PartialControls;

public interface IHasIndexIcons : IHasIcon
{
    IconViewCollection ItemsCollection { get; }
    string[] Items { get; set; }
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    DuoToneColor[] ColorItems { get; set; }
    string[] ColorItemsStr { get; set; }
    string[] ToolTips { get; set; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    DuoToneColor DefaultIconColor { get; set; }
    string DefaultToolTipText { get; set; }
    int SelectedIcon { get; set; }
}