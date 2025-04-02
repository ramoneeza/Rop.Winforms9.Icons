using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DropControls;

public static class ControlHelper
{
    public static void SetInv<T>(this Control c, ref T old, T value)
    {
        if (ReferenceEquals(old,value)) return;
        if (old is not null && old.Equals(value)) return;
        old = value;
        c.Invalidate();
    }
}
