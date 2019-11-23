using System;
using System.Collections.Generic;
using System.Text;

namespace battleship_state_tracker
{
    class BattleshipStateTracker : IBattleShipStateTracker
    {
        private const int GRIDSIZE = 10;
        public static IDictionary<string, Tuple<int, int>> battleShipSizeAndNum =
            new Dictionary<string, Tuple<int, int>>()
            {
                {"Carrier", new Tuple<int, int>(5, 1)},
                {"Battleboat", new Tuple<int, int>(4, 2)},
                {"Cruiser", new Tuple<int, int>(3, 3)},
                {"Destroyer", new Tuple<int, int>(2, 4)},
            };

        IBattleshipBoard shipBoard = new BattleshipBoard(GRIDSIZE);
        
        public void printInstruction()
        {
            Console.WriteLine("Total Board Size: " + GRIDSIZE + " X " + GRIDSIZE);
            Console.WriteLine("Battleships on board are as follows.");
            Console.WriteLine("Kind\tSize\tNo.");
            foreach (var item in battleShipSizeAndNum)
            {
                Console.WriteLine(item.Key + ":\t" + item.Value.Item1 + "\t" + item.Value.Item2);
            }
        }

        public void addBattleShipsToTheBoard()
        {
            string inputPositionStr;
            string[] inputPositionStrArray;
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
                    inputPositionStr = Console.ReadLine();
                    try
                    {
                        inputPositionStrArray = inputPositionStr.Split(',');
                        int positionX = Convert.ToInt32(inputPositionStrArray[0]);
                        int positionY = Convert.ToInt32(inputPositionStrArray[1]);

                        while (true)
                        {
                            Console.Write("Input Carrier Orientation, 'h' for Horizontal, 'v' for Vertial: ");
                            string inputOrientation = Console.ReadLine();
                            if ((inputOrientation.ToLower().Equals("h") || (inputOrientation.ToLower().Equals("v"))))
                            {
                                bool isHorizontal = (inputOrientation.ToLower().Equals("h")) ? true : false;
                                bool isLocated = this.shipBoard.locateBattleShip(positionX, positionY, isHorizontal, shipSize);
                                if (isLocated)
                                {
                                    Console.WriteLine(shipName + " has been successfully located on the board.");
                                    shipBoard.printBoard();
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
            Console.WriteLine("Input Attack Position (comma separated: ex. x,y).");
            

        }
                
        public void printStatus()
        {
            throw new NotImplementedException();
        }

        public void startGame()
        {
            throw new NotImplementedException();
        }

        
    }
}
