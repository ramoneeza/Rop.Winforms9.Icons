using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DropControls;

   public static class InternalOleDataObject
   {
       
               [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
               public struct FILEGROUPDESCRIPTORW
               {
                   public uint cItems;
                   [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
                   public FILEDESCRIPTORW[] fgd;
               }

               [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
               public struct FILEDESCRIPTORW
               {
                   public uint dwFlags;
                   public Guid clsid;
                   public uint sizelcx;
                   public uint sizelcy;
                   public uint pointlX;
                   public uint pointlY;
                   public uint dwFileAttributes;
                   public FILETIME ftCreationTime;
                   public FILETIME ftLastAccessTime;
                   public FILETIME ftLastWriteTime;
                   public uint nFileSizeHigh;
                   public uint nFileSizeLow;
                   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                   public string cFileName;
               }

               [StructLayout(LayoutKind.Sequential)]
               public struct FILETIME
               {
                   public uint dwLowDateTime;
                   public uint dwHighDateTime;
               }
       
           public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
           {
               GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
               try
               {
                   IntPtr ptr = handle.AddrOfPinnedObject();
                   return Marshal.PtrToStructure<T>(ptr);
               }
               finally
               {
                   handle.Free();
               }
           }

           public static T StreamToStructure<T>(MemoryStream st) where T : struct
           {
               var r = Marshal.SizeOf<T>();
               //var bytes = st.ToArray();
               var bytes = new byte[r];
               st.Read(bytes, 0, r);
               return ByteArrayToStructure<T>(bytes);
           }

           public static MemoryStream? GetDataAsMemoryStream(this IDataObject data,string format)
           {
               var content = data.GetData(format, true) as MemoryStream;
               return content;
           }
           public static byte[]? GetDataAsBytes(this IDataObject data,string format)
           {
               var content = data.GetData(format, true);
               if (content is MemoryStream ms) return ms.ToArray();
               if (content is byte[] bytes) return bytes;
               return null;
           }
           public static string? GetDataAsString(this IDataObject data,string format)
           {
               var content = data.GetData(format, true);
               if (content is MemoryStream ms) content=ms.ToArray();
               if (content is byte[] bytes) return Encoding.UTF8.GetString(bytes);
               if (content is string str) return str;
               return null;
           }
           public static string[]? GetDataAsStrings(this IDataObject data,string format)
           {
               var content = data.GetData(format, true);
               if (content is MemoryStream ms) content=ms.ToArray();
               if (content is byte[] bytes) content=Encoding.UTF8.GetString(bytes);
               if (content is string str) content = str.Split((char)10).Select(l=>l.TrimEnd([' ',(char)0])).ToArray();
               return content as string[];
           }
           public static T? GetDataAsStructure<T>(this IDataObject data,string format) where T:struct
           {
               var content = data.GetDataAsMemoryStream(format);
               if (content is null) return null;
               return StreamToStructure<T>(content);
           }
       }
   

