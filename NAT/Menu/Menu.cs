using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using NAT.Menu.Buttons;

namespace NAT.Menu {
    public class GameMenu {
        public List<ButtonBase> Buttons { get; private set; } = new List<ButtonBase>();

        public ButtonBase CurrentSelectedButton { get; private set; }

        public bool IsHidden { get; set; } = false;

        public Keys LeftRouteKey = Keys.Left;
        public Keys RightRouteKey = Keys.Right;
        public Keys DownRouteKey = Keys.Down;
        public Keys UpRouteKey = Keys.Up;
        public Keys ClickKey = Keys.Enter;

        public Dictionary<Keys, Tuple<int, int>> RouteDirections { get {
                return new Dictionary<Keys, Tuple<int, int>> {
                        { LeftRouteKey  , new Tuple<int, int>(-1,0)  },
                        { RightRouteKey  , new Tuple<int, int>(1,0)  },
                        { DownRouteKey  , new Tuple<int, int>(0,-1)  },
                        { UpRouteKey  , new Tuple<int, int>(0,1)  },
                    };
            }
            
        } 

        public void UpdateAll() {
            if (IsHidden) return;
            if (Buttons.Count == 0) return;
            if (CurrentSelectedButton == null) {
                CurrentSelectedButton = Buttons.First();
                CurrentSelectedButton.Select();
            }
            var state = Keyboard.GetState();
            Tuple<int, int> direction = null;
            if (state.IsKeyDown(LeftRouteKey)) direction = RouteDirections[LeftRouteKey];
            if (state.IsKeyDown(RightRouteKey)) direction = RouteDirections[RightRouteKey];
            if (state.IsKeyDown(DownRouteKey)) direction = RouteDirections[DownRouteKey];
            if (state.IsKeyDown(UpRouteKey)) direction = RouteDirections[UpRouteKey];

            if(direction != null) {
                if (CurrentSelectedButton.GetRoutes.ContainsKey(direction)) { 
                    var NextButton = CurrentSelectedButton.GetRoutes[direction];
                    if(NextButton != null) {
                        CurrentSelectedButton.SelectOther(NextButton);
                        NextButton.Select();
                        CurrentSelectedButton = NextButton;
            }}}

            if (state.IsKeyDown(ClickKey))
                CurrentSelectedButton?.OnClick?.Invoke();

        }

        public void DisplayAll(SpriteBatch spriteBatch ) {
            if (IsHidden) return;
            spriteBatch.Begin();
            foreach (var button in Buttons)
                button.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
