using BattleshipGame.Enums;
using BattleshipGame.Ships;
using BattleshipGame.UIComponents;

namespace BattleshipGame.Interfaces
{
    public interface IGrid
    {
        int GetSize();
        Ship GetShipAt(Coordinates coordinates);
        Cell GetCell(Coordinates coordinates);
        State GetCellState(Coordinates coordinates);
        void SetCellState(Coordinates coordinates, State newState);
        void ShowGrid(bool isHidden);
    }
}
