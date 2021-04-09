using BattleshipGame.Allocators;
using BattleshipGame.Creators;
using BattleshipGame.UIComponents;
using BattleshipGame.Games;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using Options;

namespace BattleshipGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ReadConfig();

            var gridOptions = config.GetSection(GridOptions.Grid)
                            .Get<GridOptions>();

            var grid = new Grid(gridOptions.Size);

            var shipsOptions = config.GetSection(ShipsOptions.Ships)
                            .Get<ShipsOptions>();

            var shipsCreator = new ShipsCreator(new ShipAllocator(grid), 
                shipsOptions.ShipsConfig);

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
