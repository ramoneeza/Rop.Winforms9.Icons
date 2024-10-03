using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls
{
    internal partial class Dummy{}
    [IncludeFrom(typeof(PartialIHasOneIconText))]
    public partial class IconLabel:Label,IHasOneIconText
    {
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
        public IconLabel()
        {
            InitShowHidden();
            InitIHasToolTip();
        }
    }
    
}
