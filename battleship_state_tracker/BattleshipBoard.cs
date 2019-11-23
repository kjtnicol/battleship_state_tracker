using System;
using System.Collections.Generic;
using System.Text;
using static battleship_state_tracker.utils.Ext;

namespace battleship_state_tracker
{
    class BattleshipBoard : IBattleshipBoard
    {
        private int gridSize;
        private OneGrid[,] gridArray;

        public BattleshipBoard(int gridSize)
        {
            this.gridSize = gridSize;
            this.gridArray = new OneGrid[gridSize, gridSize];
        }

        public GridStatus getPositionStatus(int x, int y)
        {
            if ( (x > this.gridSize-1) || (y > this.gridSize - 1))
            {
                throw new Exception("Max Position should be less than " + this.gridSize);
            }
            return this.gridArray[x, y].GridStatus;
        }

        public void setPositionStatus(int x, int y, GridStatus status)
        {
            if ((x > this.gridSize - 1) || (y > this.gridSize - 1))
            {
                throw new Exception("Max Position should be less than " + this.gridSize);
            }
            this.gridArray[x, y].GridStatus = status;
        }
        
        public void printBoard()
        {
            for (int i=0; i< this.gridSize; i++)
            {
                for (int j=0; j< this.gridSize; j++)
                {
                    this.gridArray[i, j].printGridStatus();
                }
                Console.Write('\n');
            }
        }

        public bool locateBattleShip(int x, int y, bool isHorizontal, int size)
        {
            if (isHorizontal)
            {
                for (int i=y; i < y+size; i++)
                {
                    if (this.gridArray[x, i].GridStatus != GridStatus.ShipPositioned)
                    {
                        this.gridArray[x, i].GridStatus = GridStatus.ShipPositioned;
                    } else
                    {
                        return false;
                    }
                }
            } else
            {
                for (int i = x; i < x + size; i++)
                {
                    if (this.gridArray[i, y].GridStatus != GridStatus.ShipPositioned)
                    {
                        this.gridArray[i, y].GridStatus = GridStatus.ShipPositioned;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool attackPosition(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
