using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAT.Models.Snake
{
    class SnakeBrick
    {
        public int Xpos;
        public int Ypos;
        public int nextX;
        public int nextY;
        SnakeBrick tail;
        int mapID;

        public SnakeBrick(int _Xpos, int _Ypos, int _nextX, int _nextY, SnakeBrick _tail, int _mapID) {
            Xpos = _Xpos;
            Ypos = _Ypos;
            nextX = _nextX;
            nextY = _nextY;
            tail = _tail;
            mapID = _mapID;
        }


    }
}
