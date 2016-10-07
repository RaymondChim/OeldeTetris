using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NAT.Services {
    public interface IBootingAnimationService {

        string GetCurrentString();

        bool Update(GameTime _time);

    }
}
