#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: RobertLw.Windows.WinFormEx
// File:    ListViewHeaderCheckboxEx.cs
// 
// Create by Robert.L at 2012/11/22 11:31
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using RobertLw.Windows.WinFormEx.Properties;


namespace RobertLw.Windows.WinFormEx
{
    public class ListViewHeaderCheckboxEx : ListView
    {
        public ListViewHeaderCheckboxEx()
        {
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            MultiSelect = true;
            OwnerDraw = true;
            CheckBoxes = true;
            FullRowSelect = true;
            View = View.Details;

            Columns.Add(new ColumnHeader {Name = "ColumnCheckbox", Text = "", Width = 30});
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            base.OnDrawColumnHeader(e);
            if (e.ColumnIndex == 0)
            {
                var headerCheckBox = new CheckBox {Text = "", Visible = true};
                SuspendLayout();
                e.DrawBackground();
                headerCheckBox.BackColor = Color.Transparent;
                headerCheckBox.UseVisualStyleBackColor = true;
                headerCheckBox.BackgroundImage = Resources.ListViewHeaderCheckboxBackgroud;
                headerCheckBox.SetBounds(e.Bounds.X, e.Bounds.Y,
                                         headerCheckBox.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).
                                             Width,
                                         headerCheckBox.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).
                                             Height);
                headerCheckBox.Size =
                    new Size(headerCheckBox.GetPreferredSize(new Size(e.Bounds.Width - 1, e.Bounds.Height)).Width + 1,
                             e.Bounds.Height);
                headerCheckBox.Location = new Point(4, 0);
                Controls.Add(headerCheckBox);
                headerCheckBox.Show();
                headerCheckBox.BringToFront();
                e.DrawText(TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                headerCheckBox.CheckedChanged += OnHeaderCheckboxCheckedChanged;
                ResumeLayout(true);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void OnHeaderCheckboxCheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem i in Items)
            {
                i.Checked = ((CheckBox) sender).Checked;
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);
            e.DrawDefault = true;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);
            e.DrawDefault = true;
        }
    }
}