using System;
using System.Collections.Generic;
using System.Text;

namespace battleship_state_tracker
{
    interface IBattleShipStateTracker
    {
        void printInstruction();
        void addBattleShipsToTheBoard();
        void inputAttack();
    }
}
