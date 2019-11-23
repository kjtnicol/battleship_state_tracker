using System;
using System.Collections.Generic;
using System.Text;

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
            if (isHorizontal)
            {
                if ( (y+size) > this.gridSize )
                {
                    return false;
                }
                bool isPositionAvailable = true;
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
                bool isPositionAvailable = true;
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
            return true;
        }

        public bool attackPosition(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
