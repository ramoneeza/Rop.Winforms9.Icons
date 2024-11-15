using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.ColumnsListBox;
internal partial class Dummy{}
public partial class ColumnPanel
{
    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);
        foreach (var column in _columns)
        {
            ColumnPaint(e,column);
            if (InteriorBorder)
            {
                var b = column.Bounds;
                e.Graphics.DrawLine(Pens.White, b.X, b.Y, b.X,b.Bottom);
                e.Graphics.DrawLine(new Pen(Color.Silver), b.Right-1, b.Y,b.Right- 1,b.Bottom);
            }
        }
        //Draw Border con BorderStyle sin usar ControlPaint
        switch (BorderStyle)
        {
            case BorderStyle.FixedSingle:
                e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, Width - 1, Height - 1);
                break;
            case BorderStyle.Fixed3D:
                var p1=BorderRaised? Pens.White : Pens.Gray;
                var p2=BorderRaised? Pens.Gray : Pens.White;
                e.Graphics.DrawLines(p1,new PointF[]{new(0,Height-1),new(0,0),new(Width-1,0)});
                e.Graphics.DrawLines(p2,new PointF[]{new(0,Height-1),new(Width-1,Height-1),new(Width-1,0)});
                break;
            default:
                break;
        }
    }
}