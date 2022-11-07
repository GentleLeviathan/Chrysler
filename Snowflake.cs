using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using System;
using System.Drawing;

namespace Chrysler
{
    struct Snowflake
    {
        /// <summary>
        /// Rounded to int size for System.Graphics
        /// </summary>
        public Size size;
        /// <summary>
        /// Rounded to int x,y position for System.Graphics
        /// </summary>
        public Point position;

        public double x, y;
        /// <summary>
        /// Angle in radians for Sin/Cos x-offset
        /// </summary>
        public double step;
        /// <summary>
        /// Mass of snowflake, not related to the size (yet)
        /// </summary>
        public double mass;

        private int width;
        private int height;
        private int maxWidth;
        private int maxHeight;

        /// <summary>
        /// Initialize a snowflake with the parameters for Screen Width, Screen Height, Sprite Width, Sprite Height
        /// </summary>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="spriteWidth"></param>
        /// <param name="spriteHeight"></param>
        public Snowflake(int maxWidth, int maxHeight, int spriteWidth, int spriteHeight)
        {
            this.x = (int)Math.Round(Normal.Sample(maxWidth / 2, maxWidth / 2));
            this.y = -spriteWidth * 0.9;
            this.position = new Point((int)x, (int)y);
            this.mass = SystemRandomSource.Default.NextDouble() * 2;
            this.mass = this.mass < 0.8 ? SystemRandomSource.Default.NextDouble() + 0.8 : this.mass;
            double randSize = (Normal.Sample(5, 1)) * 0.2;
            this.size = new Size((int)Math.Round(spriteWidth / 8 * randSize), (int)Math.Round(spriteHeight / 8 * randSize));
            this.step = 0;

            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            this.width = spriteWidth;
            this.height = spriteHeight;
        }

        /// <summary>
        /// Reset the snowflake with new distributed/random parameters and place it at the top of the screen
        /// </summary>
        public void Reset()
        {
            this.x = (int)Math.Round(Normal.Sample(maxWidth / 2, maxWidth / 2));
            this.y = -width * 0.9;
            this.mass = SystemRandomSource.Default.NextDouble() * 2;
            this.mass = this.mass < 0.8 ? SystemRandomSource.Default.NextDouble() + 0.8 : this.mass;
            double randSize = (Normal.Sample(5, 1)) * 0.2;
            this.size = new Size((int)Math.Round(width / 8 * randSize), (int)Math.Round(height / 8 * randSize));
            this.step = 0;
        }

        /// <summary>
        /// Perform an update on the snowflake to move it along the screen
        /// </summary>
        public void Update()
        {
            this.step += SystemRandomSource.Default.NextDouble() * 0.01;
            this.x--;
            this.x -= Math.Cos(step) * 1;
            if (this.x < -width)
            {
                Reset();
            }

            this.y += mass;
            if (this.y > maxHeight + height)
            {
                Reset();
            }

            this.position.X = (int)Math.Round(x);
            this.position.Y = (int)Math.Round(y);
        }
    }
}
