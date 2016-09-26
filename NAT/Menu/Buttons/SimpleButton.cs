using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NAT.Menu.Buttons {
    class SimpleButton : ButtonBase {
        public Texture2D SelectedTexture;
        public Texture2D UnselectedTexture;

        public Texture2D CurrentTexture {get {
                return IsSelected ? SelectedTexture : UnselectedTexture;
            } }

        public Dictionary<Tuple<int, int>, ButtonBase> _routes;
        public override Dictionary<Tuple<int, int>, ButtonBase> GetRoutes {
            get {
                return _routes;
            }
        }

        public SimpleButton(Rectangle _Rect) {
            Rect = _Rect;
        }

        public SimpleButton(Rectangle _Rect, Texture2D SelectedTex, Texture2D UnselectedTex) {
            Rect = _Rect;
            SelectedTexture = SelectedTex;
            UnselectedTexture = UnselectedTex;
        }

        private bool _isSelected;
        public override bool IsSelected {
            get {
                return _isSelected;
            }
        }

        public override void Select() {
            _isSelected = true;
            OnSelected?.Invoke();
        }

        public override void SelectOther(ButtonBase other) {
            _isSelected = false;
            OnSelectedOther?.Invoke(other);
        }

        public override void Draw(SpriteBatch spriteBranch) {
            spriteBranch.Draw(CurrentTexture, Rect, Color.White);
        }
    }
}
