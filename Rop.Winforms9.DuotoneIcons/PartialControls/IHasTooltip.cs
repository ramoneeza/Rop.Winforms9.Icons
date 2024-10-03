namespace Rop.Winforms9.DuotoneIcons.PartialControls;

public interface IHasTooltip
{
    ToolTip? ToolTip { get;  }
    bool ShowToolTip { get;  }
    string GetToolTipText();
    void InitIHasToolTip();
    void DoShowTooltip(string s);
}