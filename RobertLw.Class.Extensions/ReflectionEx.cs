#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: RobertLw.Class.Extensions
// File:    ReflectionEx.cs
// 
// Create by Robert.L at 2012/11/27 10:23
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace RobertLw.Class.Extensions
{
    public static class ReflectionEx
    {
        // var attr = p.GetAttribute<DisplayNameAttribute>(false);
        // if (attr != null && attr.DisplayName == name)
        //     p.SetValue(currentAction, e.ChangedItem.Value, null);
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
            where T : Attribute
        {
            object attribute = member.GetCustomAttributes(typeof (T), false).SingleOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof (T).Name,
                        member.Name));
            }

            return (T) attribute;
        }

        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            MemberInfo memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var attr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            return attr == null ? memberInfo.Name : attr.DisplayName;
        }

        public static string GetPropertyDescription<T>(Expression<Func<T, object>> propertyExpression)
        {
            MemberInfo memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var attr = memberInfo.GetAttribute<DescriptionAttribute>(false);
            return attr == null ? memberInfo.Name : attr.Description;
        }

        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            Debug.Assert(propertyExpression != null, "propertyExpression != null");
            var memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                var unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }
    }
}