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
        AllocationParameters AllocateShip(IGrid grid, Ship ship);
    }
}
