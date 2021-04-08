using BattleshipGame.Enums;
using BattleshipGame.Interfaces;
using BattleshipGame.Ships;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BattleshipGame
{
    public class Game
    {
        private int NumberOfShips { get; set; }
        public Game()
        {
            var config = ReadConfig();

            var grid = new Grid(config.GetValue<int>("GridSize:Width"), config.GetValue<int>("GridSize:Height"));

            var shipsCreator = new ShipsCreator(new ShipAllocator(grid), 
                config.GetSection("Ships").Get<Dictionary<string, string>>());
            
            //Ass ships
            shipsCreator.CreateShips();
            NumberOfShips = shipsCreator.NumberOfShipsCreated;

            string command = "";

            grid.ShowGrid();

            while (NumberOfShips != 0)
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

                        if (grid.GetShipAt(x, y).IsSunk)
                        {
                            NumberOfShips--;
                        }
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

        private IConfiguration ReadConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("gameSettings.json")
                .Build();
        }
        
    }
}
