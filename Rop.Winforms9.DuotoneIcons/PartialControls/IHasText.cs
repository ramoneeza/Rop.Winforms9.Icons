using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DuotoneIcons.PartialControls
{
    public interface IHasText : IHasIcon
    {
        bool UseIconColor { get; }
        float OffsetText { get; }
        float IconMarginLeft { get; }
        float IconMarginRight { get; }
        bool IsSuffix { get; }
        TextRenderingHint TextRenderingHint { get; }
        Color GetForeColor();
    }


}
