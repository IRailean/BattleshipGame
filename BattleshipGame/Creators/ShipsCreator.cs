using System;
using System.Collections.Generic;
using BattleshipGame.Interfaces;
using BattleshipGame.Ships;
using Interfaces;

namespace BattleshipGame.Creators
{
    public class ShipsCreator : IShipsCreator
    {
        public int NumberOfShipsCreated { get; private set; }
        private readonly IShipAllocator _shipAllocator;
        private readonly Dictionary<string, string> _config;

        public ShipsCreator(IShipAllocator shipAllocator, Dictionary<string, string> config)
        {
            _shipAllocator = shipAllocator;
            _config = config;
        }

        public void CreateShips()
        {
            foreach (var shipConfig in _config)
            {
                string name = shipConfig.Key;
                int quantity = Int32.Parse(shipConfig.Value);
                for (int i = 0; i < quantity; i++)
                {
                    var newShip = ShipFactory(name);
                    if (newShip is not null)
                    {
                        _shipAllocator.AddShip(newShip);
                        NumberOfShipsCreated++;
                    }
                }
            }
        }
        private Ship ShipFactory(string name) => name switch
        {
            "Battleship" => new Battleship(),
            "Destroyer" => new Destroyer(),
            _ => null
        };
    }
}