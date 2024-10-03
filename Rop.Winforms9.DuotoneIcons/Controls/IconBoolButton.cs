using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;
using System.ComponentModel;

namespace Rop.Winforms9.DuotoneIcons.Controls;

internal partial class Dummy
{
}
[IncludeFrom(typeof(PartialIHasBoolIconsText))]
public partial class IconBoolButton : Button,IHasBoolIconsText
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
        get => (!_painting)?base.Text:"";
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => base.Text = value??"";
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }
    private bool _painting = false;
    public IconBoolButton()
    {
        InitShowHidden();
        InitIHasToolTip();
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Value { get => SelectedIcon; set => SelectedIcon = value; }
    protected override void OnPaint(PaintEventArgs e)
    {
        _painting = true;
        base.OnPaint(e);
        _painting = false;
        IconPaint(e.Graphics);
    }
}