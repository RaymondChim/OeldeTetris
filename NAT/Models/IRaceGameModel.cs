using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAT.Models.Race;

namespace NAT.Models {
    public interface IRaceGameModel : IModel {

        //id карты лежит в машине, машина умная!
        Car Ferrari { get; set; } 

        // событие на конец игры
        Action GameEnd { get; set; }

        // отдай карту!
        Brick[] GetMap(int mapId);

        // отдай машинку!
        Car MainCar { get; }

        // обработать ход , те машинку вперед , просчитать коллизию и тд
        void ProcessTurn(int mapId);

        // сдвинуть машину по X на direction
        void MoveCar(int direction);

        //Поменять карту на которой находится машинка
        void ChangeCarMap();

    }
}
