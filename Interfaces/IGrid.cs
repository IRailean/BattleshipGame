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
        Ship GetShipAt(int x, int y);
        State GetCellState(int x, int y);
        void SetCellState(int x, int y, State newState);
        void ShowGrid();
        void ShowGridSecret();
        void AddShip(Ship ship);
    }
}
