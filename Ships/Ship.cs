using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame.Ships
{
    public abstract class Ship
    {
        public string Name { get; }
        public int Size { get; }
        private int CurrentXP { get; set; }
        public bool IsSunk { get; private set; } = false;
        protected Ship(string name, int size)
        {
            Name = name;
            Size = size;
            CurrentXP = Size;
        }
        public void Hit()
        {
            CurrentXP = Math.Max(0, CurrentXP - 1);

            if (CurrentXP == 0 && !IsSunk)
            {
                Console.WriteLine("Ship is sunk!");
                IsSunk = true;
            }
        }
    }
}
