using BattleshipGame.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Interfaces
{
    public interface IShipAllocator
    {
        AllocationParameters AllocateShip(Ship ship);
        void AddShip(Ship ship);
    }
}
