using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLibDotnet.Models
{
    public class PlayerInfoModel
    {
        public string Username { get; set; }
        public List<GridSpotModel> ShipList { get; set; } = new List<GridSpotModel>();
        public List<GridSpotModel> ShotGrid { get; set; } = new List<GridSpotModel>();
    }
}
