#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: RobertLw.System
// File:    MouseHelper.cs
// 
// Create by Robert.L at 2012/11/06 10:51
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.Drawing;
using RobertLw.WinOS.Api;
using RobertLw.Windows.WinForm;


namespace RobertLw.WinOS
{
    public class MouseHelper
    {
        public static void AutoClick(MouseCircleVisible visible = MouseCircleVisible.Show)
        {
            User.mouse_event(User.MOUSEEVENTF_LEFTDOWN | User.MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);
            User.mouse_event(User.MOUSEEVENTF_LEFTUP | User.MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);

            if (visible ==  MouseCircleVisible.Show)
                ShowClickedCircle(GetPosition());
        }

        public static void AutoClick(int x, int y, MouseCircleVisible visible = MouseCircleVisible.Show)
        {
            POINT p;
            User.GetCursorPos(out p);
            try
            {
                User.SetCursorPos(x, y);
                User.mouse_event(User.MOUSEEVENTF_LEFTDOWN | User.MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);
                User.mouse_event(User.MOUSEEVENTF_LEFTUP | User.MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);

                if (visible ==  MouseCircleVisible.Show)
                    ShowClickedCircle(GetPosition());
            }
            finally
            {
                User.SetCursorPos(p.x, p.y);
            }
        }

        public static Point GetPosition()
        {
            POINT p;
            User.GetCursorPos(out p);
            return new Point(p.x, p.y);
        }

        private static void ShowClickedCircle(Point point, int hidetime = 0)
        {
            var frm = new ClickedCircle();
            if (hidetime != 0) frm.HideTime = hidetime;

            frm.Location = new Point(point.X - frm.View.Width/2 + 1, point.Y - frm.View.Height/2 + 1);
            frm.Show();
        }
    }

    public enum MouseCircleVisible
    {
        Show,
        Hide,
    }
}