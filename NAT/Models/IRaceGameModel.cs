using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAT.Models.Race;

namespace NAT.Models {
    public interface IRaceGameModel {
        
        // id блять карты на которой находится машинка
        int CarMapId { get; set; }

        // событие на конец игры
        Action GameEnd { get; set; }

        // отдай карту!
        Brick[] GetMap(int mapId);

        // отдай машинку!
        Car MainCar { get; }

        // обработать ход , те машинку вперед , просчитать коллизию и тд
        void ProcessTurn();

        // сдвинуть машину по X на direction
        void MoveCar(int direction);

    }
}
