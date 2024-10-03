using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls;

internal partial class Dummy{ }

[IncludeFrom(typeof(PartialIHasOneIcon))]
public partial class SoloIconLabel : Label,IHasOneIcon
{
    public SoloIconLabel()
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
        var res=(!AutoSize)?base.GetPreferredSize(proposedSize):this.GetPreferedSize();
        if (res.Height<=6 && res.Width<=6) res = new Size(16, 16);
        return res;
    }
    public string GetText() => "";
    
}