using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using Rop.Winforms9.GraphicsEx.Colors;

namespace Rop.Winforms9.DuotoneIcons
{
    [TypeConverter(typeof(DuoToneColorConverter))]
    public struct DuoToneColor : IEquatable<DuoToneColor>
    {
        public static readonly DuoToneColor Default = new DuoToneColor(Color.Black, Color.Gray);
        public static readonly DuoToneColor DefaultOneTone = new DuoToneColor(Color.Black, Color.Transparent);
        public static readonly DuoToneColor Empty = new DuoToneColor(Color.Empty, Color.Empty);
        public static readonly DuoToneColor Disabled = new DuoToneColor(Color.Gray, Color.Transparent);
        public DuoToneColor(Color color1, Color color2)
        {
            Color1 = color1;
            Color2 = color2;
        }
        public DuoToneColor()
        {
            Color1 = Color.Empty;
            Color2 = Color.Empty;
        }
        [Browsable(true)]
        [DisplayName("Color1")]
        [Description("The first color.")]

        public Color Color1 { get; set; }
        [Browsable(true)]
        [DisplayName("Color2")]
        [Description("The second color.")]

        public Color Color2 { get; set; }
        [Browsable(false)]
        public bool IsEmpty => Color1 == Color.Empty && Color2 == Color.Empty;

        public bool Equals(DuoToneColor other)
        {
            return Color1.Equals(other.Color1) && Color2.Equals(other.Color2);
        }

        public override bool Equals(object? obj)
        {
            return obj is DuoToneColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Color1.GetHashCode() * 65497 + Color2.GetHashCode();
        }

        public void Deconstruct(out Color color1, out Color color2)
        {
            color1 = Color1;
            color2 = Color2;
        }

        public DuoToneColor WithColor1(Color color1)
        {
            return new DuoToneColor(color1, Color2);
        }
        public DuoToneColor WithColor2(Color color2)
        {
            return new DuoToneColor(Color1, color2);
        }

        public static explicit operator DuoToneColor((Color, Color) tuple)
        {
            return new DuoToneColor(tuple.Item1, tuple.Item2);
        }

        public DuoToneColor FinalColor(Color foreColor)
        {
            return new DuoToneColor(Color1.IsEmpty ? foreColor : Color1,
                Color2.IsEmpty ? Color.Transparent : Color2);
        }
        public DuoToneColor FinalColor(DuoToneColor foreColor)
        {
            return IsEmpty ? foreColor : this;
        }


        public override string ToString()
        {
            return $"({ColorNames.ColorToString(Color1)},{ColorNames.ColorToString(Color2)})";
        }
        public static bool operator ==(DuoToneColor a, DuoToneColor b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DuoToneColor a, DuoToneColor b)
        {
            return !a.Equals(b);
        }

        public static DuoToneColor OneTone(Color color)
        {
            return new DuoToneColor(color, Color.Transparent);
        }

        public static DuoToneColor Parse(string s)
        {
            if (s.StartsWith("(") && s.EndsWith(")"))
            {
                var parts = s.Substring(1, s.Length - 2).Split(',');
                if (parts.Length == 2)
                {
                    return new DuoToneColor(ColorNames.ColorFromString(parts[0]),ColorNames.ColorFromString(parts[1]));
                }
            }
            return new DuoToneColor(ColorNames.ColorFromString(s), Color.Transparent);
        }

        public static implicit operator DuoToneColor(Color color)
        {
            return new DuoToneColor(color, Color.Transparent);
        }

    }

    public class DuoToneColorConverter : TypeConverter
    {

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }
        [SuppressMessage("Microsoft.Performance", "CA1808:AvoidCallsThatBoxValueTypes")]
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {

            if (value is string strValue)
            {

                string text = strValue.Trim();
                if (text.Length == 0)
                {
                    return null;
                }
                else
                {
                    return DuoToneColor.Parse(text);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// 
        /// 
        ///      Converts the given object to another type.  The most common types to convert
        ///      are to and from a string object.  The default implementation will make a call 
        ///      to ToString on the object if the object is valid and if the destination
        ///      type is string.  If this cannot convert to the desitnation type, this will 
        ///      throw a NotSupportedException. 
        /// 
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }
            if (value is DuoToneColor pt)
            {
                if (destinationType == typeof(string))
                {
                    return pt.ToString();
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    ConstructorInfo? ctor = typeof(DuoToneColor).GetConstructor(new Type[] { typeof(Color), typeof(Color) });
                    if (ctor != null)
                    {
                        return new InstanceDescriptor(ctor, new object[] { pt.Color1, pt.Color2 });
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


        [SuppressMessage("Microsoft.Performance", "CA1808:AvoidCallsThatBoxValueTypes")]
        [SuppressMessage("Microsoft.Security", "CA2102:CatchNonClsCompliantExceptionsInGeneralHandlers")]
        public override object CreateInstance(ITypeDescriptorContext? context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException(nameof(propertyValues));
            }

            object? x = propertyValues["Color1"];
            object? y = propertyValues["Color2"];
            if (x == null || y == null || !(x is Color c1) || !(y is Color c2))
            {
                throw new ArgumentException("PropertyValueInvalidEntry");
            }
            return new DuoToneColor(c1, c2);
        }
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(DuoToneColor), attributes);
            return props.Sort(new string[] { "Color1", "Color2" });
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
        {
            return true;
        }
    }

}
