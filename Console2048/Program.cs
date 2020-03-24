using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    class Program
    {
        static void Main(string[] args)
        {
            GameCore core = new GameCore();
            core.NumberGeneratation();
            core.NumberGeneratation();
            PrintMap(core.Map);

            while (true)
            {
                KeymapExecution(core);
                if (core.IsChange == 1)
                {
                    core.NumberGeneratation();
                }
                else if (core.IsChange == 0)
                {
                    Console.WriteLine("~~~No Change~~~");
                }
                else
                {
                    Console.WriteLine("Retract?! Shame on you");
                }
                PrintMap(core.Map);
            }
        }

        private static void PrintMap(int[,] map)
        {
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    Console.Write(map[r, c] + "\t");
                }
                Console.WriteLine();
            }
        }

        private static void KeymapExecution(GameCore core)
        {
            switch (Console.ReadLine())
            {
                case "w":
                    core.Move(MoveDirection.Up);
                    break;
                case "s":
                    core.Move(MoveDirection.Down);
                    break;
                case "a":
                    core.Move(MoveDirection.Left);
                    break;
                case "d":
                    core.Move(MoveDirection.Right);
                    break;
                case "z":
                    core.Move(MoveDirection.Retract);
                    break;
            }
        }
    }
}
