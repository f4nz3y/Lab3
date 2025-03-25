using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Клас для передачі даних про подію
    class GameEventArgs : EventArgs
    {
        public string GameName { get; }
        public bool IsSimulator { get; }

        public GameEventArgs(string gameName, bool isSimulator)
        {
            GameName = gameName;
            IsSimulator = isSimulator;
        }
    }
}
