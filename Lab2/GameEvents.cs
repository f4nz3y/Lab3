using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Базовий клас для аргументів подій
    public abstract class GameEventBaseArgs : EventArgs
    {
        public string GameName { get; }
        protected GameEventBaseArgs(string gameName) => GameName = gameName;
    }

    // Подія запуску гри
    public class GameStartedEventArgs : GameEventBaseArgs
    {
        public bool IsSimulator { get; }
        public GameStartedEventArgs(string gameName, bool isSimulator) : base(gameName)
            => IsSimulator = isSimulator;
    }

    // Подія зупинки гри
    public class GameStoppedEventArgs : GameEventBaseArgs
    {
        public GameStoppedEventArgs(string gameName) : base(gameName) { }
    }

    // Подія збереження гри
    public class GameSavedEventArgs : GameEventBaseArgs
    {
        public GameSavedEventArgs(string gameName) : base(gameName) { }
    }
}