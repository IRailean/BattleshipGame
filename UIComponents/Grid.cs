using BattleshipGame.Enums;
using BattleshipGame.Interfaces;
using BattleshipGame.Ships;
using System;
using System.Collections.Generic;

namespace BattleshipGame.UIComponents
{
    public class Grid : IGrid
    {
        private readonly int _size;
        private List<List<Cell>> Cells { get ; set; }
        public Grid(int size)
        {
            _size = size;

            InitializeCells();
        }

        public int GetSize() => Cells.Count;

        public Ship GetShipAt(Coordinates coordinates) => AreValidCoords(coordinates) ? Cells[coordinates.X][coordinates.Y].Ship : null;
        public Cell GetCell(Coordinates coordinates) => AreValidCoords(coordinates) 
            ? Cells[coordinates.X][coordinates.Y] : null;
        public State GetCellState(Coordinates coordinates) => AreValidCoords(coordinates) 
            ? Cells[coordinates.X][coordinates.Y].State : State.Default;

        public void SetCellState(Coordinates coordinates, State newState)
        {
            if (AreValidCoords(coordinates))
            {
                Cells[coordinates.X][coordinates.Y].ChangeState(newState);
            }
        }

        private bool AreValidCoords(Coordinates coordinates)
        {
            if (coordinates is null) return false;
            return (coordinates.X >= 0 && coordinates.X < Cells.Count && coordinates.Y >= 0 && coordinates.Y < Cells.Count);
        }

        public void ShowGrid(bool isHidden)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                if (i == 0)
                {
                    DrawFirstRow();
                }
                for (int j = 0; j < Cells[0].Count; j++)
                {
                    if (j == 0)
                    {
                        Console.Write($"{(Char)(i + 'A')}  | ");
                    }
                    char mark = Cells[i][j].State switch
                    {
                        State.EmptyChecked => 'X',
                        State.EmptyNotChecked => 'O',
                        State.HasShipChecked => 'S',
                        State.HasShipNotChecked => isHidden ? 'O' : 'S',
                        _ => 'O'
                    };
                    SetConsoleColor(mark);
                    Console.Write($"{mark} ", mark);
                    ResetConsoleColor();
                }
                Console.WriteLine();
            }
        }
        private static void SetConsoleColor(char mark)
        {
            Console.ForegroundColor = mark switch
            {
                'O' => ConsoleColor.Green,
                'S' => ConsoleColor.Red,
                'X' => ConsoleColor.Yellow,
                _ => ConsoleColor.White,
            };
        }
        private static void ResetConsoleColor() => Console.ForegroundColor = ConsoleColor.White;
        private void DrawFirstRow()
        {
            Console.Write("     ");
            for (int j = 0; j < Cells.Count; j++)
            {
                Console.Write($"{j} ");
            }
            Console.WriteLine();
            Console.Write("     ");
            for (int j = 0; j < Cells.Count; j++)
            {
                Console.Write($"_ ");
            }
            Console.WriteLine();
        }

        private void InitializeCells()
        {
            Cells = new List<List<Cell>>();
            for (int i = 0; i < _size; i++)
            {
                var row = new List<Cell>();
                for (int j = 0; j < _size; j++)
                {
                    row.Add(new Cell());
                }
                Cells.Add(row);
            }
        }
    }
}
