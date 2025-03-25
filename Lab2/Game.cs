using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Абстрактний базовий клас для ігор
    abstract class Game : IGame
    {
        public int Id { get; }
        public string Genre { get; }
        public double RequiredCPU { get; }
        public int RequiredRAM { get; }
        public int RequiredGPU { get; }
        public int RequiredHDD { get; }

        protected Game(int id, string genre, double cpu, int ram, int gpu, int hdd)
        {
            Id = id;
            Genre = genre;
            RequiredCPU = cpu;
            RequiredRAM = ram;
            RequiredGPU = gpu;
            RequiredHDD = hdd;
        }
    }

    // Конкретні класи ігор
    class Strategy : Game
    {
        public Strategy(int id) : base(id, "Стратегiя", 2.5, 8, 4, 10) { }
    }

    class Simulator : Game
    {
        public Simulator(int id) : base(id, "Симулятор", 3.0, 16, 6, 15) { }
    }

    class OnlineCasino : Game
    {
        public OnlineCasino(int id) : base(id, "Онлайн-казино", 0, 0, 0, 0) { }
    }
}
