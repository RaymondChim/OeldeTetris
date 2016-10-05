using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using NAT.Services;

namespace NAT.Views {
    public interface IView {
        void LoadContent();

        void Init(Scores scores);

        Keys[] UpdateuserInput();
    }
}
