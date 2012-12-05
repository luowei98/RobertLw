#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: RobertLw.System
// File:    GlobalHotkey.cs
// 
// Create by Robert.L at 2012/11/05 22:20
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.Collections;
using System.Windows.Forms;
using RobertLw.WinOS.Api;


namespace RobertLw.WinOS
{
    public class GlobalHotkey : IMessageFilter
    {
        #region Delegates

        public delegate void HotkeyEventHandler(int hotkeyId);

        #endregion

        #region KeyModifiers enum

        /// <summary>
        /// 辅助按键
        /// </summary>
        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            Alt = User.MOD_ALT,
            Control = User.MOD_CONTROL,
            Shift = User.MOD_SHIFT,
            Win = User.MOD_WIN
        }

        #endregion

        private readonly IntPtr hWnd;
        private readonly Hashtable keyIDs = new Hashtable();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hWnd">当前句柄</param>
        public GlobalHotkey(IntPtr hWnd)
        {
            this.hWnd = hWnd;
            Application.AddMessageFilter(this);
        }

        #region IMessageFilter Members

        /// <summary>
        /// 消息筛选
        /// </summary>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == User.WM_HOTKEY)
            {
                if (OnHotkey != null)
                {
                    foreach (int key in keyIDs.Values)
                    {
                        if ((int) m.WParam == key)
                        {
                            OnHotkey((int) m.WParam);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        public event HotkeyEventHandler OnHotkey;

        /// <summary>
        /// 注册热键
        /// </summary>
        public int RegisterHotkey(Keys key, KeyModifiers keyflags)
        {
            int hotkeyid = Kernel.GlobalAddAtom(Guid.NewGuid().ToString());
            User.RegisterHotKey(hWnd, hotkeyid, (int) keyflags, (int) key);
            keyIDs.Add(hotkeyid, hotkeyid);
            return hotkeyid;
        }

        /// <summary>
        /// 注销所有热键
        /// </summary>
        public void UnregisterHotkeys()
        {
            Application.RemoveMessageFilter(this);
            foreach (int key in keyIDs.Values)
            {
                User.UnregisterHotKey(hWnd, key);
                Kernel.GlobalDeleteAtom((short) key);
            }
        }
    }
}