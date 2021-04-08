using BattleshipGame.Helpers;
using BattleshipGame.Ships;

namespace BattleshipGame.Interfaces
{
    public interface IShipAllocator
    {
        // Find place for a ship if possible
        AllocationParameters AllocateShip(Ship ship);
        void AddShip(Ship ship);
    }
}
