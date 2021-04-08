using BattleshipGame.Enums;
using BattleshipGame.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Interfaces
{
    public interface IGrid
    {
        int GetSize();
        Ship GetShipAt(Coordinates coordinates);
        Cell GetCell(Coordinates coordinates);
        State GetCellState(Coordinates coordinates);
        void SetCellState(Coordinates coordinates, State newState);
        void ShowGrid();
        void ShowGridSecret();
    }
}
