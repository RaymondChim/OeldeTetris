using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using NAT.Models;
using NAT.Services;

namespace NAT.Views {
    public interface IRaceGameView : IView {


        // нарисуй!
        void Display(IRaceGameModel _model);

        void DisplayGameOver();

    }
}
