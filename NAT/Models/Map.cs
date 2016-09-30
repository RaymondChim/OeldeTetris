using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAT.Models {
    public class Map {
        public List<Brick> AllBricks = new List<Brick>();

        public Block CurrentBlock;

        public Block NextBlock;

        public int CurrentBlockPassTurns = 0;

        public void addBlockToMap() {
            for(int i = 0; i < 4; i++) {
                Debug.WriteLine("Block to Map index " + i + ": " + CurrentBlock.Bricks[i].Xpos + " " + CurrentBlock.Bricks[i].Ypos);
                AllBricks.Add(CurrentBlock.Bricks[i]);
            }
                
        }

        public bool IsLocked { get; set; } = false;
    }
}
