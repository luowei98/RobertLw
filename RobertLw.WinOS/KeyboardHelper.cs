#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: RobertLw.RobertLw.WinOS
// File:    KeyboardHelper.cs
// 
// Create by Robert.L at 2012/11/30 19:39
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using RobertLw.WinOS.Api;


namespace RobertLw.WinOS
{
    public class KeyboardHelper
    {
        public static void AutoInput(char c)
        {
            short k = User.VkKeyScan((byte)c);

            User.keybd_event((byte)k, 0, 0, 0);
            User.keybd_event((byte)k, 0, User.KEYEVENTF_KEYUP, 0);
        }
    }
}