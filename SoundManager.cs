using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CyberSec_UI
{
    class SoundManager
    {
        public static void PlaySound(string filePath)
        {
            try
            {
                //load and play the sound synchronously
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.Load();
                    player.PlaySync(); // Play the sound synchronously
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, display an error message in red
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error playing sound: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
