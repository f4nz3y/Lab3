using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Фабрика для створення ігор

    abstract class GameCreator
    {
        public abstract IGame CreateGame(int id);
    }

    class StrategyCreator : GameCreator
    {
        public override IGame CreateGame(int id)
        {
            return new Strategy(id);
        }
    }

    class SimulatorCreator : GameCreator
    {
        public override IGame CreateGame(int id)
        {
            return new Simulator(id);
        }
    }

    static class GameFactory
    {
        public static IGame? CreateGame(string genre, int id)
        {
            GameCreator? creator = genre switch
            {
                "Стратегiя" => new StrategyCreator(),
                "Симулятор" => new SimulatorCreator(),
                _ => null
            };
            return creator?.CreateGame(id);
        }
    }
}
