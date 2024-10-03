using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;
using System.ComponentModel;

namespace Rop.Winforms9.DuotoneIcons.Controls;
internal partial class Dummy
{ }
[IncludeFrom(typeof(PartialIHasBoolIconsText))]
public partial class IconBoolLabel : Label,IHasBoolIconsText
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Value
    {
        get =>SelectedIcon;
        set =>SelectedIcon = value;
    }
    public IconBoolLabel()
    {
        InitShowHidden();
        InitIHasToolTip();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(this.BackColor),e.ClipRectangle);
        IconPaint(e.Graphics);
    }
    public override Size GetPreferredSize(Size proposedSize)
    {
        if (!AutoSize) return base.GetPreferredSize(proposedSize);
        return GetPreferedSize();
    }
    
}