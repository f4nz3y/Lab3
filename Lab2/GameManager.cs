using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class GameManager
    {
        private PC ?userPC;
        private readonly List<IGame> installedGames = new List<IGame>();
        private readonly Dictionary<int, string> savedGames = new Dictionary<int, string>();
        private IGame ?runningGame = null;
        private int gameIdCounter = 1;

        private readonly GameEventNotifier _notifier = new GameEventNotifier();
        public GameEventNotifier Notifier => _notifier;

        public void InitializePC(double cpu, int ram, int gpu, int hdd, bool hasInternet, bool hasWheel)
        {
            userPC = new PC(cpu, ram, gpu, hdd, hasInternet, hasWheel);
            installedGames.Add(new OnlineCasino(0)); // Додаємо OnlineCasino за замовчуванням
        }

        public bool InstallGame(string genre)
        {
            if (userPC.HDD <= 0)
            {
                return false;
            }

            if (genre == "Стратегiя" && installedGames.Any(g => g.Genre == "Стратегiя"))
            {
                return false;
            }

            IGame newGame = GameFactory.CreateGame(genre, gameIdCounter++);
            if (newGame != null && userPC.HDD >= newGame.RequiredHDD)
            {
                installedGames.Add(newGame);
                userPC.HDD -= newGame.RequiredHDD;
                return true;
            }

            return false;
        }

        public bool StartGame(int gameIndex)
        {
            if (gameIndex < 0 || gameIndex >= installedGames.Count)
            {
                return false;
            }

            IGame selectedGame = installedGames[gameIndex];

            if (!userPC.CanRun(selectedGame))
            {
                return false;
            }

            if (selectedGame is OnlineCasino && !userPC.HasInternet)
            {
                return false;
            }

            runningGame = selectedGame;
            // Виклик події GameStarted
            _notifier.OnGameStarted(selectedGame.Genre, selectedGame is Simulator);
            return true;
        }

        public bool StopGame()
        {
            if (runningGame == null)
            {
                return false;
            }

            var stoppedGame = runningGame;
            runningGame = null;
            // Виклик події GameStopped
            _notifier.OnGameStopped(stoppedGame.Genre);
            return true;
        }

        public bool UninstallGame(int gameIndex)
        {
            if (gameIndex < 0 || gameIndex >= installedGames.Count)
            {
                return false;
            }

            IGame gameForRemoval = installedGames[gameIndex];

            if (gameForRemoval is OnlineCasino)
            {
                return false;
            }

            installedGames.RemoveAt(gameIndex);
            userPC.HDD += gameForRemoval.RequiredHDD;
            savedGames.Remove(gameForRemoval.Id);
            return true;
        }

        public bool SaveGame()
        {
            if (runningGame == null)
            {
                return false;
            }

            savedGames[runningGame.Id] = $"{runningGame.Genre} - збережено";
            // Виклик події GameSaved
            _notifier.OnGameSaved(runningGame.Genre);
            return true;
        }

        public bool LoadGame()
        {
            if (runningGame == null)
            {
                return false;
            }

            return savedGames.ContainsKey(runningGame.Id);
        }

        public void OverclockPC()
        {
            if (userPC != null)
            {
                userPC = new OverclockedPC(userPC);
                Console.WriteLine("ПК розгiняно! Процесор покращено на 20%.");
            }
        }

        public List<IGame> GetInstalledGames()
        {
            return installedGames;
        }

        public int GetFreeHDD()
        {
            return userPC.HDD;
        }

        public bool IsGameRunning()
        {
            return runningGame != null;
        }

        public bool IsSimulatorRunning()
        {
            return runningGame is Simulator;
        }

        public bool HasWheel()
        {
            return userPC.HasWheel;
        }
    }
}
