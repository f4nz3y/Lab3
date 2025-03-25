using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Фабрика для створення ігор
    static class GameFactory
    {
        public static IGame? CreateGame(string genre, int id)
        {
            switch (genre)
            {
                case "Стратегiя":
                    return new Strategy(id);
                case "Симулятор":
                    return new Simulator(id);
                default:
                    return null;
            }
        }
    }
}
