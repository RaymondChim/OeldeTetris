using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NAT.Models.Snake
{
    class Snake
    {
        public SnakeBrick head;
        public int length;

        public Snake(SnakeBrick _head, int _len) {
            head = _head;
            length = _len;

        }
    }
}
