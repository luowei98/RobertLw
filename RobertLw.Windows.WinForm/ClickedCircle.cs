using System;
using System.Drawing;
using RobertLw.Windows.WinForm.Properties;


namespace RobertLw.Windows.WinForm
{
    public partial class ClickedCircle : AlphaForm
    {
        private int hideSpeed = 5;
        private int hideTime = 500;

        public ClickedCircle(Bitmap circle)
            : this(new[] {circle})
        {
        }

        public ClickedCircle(Bitmap[] circles = null)
        {
            InitializeComponent();

            ShowInTaskbar = false;

            AutoHide = true;

            if (circles == null)
            {
                circles = new[]
                          {
                              Resources.clicked_circle00,
                              Resources.clicked_circle01,
                              Resources.clicked_circle02,
                              Resources.clicked_circle03,
                              Resources.clicked_circle04,
                              Resources.clicked_circle05,
                              Resources.clicked_circle06,
                              Resources.clicked_circle07,
                              Resources.clicked_circle08,
                              Resources.clicked_circle09,
                              Resources.clicked_circle10,
                              Resources.clicked_circle11,
                              Resources.clicked_circle12,
                              Resources.clicked_circle13,
                              Resources.clicked_circle14,
                              Resources.clicked_circle15,
                              Resources.clicked_circle16,
                              Resources.clicked_circle17,
                              Resources.clicked_circle18,
                              Resources.clicked_circle19,
                              Resources.clicked_circle20,
                              Resources.clicked_circle21,
                              Resources.clicked_circle22,
                              Resources.clicked_circle23,
                              Resources.clicked_circle24,
                              Resources.clicked_circle25,
                              Resources.clicked_circle26,
                              Resources.clicked_circle27,
                          };
            }
            SetCircles(circles);
        }

        public bool AutoHide { get; set; }

        public int HideTime
        {
            get { return hideTime; }
            set
            {
                hideTime = value;

                if (value >= timer.Interval && value <= 255*timer.Interval)
                {
                    hideSpeed = 255/(value/timer.Interval);
                }
                else
                {
                    hideTime = 1000;
                }
            }
        }

        public Bitmap[] Circles { get; private set; }

        public void SetCircles(Bitmap[] circles)
        {
            if (circles == null || circles.Length == 0) return;

            Circles = new Bitmap[255];
            int step = circles.Length > 255 ? 0 : 255/circles.Length;
            for (int i = 0, j = 0; i < 255 && j < circles.Length; i += step, j++)
            {
                Circles[i] = circles[j];
            }
            Bitmap bm = Circles[0];
            for (int i = 1; i < Circles.Length; i++)
            {
                if (Circles[i] == null)
                {
                    Circles[i] = bm;
                }
                else
                {
                    bm = Circles[i];
                }
            }

            SetView(Circles[0], 255);
        }

        private void ClickedCircle_Load(object sender, EventArgs e)
        {
            if (AutoHide) timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Opacity < hideSpeed)
            {
                Close();
                return;
            }
            if (Circles != null && Circles.Length >= Opacity && Circles[255 - Opacity] != null)
            {
                SetView(Circles[255 - Opacity], Opacity - hideSpeed);
            }
            else
            {
                SetView(Opacity - hideSpeed);
            }
        }
    }
}