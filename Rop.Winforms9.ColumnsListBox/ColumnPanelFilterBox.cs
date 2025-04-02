using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.Helper;
using Timer = System.Windows.Forms.Timer;

namespace Rop.Winforms9.ColumnsListBox
{
    [DesignerCategory("Code")]
    public class ColumnPanelFilterBox : AbsColumnPanelFilterBox<string>
    {
        protected override int IndexOf(string item)
        {
            return ListBox.Items.IndexOf(item);
        }
        public ColumnPanelFilterBox(Control parent, Rectangle columnbounds, IEnumerable<string> items,bool selectmultiple,IEnumerable<string>? selecteditems=null):base(parent,columnbounds, items, selectmultiple, selecteditems)
        {}        
    }


}
