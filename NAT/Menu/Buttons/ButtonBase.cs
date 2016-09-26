using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NAT.Menu.Buttons {
    public abstract class ButtonBase {
        public abstract bool IsSelected { get; }
        public abstract Dictionary<Tuple<int, int>, ButtonBase> GetRoutes { get; }

        public Rectangle Rect { get; set; }

        public Action OnClick { get; set; }
        public Action OnSelected { get; set; }
        public Action<ButtonBase> OnSelectedOther { get; set; }

        public abstract void Select();
        public abstract void SelectOther(ButtonBase other);

        public abstract void Draw(SpriteBatch spriteBranch);

    }
}
