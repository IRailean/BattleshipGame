using Xunit;
using BattleshipGame.UIComponents;
using BattleshipGame.Ships;
using FluentAssertions;
using BattleshipGame.Enums;

namespace BattleshipGameTests
{
    public class CellTest
    {
        [Fact]
        public void SetShip_ValidShip()
        {
            //Arrange
            var expectedState = State.HasShipNotChecked;

            //Action
            var cell = new Cell();
            var ship = new Battleship();

            cell.SetShip(ship);

            //Assert
            cell.State.Should().Be(expectedState);
            cell.Ship.Should().NotBeNull();
        }

        [Fact]
        public void SetShip_NullShip()
        {
            //Arrange
            var expectedState = State.EmptyNotChecked;

            //Action
            var cell = new Cell();

            cell.SetShip(null);

            //Assert
            cell.State.Should().Be(expectedState);
            cell.Ship.Should().BeNull();
        }

        [Theory]
        [InlineData(State.EmptyChecked, State.EmptyChecked)]
        [InlineData(State.EmptyNotChecked, State.EmptyNotChecked)]
        [InlineData(State.HasShipChecked, State.HasShipChecked)]
        [InlineData(State.HasShipNotChecked, State.HasShipNotChecked)]
        [InlineData(State.Default, State.EmptyNotChecked)]
        public void ChangeStateTest(State setState, State expectedState)
        {
            //Arrange

            //Action
            var cell = new Cell();
            cell.ChangeState(setState);

            //Assert
            cell.State.Should().Be(expectedState);
        }
    }
}
