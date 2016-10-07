using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAT.Menu
{
    public class Namer
    {
        private char[] name { get; set; } = { 'A', 'A', 'A'};
        public int pointer { get; set; } = 0;

        public void moveSelector(int direction) {
            if(direction != -1 && direction !=1) throw new ArgumentException("TI SHO SDUREL?");
            pointer += direction;
            if (pointer > 2) { pointer = 0; }
            if (pointer < 0) { pointer = 2; }
        }

        public void ChangeLetter(int direction) {
            if (direction != -1 && direction != 1) throw new ArgumentException("TI SHO SDUREL? (letter edition)");
            name[pointer] = (char)((int)name[pointer] + direction);
            if ((int)name[pointer] > 90) { name[pointer] = 'A'; }
            if ((int)name[pointer] < 65) { name[pointer] = 'Z'; }
        }

        public string SubmitName() { return new string(name) ; }
        
    }
}
