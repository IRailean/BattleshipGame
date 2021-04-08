using BattleshipGame.Enums;
using BattleshipGame.Interfaces;
using BattleshipGame.Ships;
using System;

namespace BattleshipGame
{
    public class Game
    {
        public Game()
        {
            var grid = new Grid(10, 10);

            var shipAllocator = new ShipAllocator(grid);
            if (shipAllocator is null)
            {
                Console.WriteLine("NULL");
            }

            string command = "";

            var ship = new Battleship();
            shipAllocator.AddShip(ship);
            shipAllocator.AddShip(ship);

            grid.ShowGrid();

            while (command != "STOP")
            {
                command = Console.ReadLine();
                if (command == "STOP") break;

                if (command.Length == 2)
                {
                    int x = command[0] - 'A';
                    int y = command[1] - '0' - 1;
                    var state = grid.GetCellState(x, y);
                    Console.WriteLine($"Getting state at {x}, {y}: {state}");

                    if (state == State.EmptyNotChecked)
                    {
                        Console.WriteLine("Miss!");
                        grid.SetCellState(x, y, State.EmptyChecked);
                    }
                    else if (state == State.EmptyChecked)
                    {
                        Console.WriteLine("Miss!");
                    }
                    else if (state == State.HasShipNotChecked)
                    {
                        Console.WriteLine("Hit!");
                        grid.SetCellState(x, y, State.HasShipChecked);
                        grid.GetShipAt(x, y).Hit();
                    }
                    else if (state == State.HasShipChecked)
                    {
                        Console.WriteLine("You have already hit this ship!");
                    }

                    grid.ShowGridSecret();
                }
            }

            Console.WriteLine("Hello world");
        }
    }
}
