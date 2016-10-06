using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAT.Models.Race {
    public class Map {
        public List<Brick> AllBricks = new List<Brick>();

        public Block NextBlock;

        //Все стены вместе
        public List<Block> Walls = new List<Block>();

        public int CurrentBlockPassTurns = 0;

        public void addBlockToMap() {
            foreach (Brick br in NextBlock.Bricks) {
                AllBricks.Add(br);
            }
            Walls.Add(NextBlock);
        }

        public void addCarToMap(Car Ferrari) {
            for (int i = 0; i < Ferrari.Bricks.Length; i++) {
                AllBricks.Add(Ferrari.Bricks[i]);
            }
        }

        public bool IsLocked { get; set; } = false;
    }
}
