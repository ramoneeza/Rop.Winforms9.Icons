using System.ComponentModel;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls;
internal partial class Dummy{}
[IncludeFrom(typeof(PartialIHasBoolIcons))]
public partial class SoloIconBoolLabel : Label,IHasBoolIcons
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Value
    {
        get => SelectedIcon;
        set => SelectedIcon = value;
    }
    public SoloIconBoolLabel()
    {
        InitShowHidden();
        InitIHasToolTip();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);
        IconPaint(e.Graphics);
    }
    protected override void OnFontChanged(EventArgs e)
    {
        if (AutoSize) Size= GetPreferredSize(Size);
        base.OnFontChanged(e);
    }
    public override Size GetPreferredSize(Size proposedSize)
    {
        var res=(!AutoSize)?base.GetPreferredSize(proposedSize):GetPreferedSize();
        return res;
    }
}