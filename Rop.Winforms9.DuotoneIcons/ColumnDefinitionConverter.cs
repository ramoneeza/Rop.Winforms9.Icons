using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Rop.Winforms9.DuotoneIcons;

public class ColumnDefinitionConverter : TypeConverter
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
                return ColumnDefinition.Parse(text);
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
        if (value is ColumnDefinition pt)
        {
            if (destinationType == typeof(string))
            {
                return pt.ToString();
            }
            if (destinationType == typeof(InstanceDescriptor))
            {
                ConstructorInfo? ctor = typeof(ColumnDefinition).GetConstructor(new Type[] { typeof(ContentAlignment), typeof(int),typeof(string),typeof(bool),typeof(string) });
                if (ctor != null)
                {
                    return new InstanceDescriptor(ctor, new object[] { pt.TextAlign, pt.Width,pt.Text,pt.Selectable,pt.ToolTipText });
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

        object? x1 = propertyValues["TextAlign"];
        object? x2= propertyValues["Width"];
        object? x3= propertyValues["Text"];
        object? x4 = propertyValues["Selectable"];
        object? x5 = propertyValues["ToolTipText"];
        
        if (x1 is not ContentAlignment ca || x2 is not int w || x3 is not string t || x4 is not bool s || x5 is not string ttt )
        {
            throw new ArgumentException("PropertyValueInvalidEntry");
        }
        return new ColumnDefinition(ca,w,t,s,ttt);
    }
    public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context)
    {
        return true;
    }
    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(ColumnDefinition), attributes);
        return props.Sort(new string[] { "TextAlign", "Width","Text","Selectable","ToolTipText" });
    }
    public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
    {
        return true;
    }
}