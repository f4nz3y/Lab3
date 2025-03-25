using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Логування подій
    public class GameLogger
    {
        public void Subscribe(GameEventNotifier notifier)
        {
            notifier.GameStarted += (sender, e)
                => Console.WriteLine($"Гра {e.GameName} запущена.");
            notifier.GameStopped += (sender, e)
                => Console.WriteLine($"Гра {e.GameName} зупинена.");
            notifier.GameSaved += (sender, e)
                => Console.WriteLine($"Гра {e.GameName} збережена.");
        }
    }
}