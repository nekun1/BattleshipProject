using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Models
{
    public class PlayerInfoModel
    {
        public string Username { get; set; }
        public int  Points { get; set; }
        public List<GridSpotModel> ShipList { get; set; } = new List<GridSpotModel>();
        public List<GridSpotModel> Grid { get; set; } = new List<GridSpotModel>();
    }
}
