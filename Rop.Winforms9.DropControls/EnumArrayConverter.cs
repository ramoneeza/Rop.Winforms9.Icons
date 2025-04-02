using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DropControls;

public class EnumArrayConverter<T> : TypeConverter where T:Enum
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }
    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string str)
        {
            var values = str.Split([','], StringSplitOptions.RemoveEmptyEntries);
            var result = new DropControlStatus[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                result[i] = (DropControlStatus)Enum.Parse(typeof(T), values[i].Trim());
            }
            return result;
        }
        return Array.Empty<T>();
    }
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(string))
        {
            return true;
        }
        return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is T[] array)
        {
            return string.Join(", ", array);
        }
        return Array.Empty<T>();
    }
}
