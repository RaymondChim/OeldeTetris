using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAT.Models;

namespace NAT.Views {
    public interface IGameView {
        // событие "отрабатывающее" пи нажатии клавиши пользователем
        Action<string> KeyPressed { get; }

        // Отображаеть модель
        void Display(IGameModel _model);
    }
}
