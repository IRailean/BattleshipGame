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
            
            if (allocationParams is null)
            {
                Console.WriteLine($"Not able to allocate {ship.Name}");
            }

            return allocationParams;
        }
        public void AddShip(Ship ship)
        {
            var allocationParams = AllocateShip(ship);

            if (allocationParams is not null)
            {
                PutShip(ship, allocationParams);
            }
        }
        private AllocationParameters GetStartPositionAndDirection(Ship ship)
        {
            Random random = new Random();
            int numOfAttempts = _grid.GetSize() * _grid.GetSize() * 2;
            var allocationParams = new AllocationParameters();

            int attempts = 0;
            int gridSize = _grid.GetSize();
            bool success = false;

            while (attempts < numOfAttempts)
            {
                allocationParams.Direction = (Direction)random.Next(0, 2);

                allocationParams.StartPosX = (allocationParams.Direction == Direction.Horizontal)
                                                ? random.Next(0, gridSize - ship.Size + 1)
                                                : random.Next(0, gridSize);

                allocationParams.StartPosY = (allocationParams.Direction == Direction.Vertical)
                                                ? random.Next(0, gridSize - ship.Size + 1)
                                                : random.Next(0, gridSize);

                success = ShipCanBePlaced(ship, allocationParams);
                if (success) break;
                attempts++;
            }

            if (!success)
            {
                return null;
            }

            return allocationParams;
        }

        private bool ShipCanBePlaced(Ship ship, AllocationParameters allocationParams)
        {
            if (allocationParams.Direction == Direction.Horizontal)
            {
                for (int i = allocationParams.StartPosX; i < allocationParams.StartPosX + ship.Size; i++)
                {
                    if (_grid.GetShipAt(new Coordinates{X = allocationParams.StartPosY, Y = i}) is not null)
                    {
                        return false;
                    }
                }
            }
            else if (allocationParams.Direction == Direction.Vertical)
            {
                for (int i = allocationParams.StartPosY; i < allocationParams.StartPosY + ship.Size; i++)
                {
                    if (_grid.GetShipAt(new Coordinates{X = i, Y = allocationParams.StartPosX}) is not null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private void PutShip(Ship ship, AllocationParameters allocationParams)
        {
            if (allocationParams.Direction == Direction.Horizontal)
            {
                for (int i = allocationParams.StartPosX; i < allocationParams.StartPosX + ship.Size; i++)
                {
                    _grid.GetCell(new Coordinates{ X = allocationParams.StartPosY, Y = i}).SetShip(ship);
                }
            }
            else if (allocationParams.Direction == Direction.Vertical)
            {
                for (int i = allocationParams.StartPosY; i < allocationParams.StartPosY + ship.Size; i++)
                {
                    _grid.GetCell(new Coordinates{X = i, Y = allocationParams.StartPosX}).SetShip(ship);
                }
            }
        }
    }
}
