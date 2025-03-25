using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Клас для представлення ПК
    class PC
    {
        public double CPU { get; }
        public int RAM { get; }
        public int GPU { get; }
        public int HDD { get; set; }
        public bool HasInternet { get; }
        public bool HasWheel { get; }

        public PC(double cpu, int ram, int gpu, int hdd, bool hasInternet, bool hasWheel)
        {
            CPU = cpu;
            RAM = ram;
            GPU = gpu;
            HDD = hdd;
            HasInternet = hasInternet;
            HasWheel = hasWheel;
        }

        public bool CanRun(IGame game)
        {
            return CPU >= game.RequiredCPU && RAM >= game.RequiredRAM && GPU >= game.RequiredGPU;
        }
    }
}
