﻿using BattleshipGame.Enums;
using BattleshipGame.Ships;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class Cell
    {
        public State State { get; private set; } = State.EmptyNotChecked;
        public Ship Ship { get; private set; } = null;

        public Cell() {}
        public void SetShip(Ship ship)
        {
            if (Ship is null && State == State.EmptyNotChecked)
            {
                Ship = ship;
                ChangeState(State.HasShipNotChecked);
            }
        }

        public void ChangeState(State newState)
        {
            State = newState;
        }
    }
}
