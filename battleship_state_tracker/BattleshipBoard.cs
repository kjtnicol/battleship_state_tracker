using System;
using System.Collections.Generic;
using System.Text;

namespace battleship_state_tracker
{
    class BattleshipBoard : IBattleshipBoard
    {
        private int gridSize;
        private OneGrid[,] gridArray;
        private int hitCount = 0;
        private int shipPositionCount = 0;

        public BattleshipBoard(int gridSize, int shipPositionCount)
        {
            this.gridSize = gridSize;
            this.shipPositionCount = shipPositionCount;
            this.gridArray = new OneGrid[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    this.gridArray[i, j] = new OneGrid();
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
            bool isPositionAvailable=true;
            if (isHorizontal)
            {
                if ( (y+size) > this.gridSize )
                {
                    return false;
                }
                for (int i=y; i < y+size; i++)
                {
                    if (this.gridArray[x, i].GridStatus != GridStatus.Blank)
                    {
                        isPositionAvailable = false;
                        break;
                    }
                }
                if (isPositionAvailable)
                    for (int i = y; i < y + size; i++)
                        this.gridArray[x, i].GridStatus = GridStatus.ShipPositioned;
            } else
            {
                if ((x + size) > this.gridSize)
                {
                    return false;
                }
                for (int i=x; i < x+size; i++)
                {
                    if (this.gridArray[i, y].GridStatus != GridStatus.Blank)
                    {
                        isPositionAvailable = false;
                        break;
                    }
                }
                if (isPositionAvailable)
                    for (int i = x; i < x + size; i++)
                        this.gridArray[i, y].GridStatus = GridStatus.ShipPositioned;
            }
            return isPositionAvailable;
        }

        public bool attackPosition(int x, int y)
        {
            try
            {
                if (this.gridArray[x, y].GridStatus == GridStatus.ShipPositioned)
                {
                    this.gridArray[x, y].GridStatus = GridStatus.Hit;
                    this.hitCount++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Unavailable position, please try again.");
                return false;
            }
        }

        public bool isLost()
        {
            return (this.hitCount >= this.shipPositionCount) ? true : false;
        }
    }
}
