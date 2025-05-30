﻿using SkiaSharp;
using Svg.Skia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DoutoneIconBuilder
{
    public static class SvgHelper
    {
        public static MemoryStream? SvgToStream(string svgfile,int desiredheight)
        {
            using var svg = new SKSvg();
            svg.Load(svgfile);
            if (svg.Picture== null) return null;
            if (svg.Picture.CullRect.IsEmpty) return null;
            
            var scale=desiredheight / svg.Picture.CullRect.Height;
            var width = (int)(svg.Picture.CullRect.Width * scale);
            var height = (int)(svg.Picture.CullRect.Height * scale);
            using var bitMap = new SKBitmap(width,height);
            using SKCanvas canvas = new SKCanvas(bitMap);
            canvas.Scale(scale,scale);
            canvas.DrawPicture(svg.Picture);
            canvas.Flush();
            canvas.Save();
            using SKImage image = SKImage.FromBitmap(bitMap);
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            MemoryStream memStream = new MemoryStream();
            data.SaveTo(memStream);
            memStream.Seek(0, SeekOrigin.Begin);
            return memStream;
        }
        
        
        public static Bitmap? LoadSvg(string svgfile)
        {
            using var memStream = SvgToStream(svgfile, 256);
            if (memStream == null) return null;
            var bmp = new Bitmap(memStream);
            return bmp;
        }
        public static byte[]? SvgToPng(string svgfile,int desiredheight)
        {
            using var memStream = SvgToStream(svgfile, desiredheight);
            if (memStream == null) return null;
            return memStream.ToArray();
        }
    }
}
