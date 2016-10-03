using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NAT.Models.Race {
    //ну блок это массив Brick невероятно правда?
    public class Block {
        public Brick[] Bricks { get; set; } = new Brick[10];

        public Block(Brick[] Bricks) {
            this.Bricks = Bricks;
        }

        //deepCopy
        public Block(Block other_block) {
            this.Bricks = new Brick[other_block.Bricks.Length];
            for (int i = 0; i < this.Bricks.Length; i++) {
                this.Bricks[i] = new Brick(other_block.Bricks[i]);
            }
        }

    }
}
