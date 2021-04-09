using BattleshipGame.Enums;

namespace BattleshipGame.Helpers
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
