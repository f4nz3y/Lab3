using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class GameEventNotifier
    {
        // Події
        public event EventHandler<GameStartedEventArgs> GameStarted;
        public event EventHandler<GameStoppedEventArgs> GameStopped;
        public event EventHandler<GameSavedEventArgs> GameSaved;

        // Методи для виклику подій
        public void OnGameStarted(string gameName, bool isSimulator)
            => GameStarted?.Invoke(this, new GameStartedEventArgs(gameName, isSimulator));

        public void OnGameStopped(string gameName)
            => GameStopped?.Invoke(this, new GameStoppedEventArgs(gameName));

        public void OnGameSaved(string gameName)
            => GameSaved?.Invoke(this, new GameSavedEventArgs(gameName));
    }
}