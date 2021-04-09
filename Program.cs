using BattleshipGame.Allocators;
using BattleshipGame.Creators;
using BattleshipGame.UIComponents;
using BattleshipGame.Games;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace BattleshipGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ReadConfig();

            var grid = new Grid(config.GetValue<int>("GridSize:Size"));

            var shipsCreator = new ShipsCreator(new ShipAllocator(grid), 
                config.GetSection("Ships").Get<Dictionary<string, string>>());

            var game = new Game(grid, shipsCreator);
        }

        private static IConfiguration ReadConfig()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("gameSettings.json")
                .Build();
        }
    }
}
