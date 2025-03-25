using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Інтерфейс для всіх ігор
    interface IGame
    {
        int Id { get; }
        string Genre { get; }
        double RequiredCPU { get; }
        int RequiredRAM { get; }
        int RequiredGPU { get; }
        int RequiredHDD { get; }
    }
}
