namespace Chrysler
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.Translate = new System.Windows.Forms.Timer(this.components);
            this.Countdown = new System.Windows.Forms.Label();
            this.CountdownTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Translate
            // 
            this.Translate.Enabled = true;
            this.Translate.Interval = 10;
            this.Translate.Tick += new System.EventHandler(this.Translate_Tick);
            // 
            // Countdown
            // 
            this.Countdown.BackColor = System.Drawing.Color.Transparent;
            this.Countdown.CausesValidation = false;
            this.Countdown.Dock = System.Windows.Forms.DockStyle.Top;
            this.Countdown.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Countdown.ForeColor = System.Drawing.Color.DarkGray;
            this.Countdown.Location = new System.Drawing.Point(0, 0);
            this.Countdown.Name = "Countdown";
            this.Countdown.Size = new System.Drawing.Size(800, 13);
            this.Countdown.TabIndex = 0;
            this.Countdown.Text = "25 DAYS";
            this.Countdown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // CountdownTimer
            // 
            this.CountdownTimer.Enabled = true;
            this.CountdownTimer.Interval = 60000;
            this.CountdownTimer.Tick += new System.EventHandler(this.CountdownTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Countdown);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Translate;
        private System.Windows.Forms.Label Countdown;
        private System.Windows.Forms.Timer CountdownTimer;
    }
}

