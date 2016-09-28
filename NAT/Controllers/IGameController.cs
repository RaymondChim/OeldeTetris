using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NAT.Models;
using NAT.Views;

namespace NAT.Controllers {
    interface IGameController {
        // инициализирует контроллер ( я не могу описывать конструктооры в интерфейсах да)
        void Init(IGameModel _model, IGameView _view);
        // запускает игру
        void Start();

        // обновляет все это барахло, следует вызывать в глобальном update
        void Update(GameTime _time);

        // вызывает отрисовку вьюшки
        void Render();
    }
}
