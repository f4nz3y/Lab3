using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class ConsoleUI
    {
        private readonly GameManager gameManager;

        public ConsoleUI()
        {
            gameManager = new GameManager();
            // Підписка на події
            gameManager.GameStarted += OnGameStarted;
            gameManager.GameStopped += OnGameStopped;
            gameManager.GameSaved += OnGameSaved;
        }

        public void Run()
        {
            InitializePC();
            MainMenu();
        }

        private void InitializePC()
        {
            Console.Write("Чи є пiдключення до iнтернету? (1 - так, 0 - нi): ");
            bool hasInternet = Console.ReadLine().Trim() == "1";

            Console.Write("Чи є пiдключене кермо? (1 - так, 0 - нi): ");
            bool hasWheel = Console.ReadLine().Trim() == "1";

            Console.WriteLine("Введiть характеристики вашого ПК:");
            double cpu = ReadDouble("Процесор (GHz): ");
            int ram = ReadInt("Оперативна пам'ять (GB): ");
            int gpu = ReadInt("Вiдеокарта (GB): ");
            int hdd = ReadInt("Вiльне мiсце на HDD (GB): ");

            gameManager.InitializePC(cpu, ram, gpu, hdd, hasInternet, hasWheel);
        }

        private void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nОберiть дiю:");
                Console.WriteLine("1. Встановити гру");
                Console.WriteLine("2. Запустити гру");
                Console.WriteLine("3. Зупинити гру");
                Console.WriteLine("4. Видалити гру");
                Console.WriteLine("5. Переглянути встановленi iгри");
                Console.WriteLine("6. Зберегти гру");
                Console.WriteLine("7. Завантажити гру");
                Console.WriteLine("8. Вихiд");
                string ?choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        InstallGame();
                        break;
                    case "2":
                        StartGame();
                        break;
                    case "3":
                        StopGame();
                        break;
                    case "4":
                        UninstallGame();
                        break;
                    case "5":
                        ShowInstalledGames();
                        break;
                    case "6":
                        SaveGame();
                        break;
                    case "7":
                        LoadGame();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Некоректний вибiр!");
                        break;
                }
            }
        }

        private void InstallGame()
        {
            Console.Write("Оберiть жанр гри (1 - Стратегiя, 2 - Симулятор): ");
            string genre = Console.ReadLine().Trim() == "1" ? "Стратегiя" : "Симулятор";

            if (gameManager.InstallGame(genre))
            {
                Console.WriteLine($"{genre} встановлено.");
            }
            else
            {
                Console.WriteLine("Не вдалося встановити гру.");
            }
        }

        private void StartGame()
        {
            if (gameManager.IsGameRunning())
            {
                Console.WriteLine("Спочатку зупинiть поточну гру!");
                return;
            }

            ShowInstalledGames();
            int gameIndex = ReadInt("Оберiть номер гри для запуску: ") - 1;

            if (gameManager.StartGame(gameIndex))
            {
                // Повідомлення про запуск гри буде виведено через подію
            }
            else
            {
                Console.WriteLine("Не вдалося запустити гру.");
            }
        }

        private void StopGame()
        {
            if (gameManager.StopGame())
            {
                // Повідомлення про зупинку гри буде виведено через подію
            }
            else
            {
                Console.WriteLine("Немає запущеної гри.");
            }
        }

        private void UninstallGame()
        {
            ShowInstalledGames();
            int gameIndex = ReadInt("Оберiть номер гри для видалення: ") - 1;

            if (gameManager.UninstallGame(gameIndex))
            {
                Console.WriteLine("Гра видалена.");
            }
            else
            {
                Console.WriteLine("Не вдалося видалити гру.");
            }
        }

        private void SaveGame()
        {
            if (gameManager.SaveGame())
            {
                // Повідомлення про збереження гри буде виведено через подію
            }
            else
            {
                Console.WriteLine("Немає запущеної гри для збереження.");
            }
        }

        private void LoadGame()
        {
            if (gameManager.LoadGame())
            {
                Console.WriteLine("Гра завантажена.");
            }
            else
            {
                Console.WriteLine("Немає збережень для цiєї гри.");
            }
        }

        private void ShowInstalledGames()
        {
            var games = gameManager.GetInstalledGames();
            Console.WriteLine($"Встановленi iгри (Вiльне мiсце: {gameManager.GetFreeHDD()} GB):");
            for (int i = 0; i < games.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {games[i].Genre} (ID: {games[i].Id})");
            }
        }

        private static int ReadInt(string message)
        {
            Console.Write(message);
            return int.TryParse(Console.ReadLine(), out int result) ? result : 0;
        }

        private static double ReadDouble(string message)
        {
            Console.Write(message);
            return double.TryParse(Console.ReadLine(), out double result) ? result : 0;
        }

        // Обробники подій
        private void OnGameStarted(object sender, GameEventArgs e)
        {
            Console.WriteLine($"Гра {e.GameName} запущена.");
            if (e.IsSimulator && gameManager.HasWheel())
            {
                Console.WriteLine("Кермо пiдключено.");
            }
        }

        private void OnGameStopped(object sender, GameEventArgs e)
        {
            Console.WriteLine($"Гра {e.GameName} зупинена.");
        }

        private void OnGameSaved(object sender, GameEventArgs e)
        {
            Console.WriteLine($"Гра {e.GameName} збережена.");
        }
    }
}
