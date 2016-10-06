using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAT.Models
{
    class SnakeGameModel
    {
        public Map[] Maps;
        public int Score { get; set; }
        private int _CurrentMapId = 0;
        public int CurrentMapId
        {
            get
            {
                return _CurrentMapId;
            }
            set
            {
                _CurrentMapId = value;
            }
        }

        public int CurrentScore
        {
            get
            {
                return Score;
            }
        }

        public Action GameOver { get; set; }

        public Action<int> MapLocked { get; set; }

        public Action<int> MapUnlocked { get; set; }

        public SnakeGameModel()
        {
            Score = 0;
            Maps = new Map[] { new Map(), new Map() };
            var rnd = new Random();

        }


        public Brick[] BrickMap(int mapId)
        {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].AllBricks.ToArray();
        }




    }
}
