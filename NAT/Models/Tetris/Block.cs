using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NAT.Models {
    //ну блок это массив Brick невероятно правда?
    public class Block {
        public Brick[] Bricks { get; set; } = new Brick[0];
        public Brick Bind;      //Относительно какой точки вращаем
        public char BlockIndex; //I Z S J L T O  //Имя фигуры

        public int BindBlockIndex;

        public Block(Brick[] _Bricks, char BlockIndex, Brick Bind, int _bindBlock = 0) {
            Bricks = _Bricks;
            this.BlockIndex = BlockIndex;
            this.Bind = Bind;
            BindBlockIndex = _bindBlock;
        }

        //deepCopy
        public Block(Block other_block) {
            Debug.WriteLine(other_block.Bricks.Length);
            this.Bricks = new Brick[other_block.Bricks.Length];
            for (int i = 0; i < this.Bricks.Length; i++) {
                this.Bricks[i] = new Brick(other_block.Bricks[i]);
            }
            this.Bind = new Brick(other_block.Bind);
            this.BlockIndex = other_block.BlockIndex;
        }

    }
}
