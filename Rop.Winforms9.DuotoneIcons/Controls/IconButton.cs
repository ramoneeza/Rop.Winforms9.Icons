using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls
{
    internal partial class Dummy{}
    [IncludeFrom(typeof(PartialIHasOneIconText))]
    public partial class IconButton:Button,IHasOneIconText
    {
        private bool _painting = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => (!_painting)?base.Text:"";
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
            set => base.Text = value??"";
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            _painting = true;
            base.OnPaint(e);
            _painting = false;
            IconPaint(e.Graphics);
        }
        public IconButton()
        {
            base.Text = "";
            InitShowHidden();
            InitIHasToolTip();
        }
        
    }
}
