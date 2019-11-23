using System;
using System.Collections.Generic;
using System.Text;

namespace battleship_state_tracker
{
    enum GridStatus
    {
        Blank,
        ShipPositioned,
        Hit,
        Miss
    }

    class OneGrid
    {
        private GridStatus gridStatus;
        public static IDictionary<GridStatus, Char> gridDisplayCharacter =
            new Dictionary<GridStatus, Char>()
            {
                {GridStatus.Blank, 'O'},
                {GridStatus.ShipPositioned, '#'},
                {GridStatus.Hit, 'X'},
                {GridStatus.Miss, '_'},
            };
        public OneGrid()
        {
            this.gridStatus = GridStatus.Blank;
        }

        internal GridStatus GridStatus { get => gridStatus; set => gridStatus = value; }

        public void printGridStatus()
        {
            Console.Write(OneGrid.gridDisplayCharacter[this.gridStatus]);
        }
    }
}
