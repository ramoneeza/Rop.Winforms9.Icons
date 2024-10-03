using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    public class Ejemplo:Control
    {
        private Bitmap? _bitmap;

        public Bitmap? Bitmap
        {
            get => _bitmap;
            set
            {
                _bitmap = value;
                Invalidate();
            }
        }

        private int _baseLine;

        public int BaseLine
        {
            get => _baseLine;
            set
            {
                _baseLine = value; 
                Invalidate();
            }
        }

        public Ejemplo()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            Text = "Example";
        }

        public float AscentIconUnit => BaseLine / (float)(Bitmap?.Height??1);
        public float WidthUnit => (Bitmap?.Width ?? 1) / (float)(Bitmap?.Height??1);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var measureString = e.Graphics.MeasureString("A", Font);
            var ascent = GetFontAscent(Font,e.Graphics);
            var h = ascent * 1.25f;
            var w=(int)(h * WidthUnit);
            var ascenticon =(int)(h * AscentIconUnit);
            // obtener ascent de la fuente
            var y0 = 0;
            var y1=ascenticon- ascent;
            if (y1 < 0)
            {
                y0 = -y1;
                y1 = 0;
            }
            e.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, Width, Height);
            if (Bitmap!=null) e.Graphics.DrawImage(Bitmap, 0, y0, w, h);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), w, y1);
        }
        private int GetFontAscent(Font font,Graphics g)
        {
            float tamanoEnPixeles = font.SizeInPoints * g.DpiY / 72f;
            // Obtener la familia de la fuente
            FontFamily fontFamily = font.FontFamily;
            // Obtener el valor de Ascent en unidades de diseño
            int ascent = fontFamily.GetCellAscent(font.Style);
            // Convertir el valor de Ascent a unidades de la fuente
            float ascentInPixels = ascent * tamanoEnPixeles / fontFamily.GetEmHeight(font.Style);
            return (int)ascentInPixels;
        }
    }
}
