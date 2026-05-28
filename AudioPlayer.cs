using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CyberSecurityChatbot_POE
{
    public static class AudioPlayer
    {
        public static void PlayGreeting(string filePath)
        {
            // Check if the audio file exists
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Audio file not found: " + filePath);
                return;
            }

            try
            {
                // Create and play the greeting audio
                SoundPlayer player = new SoundPlayer(filePath);
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Audio error: " + ex.Message);
            }
        }
    }
}