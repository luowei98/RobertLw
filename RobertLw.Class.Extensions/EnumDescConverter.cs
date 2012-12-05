#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: RobertLw.Class.Extensions
// File:    EnumDescConverter.cs
// 
// Create by Robert.L at 2012/12/04 10:45
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.ComponentModel;
using System.Globalization;


namespace RobertLw.Class.Extensions
{
    /// <summary>
    /// EnumConverter supporting System.ComponentModel.DescriptionAttribute
    /// </summary>
    public class EnumDescConverter : EnumConverter
    {
        private readonly Type val;

        public EnumDescConverter(Type type) : base(type)
        {
            val = type;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            if (value is Enum && destinationType == typeof (string))
            {
                return EnumEx.GetEnumDescription((Enum) value);
            }
            if (value is string && destinationType == typeof (string))
            {
                return EnumEx.GetEnumDescription(val, (string) value);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                return EnumEx.GetEnumValue(val, (string) value);
            }
            if (value is Enum)
                EnumEx.GetEnumDescription((Enum) value);

            return base.ConvertFrom(context, culture, value);
        }
    }
}