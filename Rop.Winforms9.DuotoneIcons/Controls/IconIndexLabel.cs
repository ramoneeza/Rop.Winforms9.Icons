using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls;

internal partial class Dummy
{ }
[IncludeFrom(typeof(PartialIHasIndexIconsText))]
public partial class IconIndexLabel : Label,IHasIndexIconsText
{
    public IconIndexLabel()
    {
        InitShowHidden();
        InitIHasToolTip();
    }
    public void SetBoolIcon(bool? value)
    {
        SelectedIcon = (value is null) ? 2 : (value.Value ? 1 : 0);
    }
    public bool? GetBoolIcon()
    {
        return SelectedIcon switch
        {
            0 => false,
            1 => true,
            _ => null
        };
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