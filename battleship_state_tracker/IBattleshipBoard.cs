using System;
using System.Collections.Generic;
using System.Text;

namespace battleship_state_tracker
{
    interface IBattleshipBoard
    {
        public GridStatus getPositionStatus(int x, int y);
        public bool locateBattleShip(int x, int y, bool isHorizontal, int size);
        public bool attackPosition(int x, int y);
        public void printBoard();
    }
}
