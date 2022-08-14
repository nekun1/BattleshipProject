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

        public static (PlayerInfoModel, PlayerInfoModel) FlipPlayers(PlayerInfoModel player1, PlayerInfoModel player2)
        {
            (player1, player2) = (player2, player1);
            return (player1, player2);
        }

        public static bool TakeShot(PlayerInfoModel player, PlayerInfoModel opponent)
        {
            Console.WriteLine("Where would you like to take your shot?");
            string shotLocation = Console.ReadLine();
            (string locationLetter, int locationNumber) = SeparateSpot(shotLocation);
            bool shipPresent = CheckForShot(opponent, locationLetter, locationNumber);
            bool markedStatus = MarkShot(player, locationLetter, locationNumber, shipPresent);
            return markedStatus;
        }

        private static bool CheckForShot(PlayerInfoModel opponent, string locationLetter, int locationNumber)
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

        private static (string locationLetter, int locationNumber) SeparateSpot(string location)
        {
            string locationLetter = location.Substring(0, 1).ToUpper();
            int locationNumber = Int32.Parse(location.Substring(1, 1));
            return (locationLetter, locationNumber);
        }

        private static bool MarkShot(PlayerInfoModel player, string locationLetter, int locationNumber, bool shipPresent)
        {
            foreach (var gridSpot in player.Grid)
            {
                if (gridSpot.SpotLetter == locationLetter && gridSpot.SpotNumber == locationNumber)
                {
                    if (shipPresent)
                    {
                        gridSpot.Status = GridSpotStatus.Hit;
                        return true;
                    }
                    else
                    {
                        gridSpot.Status = GridSpotStatus.Miss;
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            bool output = false;
            if (location.Length > 1)
            {
                (string locationLetter, int locationNumber) = SeparateSpot(location);
                foreach (var ship in model.Grid)
                {
                    if (locationLetter == ship.SpotLetter && locationNumber == ship.SpotNumber)
                    {
                        GridSpotModel spot = new GridSpotModel
                        {
                            SpotLetter = locationLetter,
                            SpotNumber = locationNumber,
                            Status = GridSpotStatus.Empty
                        };
                        model.ShipList.Add(spot);
                        output = true;
                    } 
                }
            }
            return output;
        }


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
        static void Fire()
        {

        }

        static void CanFire()
        {

        }
    }
}
