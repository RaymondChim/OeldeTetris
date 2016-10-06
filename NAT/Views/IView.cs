using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using NAT.Services;
using NAT.Models;

namespace NAT.Views {
    public interface IView {
        void LoadContent();

        void Init(Scores scores);

        Keys[] UpdateuserInput();

        void Display(IModel _model);

    }
}
