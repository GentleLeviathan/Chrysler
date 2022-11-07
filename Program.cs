using System;
using System.Windows.Forms;

namespace Chrysler
{
    static class Program
    {
        public static string sourceVersion = "https://raw.githubusercontent.com/GentleLeviathan/Chrysler/main/version";
        public static string updateResult;

        [STAThread]
        static void Main()
        {
            updateResult = UpdateCheck.Execute();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
