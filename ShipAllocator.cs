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
        private readonly IGrid _grid;
        public ShipAllocator(IGrid grid)
        {
            _grid = grid;
        }

        public AllocationParameters AllocateShip(Ship ship)
        {
            var allocationParams = GetStartPositionAndDirection(ship);

            return allocationParams;
        }

        private AllocationParameters GetStartPositionAndDirection(Ship ship)
        {
            Random random = new Random();
            int numOfAttempts = 10;
            var allocationParams = new AllocationParameters();

            int attempts = 0;
            int gridSize = _grid.GetSize();
            while (attempts < numOfAttempts)
            {
                allocationParams.Direction = (Direction)random.Next(0, 2);

                allocationParams.StartPosX = (allocationParams.Direction == Direction.Horizontal)
                                                ? random.Next(0, gridSize - ship.Size + 1)
                                                : random.Next(0, gridSize);

                allocationParams.StartPosY = (allocationParams.Direction == Direction.Vertical)
                                                ? random.Next(0, gridSize - ship.Size + 1)
                                                : random.Next(0, gridSize);

                if (!ShipCanBePlaced(ship, allocationParams)) break;
                attempts++;
            }

            return allocationParams;
        }

        private bool ShipCanBePlaced(Ship ship, AllocationParameters allocationParams)
        {
            if (allocationParams.Direction == Direction.Horizontal)
            {
                for (int i = allocationParams.StartPosX; i < allocationParams.StartPosX + ship.Size; i++)
                {
                    if (_grid.GetShipAt(allocationParams.StartPosY, i) is not null)
                    {
                        return true;
                    }
                }
            }
            else if (allocationParams.Direction == Direction.Vertical)
            {
                for (int i = allocationParams.StartPosY; i < allocationParams.StartPosY + ship.Size; i++)
                {
                    if (_grid.GetShipAt(i, allocationParams.StartPosX) is not null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void AddShip(Ship ship)
        {
            var allocationParams = AllocateShip(ship);

            if (allocationParams is not null)
            {
                PutShip(ship, allocationParams);
            }
        }
        private void PutShip(Ship ship, AllocationParameters allocationParams)
        {
            if (allocationParams.Direction == Direction.Horizontal)
            {
                for (int i = allocationParams.StartPosX; i < allocationParams.StartPosX + ship.Size; i++)
                {
                    _grid.GetCell(allocationParams.StartPosY, i).SetShip(ship);
                }
            }
            else if (allocationParams.Direction == Direction.Vertical)
            {
                for (int i = allocationParams.StartPosY; i < allocationParams.StartPosY + ship.Size; i++)
                {
                    _grid.GetCell(i, allocationParams.StartPosX).SetShip(ship);
                }
            }
        }
    }
}
