using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.PartialControls
{
    internal partial class Dummy{}
    [DummyPartial]
    [AlsoInclude(typeof(PartialIHasBank))]
    [AlsoInclude(typeof(PartialIHasTooltip))]
    internal partial class PartialIHasIcon:Control, IHasIcon
    {
        public virtual ContentAlignment GetTextAlign()
        {
                switch (this as Control)
                {
                    case LinkLabel linkLabel:
                        return linkLabel.TextAlign;
                    case Label label:
                        return label.TextAlign;

                    case Button button:
                        return button.TextAlign;

                    case CheckBox checkBox:
                        return checkBox.TextAlign;

                    case RadioButton radioButton:
                        return radioButton.TextAlign;
                    default:
                        return ContentAlignment.MiddleCenter;
                }
        }
        public virtual void IconPaint(Graphics gr)
        {
            var args = IconArgs.Factory(this);
            if (!args.HasText && args.Icon == null) return;
            var measure = MeasuredIconString.Factory(gr,args);
            var offset = this.AlignOffset(GetTextAlign(), measure.Bounds);
            gr.DrawStringWithIcons(offset,measure);
        }
        public virtual Size GetPreferedSize()
        {
            using var g = CreateGraphics();
            var args= IconArgs.Factory(this);
            var measure = MeasuredIconString.Factory(g,args);
            var sf = measure.Bounds.Size;
            var w = 1 + sf.Width;
            var h = 1 + sf.Height;
            w += Padding.Horizontal + 3f;
            h += Padding.Vertical + 1f;
            var m = Math.Max(args.OffsetIcon,args.OffsetText);
            if (m > 0) h += m;
            return new Size((int)w, (int)h);
        }
        public DuoToneIcon? GetIcon() => Icons?.GetIcon(GetIconCode());
        
    }
    // Abstract Partial
    internal partial class PartialIHasIcon
    {
        // Abstract part

        [ExcludeThis]
        public string GetIconCode() => "";

        [ExcludeThis]
        public DuoToneColor GetIconColor()=>DuoToneColor.Default;
        [ExcludeThis]
        public string GetToolTipText() => "";
        [ExcludeThis]
        public string GetText() => "";
    }
}
