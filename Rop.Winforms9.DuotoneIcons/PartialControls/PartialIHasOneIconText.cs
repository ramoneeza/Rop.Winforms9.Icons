using System.ComponentModel;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.PartialControls;
internal partial class Dummy{}
[DummyPartial]
[AlsoInclude(typeof(PartialIHasOneIcon))]
[AlsoInclude(typeof(PartialIHasText),"PartialIHasIcon")]
internal partial class PartialIHasOneIconText:Control,IHasOneIconText
{
    public virtual string GetText() => Text;
}