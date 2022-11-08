using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chrysler
{
    public partial class MainForm : Form
    {
        private DateTime christmasDay;
        private Image flakeSprite = Image.FromFile("snowflake_single_1.png");
        private Image snowAccum = Image.FromFile("snowaccum.png");

        private int screenWidth, screenHeight;

        private int accumCount;
        private int[] accumPos;
        private float accumHeight;

        private Snowflake[] flakes;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AllowTransparency = true;
            Opacity = 0.65;

            christmasDay = new DateTime(DateTime.Now.Year, 12, 25);
            Countdown.Text = Program.updateResult + " - " + (christmasDay.DayOfYear - System.DateTime.Now.DayOfYear) + " DAYS";
            System.Diagnostics.Debug.WriteLine(Program.updateResult);

            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;

            accumHeight = screenHeight;
            accumCount = (int)Math.Round((float)screenWidth / snowAccum.Width) + 2;
            accumPos = new int[accumCount];

            for (int i = 0; i < accumPos.Length; i++)
            {
                accumPos[i] = snowAccum.Width * i;
            }

            flakes = new Snowflake[128];
            for(int i = 0; i < flakes.Length; i++)
            {
                flakes[i] = new Snowflake(screenWidth, screenHeight, flakeSprite.Width, flakeSprite.Height);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            PaintSprites(e);
        }

        private void PaintSprites(PaintEventArgs e)
        {
            for (int i = 0; i < flakes.Length; i++)
            {
                e.Graphics.DrawImage(flakeSprite, new Rectangle(flakes[i].position, flakes[i].size));
            }

            for (int i = 0; i < accumPos.Length; i++)
            {
                e.Graphics.DrawImage(snowAccum, new Rectangle(new Point(accumPos[i], (int)accumHeight), new Size(snowAccum.Width, snowAccum.Height)));
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            Countdown.Text = christmasDay.DayOfYear - System.DateTime.Now.DayOfYear + " DAYS";
        }

        private void Translate_Tick(object sender, EventArgs e)
        {
            Refresh();
            accumHeight -= 0.0025f;
            accumHeight = accumHeight < screenHeight - (snowAccum.Height / 3) ? screenHeight - (snowAccum.Height / 3) : accumHeight;

            for (int i = 0; i < flakes.Length; i++)
            {
                flakes[i].Update();
            }
        }
    }
}
