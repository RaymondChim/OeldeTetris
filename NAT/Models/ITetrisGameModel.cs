using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAT.Models;

namespace NAT.Models {
    public interface ITetrisGameModel {

        int Score { get; set; }

        Action GameOver { get; set; }
        Action<int> MapLocked { get;set;}
        Action<int> MapUnlocked { get; set; }

        // идентификатор текущей открытой карты
        int CurrentMapId { get; set; }

        // набранный счет
        int CurrentScore { get; }

        // "запеченная" карты карты :)
        Brick[] BrickMap(int mapId);
        // текущий падающий блок на карте
        Block GetCurrentBlock(int mapId);

        // кэп
        void MoveCurrentBlock(int direction, int mapId);
        void FlipCurrentBlock(int mapId);

        // опускает текущий блок вниз и просчитывает закрытие линий на всех картах
        void ProccessTurn(int mapId);

        // следующий блок который будет падать по идентификатору карты
        Block GetNextBlock(int mapId);

    }
}
