using BattleshipGame.Enums;
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

            while (NumberOfShips != 0)
            {
                string command = Console.ReadLine();

                if (command == "STOP") break;

                var coords = ProcessCommand(command);

                Console.Clear();
                MakeMove(coords);
                _grid.ShowGrid(true);
            }
        }

        private Coordinates ProcessCommand(string command)
        {
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
            var state = _grid.GetCellState(coordinates);
            
            if (state == State.EmptyNotChecked)
            {
               MissAction(coordinates);
            }
            else if (state == State.HasShipNotChecked)
            {
                HitAction(coordinates);
            }
            else
            {
                Console.WriteLine("You`ve already checked this location!");
            }
        }

        private void MissAction(Coordinates coordinates)
        {
             Console.WriteLine("Miss!");
            _grid.SetCellState(coordinates, State.EmptyChecked);
        }

        private void HitAction(Coordinates coordinates)
        {
            Console.WriteLine("Hit!");

            _grid.SetCellState(coordinates, State.HasShipChecked);
            _grid.GetShipAt(coordinates).Hit();

            if (_grid.GetShipAt(coordinates).IsSunk)
            {
                NumberOfShips--;
            }
        }
    }
}
