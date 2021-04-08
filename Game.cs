﻿using BattleshipGame.Enums;
using BattleshipGame.Interfaces;
using BattleshipGame.Ships;
using Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BattleshipGame
{
    public class Game
    {
        private int NumberOfShips { get; set; }
        public IGrid _grid { get; set; }
        public IShipsCreator _shipsCreator { get; set; }
        public Game(IGrid grid, IShipsCreator shipsCreator)
        {
            _grid = grid;
            _shipsCreator = shipsCreator;

            _shipsCreator.CreateShips();
            NumberOfShips = _shipsCreator.NumberOfShipsCreated;

            PlayGame();
        }

        private void PlayGame()
        {
            _grid.ShowGrid(false);
            PrintWelcomeMessage();

            while (NumberOfShips != 0)
            {
                Console.WriteLine("Please enter coordinates to shoot, write 'STOP' to exit the game: ");
                string command = Console.ReadLine();

                if (command == "STOP") break;

                var coords = ProcessCommand(command);
                if (coords is null)
                {
                    Console.WriteLine($"Command {command} is incorrect, try one more time");
                    continue;
                }
                Console.Clear();
                MakeMove(coords);
                _grid.ShowGrid(true);
            }

            PrintClosingMessage();
        }

        private void PrintWelcomeMessage() => Console.WriteLine("Welcome to Battleship game!");
        private void PrintClosingMessage()
        {
            if (NumberOfShips == 0)
            {
                Console.WriteLine("Congratulations, you have won!");
            }
        }
        private Coordinates ProcessCommand(string command)
        {
            if (command is null) return null;

            if (command.Length != 2) 
            {
                return null;
            }
            int x = command[0] - 'A';
            int y = command[1] - '0' - 1;
            
            if (x < 0 || x > _grid.GetSize() || y < 0 || y > _grid.GetSize())
            {
                return null;
            }

            return new Coordinates{X = x, Y = y};
        }

        private void MakeMove(Coordinates coordinates)
        {
            if (coordinates is null) return;

            var state = _grid.GetCellState(coordinates);
            
            switch (state)
            {
                case State.EmptyNotChecked: MissAction(coordinates); break;
                case State.HasShipNotChecked: HitAction(coordinates); break;
                case State.EmptyChecked:
                case State.HasShipChecked: Console.WriteLine("You`ve already checked this location!"); break;
                default: Console.WriteLine("Unknown state"); break;
            };
        }

        private void MissAction(Coordinates coordinates)
        {
            if (coordinates is null) return;

             Console.WriteLine("Miss!");
            _grid.SetCellState(coordinates, State.EmptyChecked);
        }

        private void HitAction(Coordinates coordinates)
        {
            if (coordinates is null) return;

            Console.WriteLine("Hit!");

            _grid.SetCellState(coordinates, State.HasShipChecked);
            var ship = _grid.GetShipAt(coordinates);

            if (ship is null) return;

            ship.Hit();

            if (ship.IsSunk)
            {
                NumberOfShips--;
            }
        }
    }
}
