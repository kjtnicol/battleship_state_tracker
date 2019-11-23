using System;

namespace battleship_state_tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Battleship State Tracker Game!");
            BattleshipStateTracker battleshipStateTracker = new BattleshipStateTracker();
            battleshipStateTracker.printInstruction();
            battleshipStateTracker.addBattleShipsToTheBoard();
            battleshipStateTracker.printStatus();
            battleshipStateTracker.startGame();
        }
    }
}
