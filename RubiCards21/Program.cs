using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleGame game = new BattleGame();

            while (!game.IsFinished)
            {
                game.Update();
                game.Display();
            }

            Console.ReadKey();
        }
    }
}
