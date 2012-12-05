namespace RobertLw.Windows.WinForm
{
    partial class ClickedCircle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private global::System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new global::System.ComponentModel.Container();
            this.timer = new global::System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new global::System.EventHandler(this.timer_Tick);
            // 
            // ClickedCircle
            // 
            this.AutoScaleDimensions = new global::System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new global::System.Drawing.Size(300, 300);
            this.Name = "ClickedCircle";
            this.Text = "ClickedCircle";
            this.Load += new global::System.EventHandler(this.ClickedCircle_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private global::System.Windows.Forms.Timer timer;
    }
}