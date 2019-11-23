using System;
using System.Collections.Generic;
using System.Text;

namespace battleship_state_tracker
{
    class BattleshipStateTracker : IBattleShipStateTracker
    {
        private const int GRIDSIZE = 10;
        private int shipPositionCount = 0;
        public static IDictionary<string, Tuple<int, int>> battleShipSizeAndNum =
            new Dictionary<string, Tuple<int, int>>()
            {
                {"Carrier", new Tuple<int, int>(5, 1)},
                {"Battleboat", new Tuple<int, int>(4, 1)},
                {"Cruiser", new Tuple<int, int>(3, 1)},
                {"Destroyer", new Tuple<int, int>(2, 1)},
            };

        private IBattleshipBoard shipBoard;
        
        public BattleshipStateTracker()
        {
            this.calculateShipPositionCount();
            this.shipBoard = new BattleshipBoard(GRIDSIZE, this.shipPositionCount);
        }

        private void calculateShipPositionCount()
        {
            foreach (var item in battleShipSizeAndNum)
            {
                this.shipPositionCount += (item.Value.Item1 * item.Value.Item2);
            }
        }

        private Tuple<int, int> getPositionFromInput()
        {
            int positionX, positionY;
            while (true)
            {
                string inputPositionStr = Console.ReadLine();
                try
                {
                    var inputPositionStrArray = inputPositionStr.Split(',');
                    positionX = Convert.ToInt32(inputPositionStrArray[0]);
                    positionY = Convert.ToInt32(inputPositionStrArray[1]);
                    break;
                } catch
                {
                    Console.WriteLine("Incorrect format, please try again.");
                }                
            }            
            
            return new Tuple<int, int>(positionX, positionY);
        }

        public void printInstruction()
        {
            Console.WriteLine("Total Board Size: " + GRIDSIZE + " X " + GRIDSIZE);
            Console.WriteLine("Battleships on board are as follows.");
            Console.WriteLine("Kind\t\tSize\tNo.");
            foreach (var item in battleShipSizeAndNum)
            {
                Console.WriteLine(item.Key + ":\t" + item.Value.Item1 + "\t" + item.Value.Item2);
            }
        }

        public void addBattleShipsToTheBoard()
        {
            foreach (var item in battleShipSizeAndNum)
            {
                string shipName = item.Key;
                int shipSize = item.Value.Item1;
                int shipNum = item.Value.Item2;
                int i = 0;
                while (i < shipNum)
                {
                    Console.Write("Input " + shipName + 
                        " Start Position (left-most or top, ex. 3,2) (remaining " + (shipNum - i) + "): ");
                    var inputPosition = this.getPositionFromInput();
                    try
                    {
                        while (true)
                        {
                            Console.Write("Input Carrier Orientation, 'h' for Horizontal, 'v' for Vertial: ");
                            string inputOrientation = Console.ReadLine();
                            if ((inputOrientation.ToLower().Equals("h") || (inputOrientation.ToLower().Equals("v"))))
                            {
                                bool isHorizontal = (inputOrientation.ToLower().Equals("h")) ? true : false;
                                bool isLocated = this.shipBoard.locateBattleShip(inputPosition.Item1, inputPosition.Item2, isHorizontal, shipSize);
                                if (isLocated)
                                {
                                    Console.WriteLine(shipName + " has been successfully located on the board.");
                                    this.shipBoard.printBoard();
                                    i++;                                    
                                } else
                                {
                                    Console.WriteLine("The input position is unavailable, please try again.");
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Incorrect input, please try again.");
                            }
                        }                        
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Incorrect Format, please try again.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Too Big number, please try again.");
                    }
                }
            }
        }
        public void inputAttack()
        {
            while (true)
            {
                Console.Write("Input Attack Position (comma separated: ex. x,y): ");
                var attackPosition = this.getPositionFromInput();
                bool isHit = this.shipBoard.attackPosition(attackPosition.Item1, attackPosition.Item2);
                if (isHit)
                {
                    Console.WriteLine("The attack worked with a hit!");
                    this.shipBoard.printBoard();
                }
                else
                {
                    Console.WriteLine("The attack was missed!");
                }
                if (this.shipBoard.isLost())
                {
                    Console.WriteLine("All ships have been sunken, you lost.");
                    break;
                }
            }
        }
                
    }
}
