using BattleshipGame.Interfaces;
using BattleshipGame.Ships;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BattleshipGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ReadConfig();

            var grid = new Grid(config.GetValue<int>("GridSize:Width"), config.GetValue<int>("GridSize:Height"));

            var shipsCreator = new ShipsCreator(new ShipAllocator(grid), 
                config.GetSection("Ships").Get<Dictionary<string, string>>());

            var game = new Game(grid, shipsCreator);
        }

        private static IConfiguration ReadConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("gameSettings.json")
                .Build();
        }
    }
}
