using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAT.Models {
    //ну блок это массив Brick невероятно правда?
    public class Block {
        public Brick[] Bricks { get; set; } = new Brick[0];
        public Brick Bind;      //Относительно какой точки вращаем
        public char BlockIndex; //I Z S J L T O  //Имя фигуры

        public Block(Brick[] _Bricks, char BlockIndex, Brick Bind) {
            Bricks = _Bricks;
            this.BlockIndex = BlockIndex;
            this.Bind = Bind;
        }

    }
}
