#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: RobertLw.Class.Extensions
// File:    OrderedDisplayNameAttribute.cs
// 
// Create by Robert.L at 2012/12/05 11:09
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.ComponentModel;


namespace RobertLw.Class.Extensions
{
    public class OrderedDisplayNameAttribute : DisplayNameAttribute
    {
        public OrderedDisplayNameAttribute(int position, string displayName)
        {
            base.DisplayNameValue = new String('\t', position) + displayName;
        }
    }
}