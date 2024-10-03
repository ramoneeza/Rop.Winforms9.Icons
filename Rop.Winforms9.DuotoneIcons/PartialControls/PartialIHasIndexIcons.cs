using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.IncludeFrom.Annotations;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.ComponentModel;
using System.Drawing.Design;
using Rop.Winforms9.DuotoneIcons.PartialControls;
using Rop.Winforms9.DuotoneIcons;

namespace Rop.Winforms9.DuotoneIcons.PartialControls;
internal partial class Dummy{}

[DummyPartial]
[IncludeFrom(typeof(PartialIHasIcon))]
internal partial class PartialIHasIndexIcons:Control,IHasIndexIcons
{
    public event EventHandler? SelectedIconChanged;
    private readonly IconViewCollection _items= new IconViewCollection();
    private int _selectedicon;
    private DuoToneColor _defaultIconColor;
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IconViewCollection ItemsCollection => _items;
    [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public string[] Items
    {
        get => _items.GetIcons();
        set
        {
            _items.SetIcons(value);
            LaunchTextChanged();
        }
    }
    [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public string[] ColorItemsStr
    {
        get => _items.GetColors().Select(x => x.ToString()).ToArray();
        set
        {
            _items.SetColor(value.Select(DuoToneColor.Parse).ToList());
            Invalidate();
        }
    }
    
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public DuoToneColor[] ColorItems
    {
        get => _items.GetColors();
        set
        {
            _items.SetColor(value);
            Invalidate();
        }
    }
    [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public string[] ToolTips
    {
        get => _items.GetTooltips();
        set => _items.SetTooltip(value);
    }
    [DefaultValue("")]
    public virtual string DefaultToolTipText { get; set; } = "";
    public virtual string GetToolTipText() =>(ShowToolTip)?_items.GetTooltipOrDefault(SelectedIcon, DefaultToolTipText):"";
    [DefaultValue(-1)]
    public int SelectedIcon
    {
        get => _selectedicon;
        set
        {
            _selectedicon = value;
            LaunchTextChanged();
            SelectedIconChanged?.Invoke(this,EventArgs.Empty);
        }
    }
    public virtual DuoToneColor GetIconColor() => GetIconColor(SelectedIcon);
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual DuoToneColor DefaultIconColor
    {
        get => _defaultIconColor;
        set
        {
            _defaultIconColor = value;
            Invalidate();
        }
    }
    public void SetIconColor(int index, DuoToneColor color)
    {
        _items.SetColor(index,color);
        Invalidate();
    }
    public void SetToolTipText(int index, string msg) => _items.SetTooltip(index, msg);
    public DuoToneColor GetIconColor(int i, DuoToneColor? def=null) =>_items.GetColorOrDefault(i, def);
    public string GetIconCode(int i) => _items.GetIconOrDefault(i, "");

    public virtual string GetIconCode() => GetIconCode(SelectedIcon);
    public void SetIcon(int i, string text)
    {
        _items.SetIcon(i, text);
        Invalidate();
    }
    public virtual string GetText() => "";
}