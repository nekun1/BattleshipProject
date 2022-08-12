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

        public static void MakeMove(PlayerInfoModel player, PlayerInfoModel opponent)
        {
            (string locationLetter, int locationNumber) = GetShotLocation();
            foreach(var gridSpot in opponent.ShipList)
            {
                if (gridSpot.SpotLetter == locationLetter && gridSpot.SpotNumber == locationNumber)
                {
                    foreach(var grid in player.Grid)
                    {
                        if (grid.SpotLetter == locationLetter && grid.SpotNumber == locationNumber)
                        {
                            grid.Status = GridSpotStatus.Hit;
                        }
                    }
                } 
            }
        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            if (location.Length > 1)
            {
                string locationLetter = location.Substring(0, 1).ToUpper();
                int locationNumber = Int32.Parse(location.Substring(1, 1));
                if ((locationLetter == "A" || locationLetter == "B" || locationLetter == "C" || locationLetter == "D" || locationLetter == "E") && (locationNumber == 1 || locationNumber == 2 || locationNumber == 3 || locationNumber == 4 || locationNumber == 5))
                {
                    GridSpotModel spot = new GridSpotModel
                    {
                        SpotLetter = locationLetter,
                        SpotNumber = locationNumber,
                        Status = GridSpotStatus.Empty
                    };
                    model.ShipList.Add(spot);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
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
