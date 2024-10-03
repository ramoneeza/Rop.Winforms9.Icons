using System.ComponentModel;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Rop.Winforms8._1.DoutoneIconBuilder;

namespace Rop.Winforms8.DuotoneIcons
{
    public static class Converter
    {
        
        public static void From32BTo4B(ReadOnlySpan<int> bmp,Span<byte> dst)
        {
            var i = 0;
            var f = 0;
            while(i<bmp.Length)
            {
                var g0 = GetGray2B((UInt32)bmp[i]);
                i++;
                var g1 = GetGray2B((UInt32)bmp[i]);
                i++;
                var bytepack = g1 + g0 * 16;
                dst[f] = (byte)bytepack;
                f++;
            }
            return;
            // Local functions
            int GetGray2B(UInt32 a)
            {
                if ((a & 0xFF000000) ==0) return 0;
                var gr = a & 0x00FFFFFF;
                if (gr==0) return 1;
                if (gr==0x00FFFFFF) return 0;
                return 2;
            }
        }
        public static void From32BTo4B(Bitmap bmp,Span<byte> dst)
        {
            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bmpPtr = bmpData.Scan0;
            var strideint = bmpData.Stride / 4;
            var buffer = new int[bmpData.Height * strideint];
            Marshal.Copy(bmpPtr, buffer, 0, buffer.Length);
            bmp.UnlockBits(bmpData);
            for (var y = 0; y < bmp.Height; y++)
            {
                var linea= buffer.AsSpan().Slice(y * strideint, bmp.Width);
                From32BTo4B(linea, dst.Slice(y * bmp.Width / 2));
            }
        }

        public static byte[] From32BTo4B(Bitmap bmp)
        {
            var res= new byte[bmp.Width * bmp.Height / 2]; // 2 pixels per byte
            From32BTo4B(bmp, res.AsSpan());
            return res;
        }


        public static byte[] From32BTo4B(ReadOnlySpan<int> bmp)
        {
            var res=new byte[bmp.Length/2]; // 2 pixels per byte
            From32BTo4B(bmp, res.AsSpan());
            return res;
        }
        public static void From4BTo32B(ReadOnlySpan<byte> bmp,Span<int> dst,Color? color1=null,Color? color2=null)
        {
            int[] colors=[Color.Transparent.ToArgb(),(color1??Color.Black).ToArgb(),(color2??Color.Gray).ToArgb(),Color.White.ToArgb()];
            From4BTo32B(bmp, dst, colors);
        }
        private static void From4BTo32B(ReadOnlySpan<byte> bmp,Span<int> dst,int[] colors)
        {
            var i = 0;
            var f = 0;
            while(i<bmp.Length)
            {
                var a = bmp[i];
                var g0= a & 0x0F;
                var g1 = a >> 4;
                dst[f] = colors[g0];
                f++;
                dst[f] = colors[g1];
                f++;
                i++;
            }
        }
        
        public static int[] From4BTo32B(ReadOnlySpan<byte> bmp, Color? color1 = null, Color? color2 = null)
        {
            var res = new int[bmp.Length * 2]; // 2 pixels per byte
            From4BTo32B(bmp, res.AsSpan(), color1, color2);
            return res;
        }
        public static Bitmap ToBmp4(ReadOnlySpan<byte> data,Size size,Color? color1=null,Color? color2=null)
        {
            var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format4bppIndexed);
            var pal = bmp.Palette;
            pal.Entries[0] = Color.Transparent;
            pal.Entries[1] = color1??Color.Black;
            pal.Entries[2] = color2??Color.Gray;
            bmp.Palette = pal;
            var bmpData = bmp.LockBits(new Rectangle(0, 0, size.Width, size.Height), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);
            var scan0 = bmpData.Scan0;
            var stride = bmpData.Stride;
            var buffer=new byte[bmpData.Stride * size.Height];
            var bufferspan= buffer.AsSpan();
            for (var y = 0; y < size.Height; y++)
            {
                var linea = data.Slice(y * size.Width/2, size.Width/2);
                var lineadst = bufferspan.Slice(y * stride,stride);
                linea.CopyTo(lineadst);
            }
            Marshal.Copy(buffer,0,scan0,buffer.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
        public static Bitmap ToBmp32(ReadOnlySpan<byte> data,Size size,Color? color1=null,Color? color2=null)
        {
            int[] colors=[Color.Transparent.ToArgb(),(color1??Color.Black).ToArgb(),(color2??Color.Gray).ToArgb(),Color.White.ToArgb()];
            var bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            var bmpData = bmp.LockBits(new Rectangle(0, 0, size.Width, size.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            var scan0 = bmpData.Scan0;
            var stridebyte = bmpData.Stride;
            var stride = stridebyte / 4;
            var buffer=new int[stride * size.Height];
            var bufferspan= buffer.AsSpan();
            for (var y = 0; y < size.Height; y++)
            {
                var linea = data.Slice(y * size.Width, size.Width);
                From4BTo32B(linea, bufferspan.Slice(stride*y), colors);
            }
            Marshal.Copy(buffer,0,scan0,buffer.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        public static int[] Normalize(Bitmap bmp)
        {
            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bmpPtr = bmpData.Scan0;
            var strideint = bmpData.Stride / 4;
            var buffer = new int[bmpData.Height * strideint];
            Marshal.Copy(bmpPtr, buffer, 0, buffer.Length);
            bmp.UnlockBits(bmpData);
            var res = new int[bmp.Width * bmp.Height];
            for (var y = 0; y < bmp.Height; y++)
            {
                var linea= buffer.AsSpan().Slice(y * strideint, bmp.Width);
                for(var x = 0; x < bmp.Width; x++)
                {
                    var v = (UInt32)linea[x];
                    var a = (v & 0xFF000000) >> 24;
                    var rgb = (v & 0x00FFFFFF);
                    if (a == 0)
                        v = 0x00ffffff;
                    else
                    {
                        if (rgb == 0)
                        {
                            v = 0xff000000;
                        }
                        else
                        {
                            if (rgb != 0xffffff)
                            {
                                v =0xff808080;
                            }
                            else
                            {
                                v = 0x00ffffff;
                            }
                        }
                    }
                    res[y * bmp.Width + x] = (int)v;
                }
            }

            return res;
        }
    }
}