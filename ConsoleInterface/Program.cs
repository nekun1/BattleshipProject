using System;
using BattleLib;
using BattleLib.Models;

namespace ConsoleInterface
{
    class Program
    {
        static void Main()
        {
            WelcomeText();
            PlayerInfoModel player1 = CreatePlayer("Player 1");
            PlayerInfoModel player2 = CreatePlayer("Player 2");
            bool gameFinished;
            do
            {
                DrawGrid(player1);
                Shoot(player1, player2);
                Console.Clear();
                (player1, player2) = GameLogic.FlipPlayers(player1, player2);
                gameFinished = GameLogic.IsGameFinished(player1, player2);
            } while (!gameFinished);
            PlayerInfoModel winner = GameLogic.DetermineWinner(player1, player2);
            Console.WriteLine($"Congratulations {winner.Username}, you won!");
            Console.WriteLine($"It took you {winner.Shots} shots to sink your enemy's ships!");
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
            if (input == null)
            {
                input = "Default";
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

                if (isValidLocation == false)
                {
                    Console.WriteLine("That's not a valid location, please try again.");
                }

            } while (model.ShipList.Count < 5);
        }

        //Gets the input from the player and calls TakeShot().
        private static void Shoot(PlayerInfoModel player1, PlayerInfoModel player2)
        {
            bool shotOutcome = false;
            bool isValid = false;
            Console.WriteLine($"\nWhere would you like to take your shot {player1.Username}?");
            do
            {
                string shotLocation = Console.ReadLine();
                if (shotLocation.Length == 2)
                {
                    isValid = true;
                    shotOutcome = GameLogic.TakeShot(player1, player2, shotLocation);
                }
                else
                    Console.WriteLine("Please enter a valid location");
            }while(!isValid);
            string message = shotOutcome == false ? "You missed!" : "You scored!";
            Console.Clear();
            DrawGrid(player1);
            Console.WriteLine($"{message}\nPress any key to change player");
            Console.ReadLine();
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
            Console.WriteLine();
        }

        static void PrintScore()
        {

        }

        static void PrintStatistics()
        {

        }
    }
}