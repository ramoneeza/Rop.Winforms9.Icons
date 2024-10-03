using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.Controls
{
    internal partial class Dummy{ }
    [IncludeFrom(typeof(PartialIHasOneIcon))]
    public partial class SoloIconButton:Button,IHasOneIcon
    {
        public event EventHandler? ValueChanged;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Value
        {
            get => this.Enabled;
            set => Enabled = value;
        }
        public SoloIconButton():base()
        {
            InitShowHidden();
            InitIHasToolTip();
        }
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
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            ValueChanged?.Invoke(this, e);
        }

        public string GetText() => "";
    }
}
