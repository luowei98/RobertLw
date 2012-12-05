#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: RobertLw.Windows.WinForm
// File:    HotkeyBox.cs
// 
// Create by Robert.L at 2012/11/17 10:06
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.Windows.Forms;
using RobertLw.WinOS;


namespace RobertLw.Windows.WinFormEx
{
    public class HotkeyBox : TextBox
    {
        private HotkeyValue keyValue;
        private bool winkey;

        public HotkeyValue Value
        {
            get { return keyValue; }
            set
            {
                keyValue = value;
                Text = keyValue == null ? "" : keyValue.ToString();
                SelectionStart = Text.Length;
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin)
                winkey = true;

            Value = new HotkeyValue
                    {
                        Ctrl = e.Control,
                        Shift = e.Shift,
                        Alt = e.Alt,
                        Win = winkey,
                        Key = e.KeyCode,
                    };
            Text = Value.ToString();
            SelectionStart = Text.Length;

            e.SuppressKeyPress = false;
            e.Handled = true;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            winkey = false;
            if (Value == null || Value.Key == Keys.None)
            {
                Text = "";
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            winkey = false;
            if (Value == null || Value.Key == Keys.None)
            {
                Text = "";
            }
        }

        #region Nested type: HotkeyValue

        public class HotkeyValue
        {
            private Keys key = Keys.None;
            private string text = "";

            public HotkeyValue(string value = null)
            {
                if (value == null) return;

                Ctrl = value.Contains("Ctrl");
                Shift = value.Contains("Shift");
                Alt = value.Contains("Alt");
                Win = value.Contains("Win");

                string ks = value.Substring(value.LastIndexOf("+") + 1);
                Keys k;
                if (!Enum.TryParse(ks, out k)) return;
                Key = k;
            }

            public bool Ctrl { get; set; }
            public bool Shift { get; set; }
            public bool Alt { get; set; }
            public bool Win { get; set; }

            public GlobalHotkey.KeyModifiers Modifiers
            {
                get
                {
                    var m = GlobalHotkey.KeyModifiers.None;
                    if (Ctrl) m |= GlobalHotkey.KeyModifiers.Control;
                    if (Shift) m |= GlobalHotkey.KeyModifiers.Shift;
                    if (Alt) m |= GlobalHotkey.KeyModifiers.Alt;
                    if (Win) m |= GlobalHotkey.KeyModifiers.Win;
                    return m;
                }
            }

            public Keys Key
            {
                get { return key; }
                set
                {
                    key = value;
                    if (key >= Keys.A && key <= Keys.Z)
                    {
                        text = key.ToString();
                    }
                    else if (key >= Keys.D0 && key <= Keys.D9)
                    {
                        text = key.ToString().Remove(0, 1);
                    }
                    else if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
                    {
                        text = key.ToString().Replace("Pad", "");
                    }
                    else if (key >= Keys.F1 && key <= Keys.F12)
                    {
                        text = key.ToString();
                    }
                    else
                    {
                        text = "";
                        key = Keys.None;
                    }
                }
            }

            public override string ToString()
            {
                return (Ctrl ? "Ctrl+" : "") +
                       (Shift ? "Shift+" : "") +
                       (Alt ? "Alt+" : "") +
                       (Win ? "Win+" : "") +
                       text;
            }
        }

        #endregion
    }
}