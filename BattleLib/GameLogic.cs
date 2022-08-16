using BattleLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib
{
    public static class GameLogic
    {
        //Populates the grid with A-E and 1-5.
        public static void PopulateGrid(PlayerInfoModel player)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E",
            };

            List<int> numbers = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {
                    AddGridSpot(player, letter, number);
                }
            }
        }

        //Places the ship in a player's ShipList at said location.
        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            bool output = false;
            if (location.Length == 2)
            {
                (string locationLetter, int locationNumber) = SeparateLocation(location);
                bool shipPresent = CheckForShip(model, locationLetter, locationNumber);
                foreach (var ship in model.Grid)
                {
                    if (shipPresent)
                        continue;
                    if (locationLetter == ship.SpotLetter && locationNumber == ship.SpotNumber)
                    {
                        GridSpotModel spot = new GridSpotModel
                        {
                            SpotLetter = locationLetter,
                            SpotNumber = locationNumber,
                            Status = GridSpotStatus.Ship
                        };
                        model.ShipList.Add(spot);
                        output = true;
                    }
                }
            }
            return output;
        }

        //Flips the players around after they take their turn.
        public static (PlayerInfoModel, PlayerInfoModel) FlipPlayers(PlayerInfoModel player1, PlayerInfoModel player2)
        {
            (player1, player2) = (player2, player1);
            return (player1, player2);
        }

        //Fucking mess, checks if a ship is present at specified location via CheckForShot, then uses MarkShot to mark the location on the grid.
        //TODO: Probably shouldn't be a bool?
        public static bool TakeShot(PlayerInfoModel player, PlayerInfoModel opponent, string shotLocation)
        {
            (string locationLetter, int locationNumber) = SeparateLocation(shotLocation);
            bool shipPresent = CheckForShip(opponent, locationLetter, locationNumber);
            bool markedStatus = MarkShotStatus(player, locationLetter, locationNumber, shipPresent);
            player.Shots++;
            return markedStatus;
        }

        //Checks if a ship exists at said location.
        private static bool CheckForShip(PlayerInfoModel opponent, string locationLetter, int locationNumber)
        {
            bool shipPresent = false;
            foreach (var gridSpot in opponent.ShipList)
            {
                if (gridSpot.SpotLetter == locationLetter && gridSpot.SpotNumber == locationNumber)
                {
                    shipPresent = true;
                }
            }
            return shipPresent;
        }

        //Separates the input into two single variables.
        private static (string locationLetter, int locationNumber) SeparateLocation(string location)
        {
            string locationLetter = location.Substring(0, 1).ToUpper();
            int locationNumber;
            bool locationNumberCheck = int.TryParse(location.Substring(1, 1), out int result);
            if (locationNumberCheck)
            {
                locationNumber = result;
            }
            else
                locationNumber = 0;
            return (locationLetter, locationNumber);
        }

        //Marks the location on the grid as either a hit or a miss.
        private static bool MarkShotStatus(PlayerInfoModel player, string locationLetter, int locationNumber, bool shipPresent)
        {
            bool shotStatus = false;
            foreach (var gridSpot in player.Grid)
            {
                if (gridSpot.SpotLetter == locationLetter && gridSpot.SpotNumber == locationNumber)
                {
                    if (shipPresent)
                    {
                        gridSpot.Status = GridSpotStatus.Hit;
                        shotStatus = true;
                        player.Points++;
                    }
                    else
                        gridSpot.Status = GridSpotStatus.Miss;
                }
            }
            return shotStatus;
        }

        //Adds a spot to the grid, used in PopulateGrid method
        private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };
            model.Grid.Add(spot);
        }

        public static bool IsGameFinished(PlayerInfoModel player1, PlayerInfoModel player2)
        {
            bool gameFinished = false;
            if(player1.Points == 5 || player2.Points == 5)
                gameFinished = true;
            return gameFinished;
        }

        public static PlayerInfoModel DetermineWinner(PlayerInfoModel player1, PlayerInfoModel player2)
        {
            PlayerInfoModel winner = player1.Points > player2.Points ? player1 : player2;
            return winner;
        }
    }
}
