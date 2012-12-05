using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using RobertLw.WinOS.Api;


namespace RobertLw.Windows.WinForm
{
    public partial class AlphaForm : Form
    {
        public AlphaForm(Bitmap bitmap = null, int opacity = 255)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;

            if (bitmap != null)
                SetView(bitmap, opacity);
        }

        public Bitmap View { get; private set; }

        public new int Opacity { get; private set; }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                    cp.ExStyle |= User.WS_EX_LAYERED;
                return cp;
            }
        }

        public void SetView(Bitmap newView, int newOpacity)
        {
            if (newView == null) return;

            if (newView.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            View = newView;
            Opacity = newOpacity%256;

            IntPtr screenDc = User.GetDC(IntPtr.Zero);
            IntPtr memDc = GDI.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = newView.GetHbitmap(Color.FromArgb(0));
                oldBitmap = GDI.SelectObject(memDc, hBitmap);

                var size = new SIZE {cx = newView.Width, cy = newView.Height};
                var pointSource = new POINT {x = 0, y = 0};
                var topPos = new POINT {x = Left, y = Top};
                var blend = new User.BLENDFUNCTION
                            {
                                BlendOp = User.AC_SRC_OVER,
                                BlendFlags = 0,
                                SourceConstantAlpha = (byte) newOpacity,
                                AlphaFormat = User.AC_SRC_ALPHA
                            };

                User.UpdateLayeredWindow(Handle, screenDc, ref topPos,
                                         ref size, memDc, ref pointSource,
                                         0,
                                         ref blend, User.ULW_ALPHA);
            }
            finally
            {
                User.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    GDI.SelectObject(memDc, oldBitmap);
                    GDI.DeleteObject(hBitmap);
                }
                GDI.DeleteDC(memDc);
            }
        }

        public void SetView(Bitmap newView)
        {
            SetView(newView, Opacity);
        }

        public void SetView(int newOpacity)
        {
            SetView(View, newOpacity);
        }

        public void SetView()
        {
            SetView(View, Opacity);
        }
    }
}