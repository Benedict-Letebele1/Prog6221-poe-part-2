using System;
using System.Windows.Forms;

namespace CyberSecurityChatbot_POE
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Start the Windows Forms application
            ApplicationConfiguration.Initialize();

            Application.Run(new Form1());
        }
    }
}