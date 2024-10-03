using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DuotoneIcons.PartialControls;

public class IconView
{
    public string Icon { get; set; } = "";
    public DuoToneColor Color { get; set; } = DuoToneColor.Empty;
    public string Text { get; set; } = "";
    public string ToolTip { get; set; } = "";
    public bool IsEmpty => Icon == "" && Color == DuoToneColor.Empty && Text == "" && ToolTip == "";
    public IconView() { }
    public IconView(string icon, DuoToneColor color, string text, string toolTip)
    {
        Icon = icon;
        Color = color;
        Text = text;
        ToolTip = toolTip;
    }
    public IconView(IconView iconView)
    {
        Icon = iconView.Icon;
        Color = iconView.Color;
        Text = iconView.Text;
        ToolTip = iconView.ToolTip;
    }
}
public class IconViewCollection
{
    private readonly List<IconView> _items = new List<IconView>();
    public IReadOnlyList<IconView> Items => _items;
    public IconView? Get(int index)
    {
        if (index < 0 || index > _items.Count) return null;
        if (index == _items.Count)
        {
            var item = new IconView();
            _items.Add(item);
            return item;
        }
        return _items[index];
    }
    private void _trimItems()
    {
        while (_items.Count > 0 && _items[^1].IsEmpty) _items.RemoveAt(Items.Count - 1);
    }
    public void SetIcons(IReadOnlyList<string> icons)
    {
        for (int i = 0; i < icons.Count; i++)
        {
            SetIcon(i, icons[i]);
        }
        _trimItems();
    }
    public void SetColor(IReadOnlyList<DuoToneColor> colors)
    {
        for (int i = 0; i < colors.Count; i++)
        {
            SetColor(i, colors[i]);
        }
        _trimItems();
    }
    public void SetText(IReadOnlyList<string> texts)
    {
        for (int i = 0; i < texts.Count; i++)
        {
            SetText(i, texts[i]);
        }
        _trimItems();
    }

    public void SetTooltip(IReadOnlyList<string> tooltips)
    {
        for (int i = 0; i < tooltips.Count; i++)
        {
            SetTooltip(i, tooltips[i]);
        }
        _trimItems();
    }
    public string[] GetIcons() => Items.Select(i => i.Icon).ToArray();
    public DuoToneColor[] GetColors() => Items.Select(i => i.Color).ToArray();
    public string[] GetTexts() => Items.Select(i => i.Text).ToArray();
    public string[] GetTooltips() => Items.Select(i => i.ToolTip).ToArray();
    public string GetTooltipOrDefault(int index, string def)
    {
        var tt = Get(index)?.ToolTip;
        return string.IsNullOrEmpty(tt) ? def : tt;
    }
    public void SetColor(int i, DuoToneColor color)
    {
        var item = Get(i);
        if (item == null) return;
        item.Color = color;
    }
    public void SetIcon(int i, string icon)
    {
        var item = Get(i);
        if (item == null) return;
        item.Icon = icon;
    }
    public void SetText(int i, string text)
    {
        var item = Get(i);
        if (item == null) return;
        item.Text = text;
    }
    public void SetTooltip(int i, string tooltip)
    {
        var item = Get(i);
        if (item == null) return;
        item.ToolTip = tooltip;
    }
    public DuoToneColor GetColorOrDefault(int i, DuoToneColor? def)
    {
        var item = Get(i);
        if (item?.Color.IsEmpty ?? true) return def ?? DuoToneColor.Empty;
        return item.Color;
    }
    public string GetTextOrDefault(int i, string parentText)
    {
        var item = Get(i);
        return string.IsNullOrEmpty(item?.Text) ? parentText : item.Text;
    }
    public string GetIconOrDefault(int i, string defaulticon)
    {
        var item = Get(i);
        return string.IsNullOrEmpty(item?.Icon) ? defaulticon : item.Icon;
    }
}
