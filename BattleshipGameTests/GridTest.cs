using BattleshipGame;
using BattleshipGame.Enums;
using BattleshipGame.Ships;
using BattleshipGame.UIComponents;
using FluentAssertions;
using Xunit;

namespace BattleshipGameTests
{
    public class GridTest
    {
        [Theory]
        [InlineData(10, 0, 0)]
        public void GetShipAt_ValidCoordinates_ReturnedShip(int size, int x, int y)
        {
            //Arrange
            var coordinates = new Coordinates { X = x, Y = y };
            var grid = new Grid(size);
            grid.GetCell(coordinates).SetShip(new Battleship());

            //Action
            var ship = grid.GetShipAt(coordinates);

            //Assert
            ship.Name.Should().Be("Battleship");
        }

        [Theory]
        [InlineData(10, -1, -1)]
        [InlineData(10, 11, 11)]
        public void GetCell_InvalidCoordinates_ReturnsNull(int size, int x, int y)
        {
            //Arrange
            var coordinates = new Coordinates { X = x, Y = y };
            var grid = new Grid(size);
            grid.GetCell(coordinates);

            //Action
            var cell = grid.GetCell(coordinates);

            //Assert
            cell.Should().BeNull();
        }

        [Fact]
        public void GetCellState_ValidCoordinates_ReturnStateEmptyNotChecked()
        {
            //Arrange
            var coordinates = new Coordinates { X = 0, Y = 0 };
            var grid = new Grid(10);

            //Action
            var state = grid.GetCellState(coordinates);

            //Assert
            state.Should().Be(State.EmptyNotChecked);
        }

        [Fact]
        public void GetCellState_InvalidCoordinates_ReturnStateDefault()
        {
            //Arrange
            var coordinates = new Coordinates { X = -1, Y = -1 };
            var grid = new Grid(10);

            //Action
            var state = grid.GetCellState(coordinates);

            //Assert
            state.Should().Be(State.Default);
        }


    }
}
