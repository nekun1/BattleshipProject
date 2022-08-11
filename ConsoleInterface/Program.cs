using System;
using BattleLib;
using BattleLib.Models;
using BattleLibDotnet;
using BattleLibDotnet.Models;

namespace ConsoleInterface
{
    class Program
    {
        static void Main()
        {
            WelcomeText();
            PlayerInfoModel player1 = CreatePlayer("Player 1");
            PlayerInfoModel player2 = CreatePlayer("Player 2");
            //do
            //{
                DrawGrid(player1);
                GameLogic.MakeMove(player1, player2);
                DrawGrid(player1);
            //} while (!gameFinished);
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

            output.Username = AskForUsersName();

            GameLogic.PopulateGrid(output);

            PlaceShips(output); 

            Console.Clear();

            return output;
        }

        static string AskForUsersName()
        {
            Console.WriteLine("What's your name?");
            string? input = Console.ReadLine();
            if(input == null)
            {
                input = "";
            }
            return input;
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where do you want to place your ship number {model.ShipList.Count + 1}: ");
                string? location = Console.ReadLine();

                bool isValidLocation = GameLogic.PlaceShip(model, location);

                if(isValidLocation == false)
                {
                    Console.WriteLine("That's not a valid location, please try again.");
                }

            } while (model.ShipList.Count < 5);
        }
        
        private static void DrawGrid(PlayerInfoModel player)
        {
            string currentRow = player.Grid[0].SpotLetter;

            foreach(var gridSpot in player.Grid)
            {
                if(gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }

                if(gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                    Console.WriteLine("?");
            }
                if(gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                    Console.WriteLine("?");
            }
        }

        static void PrintScore()
        {

        }

        static void PrintStatistics()
        {

        }
    }
}