using BattleshipGame.Enums;
using BattleshipGame.Interfaces;
using BattleshipGame.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    class ShipAllocator : IShipAllocator
    {
        public ShipAllocator()
        {
        }

        public AllocationParameters AllocateShip(IGrid grid, Ship ship)
        {
            var coords = GetStartPositionAndDirection(grid, ship);

            return coords;
        }

        private AllocationParameters GetStartPositionAndDirection(IGrid grid, Ship ship)
        {
            Random random = new Random();
            int numOfAttempts = 10;
            var allocationParameters = new AllocationParameters();

            int attempts = 0;
            int gridSize = grid.GetSize();
            while (attempts < numOfAttempts)
            {
                allocationParameters.Direction = (Direction)random.Next(0, 2);

                allocationParameters.StartPosX = (allocationParameters.Direction == Direction.Horizontal) 
                                                ? random.Next(0, gridSize - ship.Size + 1) 
                                                : random.Next(0, gridSize);

                allocationParameters.StartPosY = (allocationParameters.Direction == Direction.Vertical) 
                                                ? random.Next(0, gridSize - ship.Size + 1) 
                                                : random.Next(0, gridSize);

                if (!ShipCanBePlaced(grid, ship, allocationParameters)) break;
                attempts++;
            }

            return allocationParameters;
        }

        private bool ShipCanBePlaced(IGrid grid, Ship ship, AllocationParameters allocationParameters)
        {
            if (allocationParameters.Direction == Direction.Horizontal)
            {
                for (int i = allocationParameters.StartPosX; i < allocationParameters.StartPosX + ship.Size; i++)
                {
                    if (grid.GetShipAt(allocationParameters.StartPosY, i) is not null)
                    {
                        return true;
                    }
                }
            }
            else if (allocationParameters.Direction == Direction.Vertical)
            {
                for (int i = allocationParameters.StartPosY; i < allocationParameters.StartPosY + ship.Size; i++)
                {
                    if (grid.GetShipAt(i, allocationParameters.StartPosX) is not null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
