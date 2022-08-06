using System;
using BattleLibDotnet;
using BattleLibDotnet.Models;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeText();
            PlayerInfoModel player1 = CreatePlayer("Player 1");
            PlayerInfoModel player2 = CreatePlayer("Player 2");
            Console.ReadLine();
        }

        static void WelcomeText()
        {
            Console.WriteLine("Battleship Project");
            Console.WriteLine("====================");
        }
        
        private static PlayerInfoModel CreatePlayer(string playerIdentifier)
        {
            Console.WriteLine($"Player information for {playerIdentifier}");

            PlayerInfoModel output = new PlayerInfoModel();

            output.UserName = AskForUsersName();

            GameLogic.DrawGrid(output);

            PlaceShips(output); 

            Console.Clear();

            return output;
        }

        static string AskForUsersName()
        {
            Console.WriteLine("What's your name?");
            string input = Console.ReadLine();
            return input;
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where do you want to place your ship number {model.ShipList.Count + 1}: ");
                string location = Console.ReadLine();

                bool isValidLocation = GameLogic.PlaceShip(model, location);

                if(isValidLocation == false)
                {
                    Console.WriteLine("That's not a valid location, please try again.");
                }

            } while (model.ShipList.Count < 5);
        }

        static void Fire()
        {

        }

        static void CanFire()
        {

        }

        static void PrintScore()
        {

        }

        static void PrintStatistics()
        {

        }
    }
}