using System.ComponentModel;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls;
internal partial class Dummy{}

[IncludeFrom(typeof(PartialIHasIndexIcons))]
public partial class SoloIconIndexLabel :Label,IHasIndexIcons
{
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override string Text { get => base.Text; set { } }
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public void SetBoolIcon(bool? value)
    {
        SelectedIcon = (value is null) ? 2 : (value.Value ? 1 : 0);
    }
    public SoloIconIndexLabel()
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