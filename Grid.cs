using BattleshipGame.Enums;
using BattleshipGame.Interfaces;
using BattleshipGame.Ships;
using System;
using System.Collections.Generic;

namespace BattleshipGame
{
    public class Grid : IGrid
    {
        private readonly int _width;
        private readonly int _height;
        private IShipAllocator _shipAllocator { get; set; }
        private List<List<Cell>> Cells { get; set; }
        public Grid(int width, int height, IShipAllocator shipAllocator)
        {
            _width = width;
            _height = height;
            _shipAllocator = shipAllocator;

            InitializeCells();
        }

        public int GetSize()
        {
            return Cells.Count;
        }
        public Ship GetShipAt(int x, int y)
        {
            return AreValidCoords(x, y) ? Cells[x][y].Ship : null;
        }

        public State GetCellState(int x, int y)
        {
            return AreValidCoords(x, y) ? Cells[x][y].State : State.Default;
        }
        public void SetCellState(int x, int y, State newState)
        {
            if (AreValidCoords(x, y))
            {
                Cells[x][y].ChangeState(newState);
            }
        }

        private bool AreValidCoords(int x, int y)
        {
            return (x >= 0 && x < Cells.Count && y >= 0 && y < Cells.Count);
        }
        public void AddShip(Ship ship)
        {
            var allocationParameters = _shipAllocator.AllocateShip(this, ship);

            if (allocationParameters is not null)
            {
                PutShip(allocationParameters, ship);
            }
        }

        public void ShowGrid()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                for (int j = 0; j < Cells[0].Count; j++)
                {
                    char mark = Cells[i][j].State switch
                    {
                        State.EmptyChecked => 'X',
                        State.EmptyNotChecked => 'O',
                        State.HasShipChecked => 'S',
                        State.HasShipNotChecked => 'S',
                        _ => 'O'
                    };
                    Console.Write($"{mark} ", mark);
                }
                Console.WriteLine();
            }
        }
        public void ShowGridSecret()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                for (int j = 0; j < Cells[0].Count; j++)
                {
                    char mark = Cells[i][j].State switch
                    {
                        State.EmptyChecked => 'X',
                        State.EmptyNotChecked => 'O',
                        State.HasShipChecked => 'S',
                        _ => 'O'
                    };
                    Console.Write($"{mark} ", mark);
                }
                Console.WriteLine();
            }
        }

        private void InitializeCells()
        {
            Cells = new List<List<Cell>>();
            for (int i = 0; i < _height; i++)
            {
                var row = new List<Cell>();
                for (int j = 0; j < _width; j++)
                {
                    row.Add(new Cell());
                }
                Cells.Add(row);
            }
        }
        private void PutShip(AllocationParameters allocationParameters, Ship ship)
        {
            if (allocationParameters.Direction == Direction.Horizontal)
            {
                for (int i = allocationParameters.StartPosX; i < allocationParameters.StartPosX + ship.Size; i++)
                {
                    Cells[allocationParameters.StartPosY][i].SetShip(ship);
                }
            }
            else if (allocationParameters.Direction == Direction.Vertical)
            {
                for (int i = allocationParameters.StartPosY; i < allocationParameters.StartPosY + ship.Size; i++)
                {
                    Cells[i][allocationParameters.StartPosX].SetShip(ship);
                }
            }
        }
    }
}
