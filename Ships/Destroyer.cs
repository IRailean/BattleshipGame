using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame.Ships
{
    class Destroyer : Ship
    {
        public Destroyer() : base("Destroyer", 4)
        {
            Console.WriteLine("Destroyer created");
        }
    }
}
