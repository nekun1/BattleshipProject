using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Models
{
    public class PlayerInfoModel
    {
        public string UserName { get; set; }
        public List<GridSpotModel> ShipList { get; set; }
        public List<GridSpotModel> ShotsFired { get; set; }
    }
}
