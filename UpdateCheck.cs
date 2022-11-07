using System.IO;

namespace Chrysler
{
    static class UpdateCheck
    {
        public static string Execute()
        {
            string currentVersion = File.ReadAllText("version");
            string masterVersion = currentVersion;
            System.Net.WebClient client = new System.Net.WebClient();
            try
            {
                masterVersion = client.DownloadString(new System.Uri(Program.sourceVersion));
            }
            catch (System.Net.WebException)
            {
                return "UPDATECHECK FAILED";
            }

            if(!masterVersion.Contains(currentVersion))
            {
                System.Diagnostics.Debug.WriteLine("UPDATE AVAILABLE!");
                return "UPDATE AVAILABLE!";
            }

            System.Diagnostics.Debug.WriteLine("UP TO DATE!");
            return "UP TO DATE!";
        }
    }
}
