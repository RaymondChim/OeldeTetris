using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NAT.Models;
using System.Diagnostics;
using System.Windows;

using NAT.Services;

namespace NAT.Views {
    public interface ITetrisGameView : IView {
        // событие "отрабатывающее" пи нажатии клавиши пользователем

        // Отображаеть модель

        void TestDisplay();
        // тут вьюшка должна прочекать пользовательский ввод и вызвать событие если надо

        void DisplayGameOver();
    } }
 