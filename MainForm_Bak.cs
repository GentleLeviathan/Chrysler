using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MathNet.Numerics.Distributions;

namespace Chrysler
{
    public partial class MainForm : Form
    {
        private Image snowSprite = Image.FromFile("snowflakes_s.png");
        private Image snowSpriteSingle = Image.FromFile("snowflake_single_1.png");
        private Image snowAccum = Image.FromFile("snowaccum.png");

        double[] steps;
        int[] xPos, yPos;
        int screenWidth, screenHeight;
        int xCount, yCount;

        int accumCount;
        int[] accumPos;
        float accumHeight;
        double step = 30;
        bool singleSprite = true;

        private int oldWindowLong;
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        Random rand;
        Normal normalDist;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            rand = new Random(51563);
            normalDist = new Normal();
            AllowTransparency = true;
            Opacity = 0.65;
            SetFormTransparent(this.Handle);

            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;

            accumHeight = screenHeight;
            accumCount = (int)Math.Round((float)screenWidth / snowAccum.Width) + 2;
            accumPos = new int[accumCount];

            for (int i = 0; i < accumPos.Length; i++)
            {
                accumPos[i] = snowAccum.Width * i;
            }

            if (singleSprite)
            {
                SetupSingle();
                return;
            }
            SetupMulti();
        }

        private void SetupMulti()
        {
            xCount = (int)Math.Round((float)screenWidth / snowSprite.Width) + 2;
            yCount = (int)Math.Round((float)screenHeight / snowSprite.Height) + 2;

            steps = new double[xCount];
            xPos = new int[xCount];
            yPos = new int[yCount];

            for (int i = 0; i < xPos.Length; i++)
            {
                xPos[i] = snowSprite.Width * i;
            }
            for (int i = 0; i < yPos.Length; i++)
            {
                yPos[i] = snowSprite.Height * i;
            }
        }

        private int random(int min, int max)
        {
            return (int)Math.Round(min + rand.NextDouble() * (max - min));
        }

        private void SetupSingle()
        {
            xCount = 128;
            yCount = 128;

            steps = new double[xCount];
            xPos = new int[xCount];
            yPos = new int[yCount];

            for (int i = 0; i < xPos.Length; i++)
            {
                xPos[i] = (int)Math.Round(Normal.Sample(screenWidth / 2, screenWidth / 2));
            }
            for (int i = 0; i < yPos.Length; i++)
            {
                yPos[i] = (int)Math.Round(Normal.Sample(screenHeight / 2, screenHeight / 2));
                System.Diagnostics.Debug.WriteLine(yPos[i]);
            }
        }

        public void SetFormTransparent(IntPtr Handle)
        {
            oldWindowLong = GetWindowLong(Handle, -20);
            SetWindowLong(Handle, -20, Convert.ToInt32(oldWindowLong | 0x00080000 | 0x00000020L));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            if (singleSprite)
            {
                PaintSingle(e);
                return;
            }
            PaintMulti(e);
        }

        private void PaintSingle(PaintEventArgs e)
        {
            for (int i = 0; i < xPos.Length; i++)
            {
                e.Graphics.DrawImage(snowSpriteSingle, new Rectangle(new Point(xPos[i], yPos[i]), new Size(snowSpriteSingle.Width / 8, snowSpriteSingle.Height / 8)));
            }
        }

        private void PaintMulti(PaintEventArgs e)
        {
            for (int i = 0; i < xPos.Length; i++)
            {
                for (int j = 0; j < yPos.Length; j++)
                {
                    e.Graphics.DrawImage(snowSprite, new Rectangle(new Point(xPos[i], yPos[j]), new Size(snowSprite.Width, snowSprite.Height)));
                }
            }
            for (int i = 0; i < accumPos.Length; i++)
            {
                e.Graphics.DrawImage(snowAccum, new Rectangle(new Point(accumPos[i], (int)accumHeight), new Size(snowAccum.Width, snowAccum.Height)));
            }
        }

        private void Translate_Tick(object sender, EventArgs e)
        {
            Refresh();
            accumHeight -= 0.01f;
            accumHeight = accumHeight < screenHeight - (snowAccum.Height / 2) ? screenHeight - (snowAccum.Height / 2) : accumHeight;

            if (singleSprite)
            {
                for (int i = 0; i < xPos.Length; i++)
                {
                    steps[i] += rand.NextDouble() * 0.01;
                    xPos[i]--;
                    xPos[i] -= (int)(Math.Cos(steps[i]) * 4);
                    if (xPos[i] < -snowSpriteSingle.Width)
                    {
                        xPos[i] = screenWidth + snowSpriteSingle.Width;
                    }
                }
                for (int i = 0; i < yPos.Length; i++)
                {
                    yPos[i] += 2;
                    if (yPos[i] > screenHeight + snowSpriteSingle.Height)
                    {
                        yPos[i] = -snowSpriteSingle.Height;
                    }
                }
                return;
            }
            for (int i = 0; i < xPos.Length; i++)
            {
                xPos[i] -= 1;
                if(xPos[i] < -snowSprite.Width)
                {
                    xPos[i] = screenWidth + snowSprite.Width;
                }
            }
            for (int i = 0; i < yPos.Length; i++)
            {
                yPos[i] += 2;
                if (yPos[i] > screenHeight + snowSprite.Height)
                {
                    yPos[i] = -snowSprite.Height;
                }
            }
        }
    }
}
