using BattleshipGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    public class AllocationParameters
    {
        public Direction Direction { get; set; } = Direction.Default;
        public int StartPosX { get; set; } = -1;
        public int StartPosY { get; set; } = -1;
        public AllocationParameters() {}
        public AllocationParameters(Direction direction, int startPosX, int startPosY)
        {
            Direction = direction;
            StartPosX = startPosX;
            StartPosY = startPosY;
        }
    }
}
