﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAT.Models {
    // нуу конкурс кто лучше напишет свой корте объявляется открытым
    // immutable потому что модно
    public class Brick {

        public int Xpos;
        public int Ypos;

        public Brick(int _XPos, int _YPos) {
            Xpos = _XPos;
            Ypos = _YPos;
        }


        //deepCopy
        public Brick(Brick other_block) {
            this.Xpos = other_block.Xpos;
            this.Ypos = other_block.Ypos;
        }

    }
    // конкурс окончем
    // победил пока я
}
