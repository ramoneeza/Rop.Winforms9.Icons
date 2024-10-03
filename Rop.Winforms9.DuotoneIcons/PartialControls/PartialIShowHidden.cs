using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.DuotoneIcons.PartialControls;

namespace Rop.Winforms9.DuotoneIcons.PartialControls;

internal partial class Dummy{}

// Dummy definition

[DummyPartial]
internal class PartialIShowHidden:Control,IShowHidden
{
    private MethodInfo _onFontChangedMethod=default!;
    private MethodInfo _onTextChangedMethod=default!;
    public void InitShowHidden()
    {
        var t = this.GetType();
        _onFontChangedMethod = t.GetMethod("OnFontChanged", BindingFlags.Instance | BindingFlags.NonPublic) ??
                               throw new Exception("OnFontChanged not found");
        _onTextChangedMethod = t.GetMethod("OnTextChanged", BindingFlags.Instance | BindingFlags.NonPublic) ??
                               throw new Exception("OnTextChanged not found");
    }
    public void LaunchFontChanged()
    {
        _onFontChangedMethod.Invoke(this, new object[] { EventArgs.Empty });
    }
    public void LaunchTextChanged() => _onTextChangedMethod.Invoke(this, new object[] { EventArgs.Empty });

    protected bool _setPropFch<T>(ref T prop, T value)
    {
        if (EqualityComparer<T>.Default.Equals(prop, value)) return false;
        prop = value;
        LaunchFontChanged();
        return true;
    }
    protected bool _setPropTch<T>(ref T prop, T value)
    {
        if (EqualityComparer<T>.Default.Equals(prop, value)) return false;
        prop = value;
        LaunchTextChanged();
        return true;
    }
    protected bool _setPropInv<T>(ref T prop, T value)
    {
        if (EqualityComparer<T>.Default.Equals(prop, value)) return false;
        prop = value;
        Invalidate();
        return true;
    }
}