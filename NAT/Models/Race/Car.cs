using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAT.Models.Race {
    public class Car {
        public Brick[] Bricks { get; set; } = new Brick[6];
        public int mapId;

        public Car(Brick[] Bricks, int mapId) {
            this.Bricks = Bricks;
            this.mapId = mapId;
        }

        //deepCopy
        public Car(Car other_car) {
            this.Bricks = other_car.Bricks;
            this.mapId = other_car.mapId;
        }
    }
}
