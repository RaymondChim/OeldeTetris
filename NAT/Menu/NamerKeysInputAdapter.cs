using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace NAT.Menu {
    public class NamerKeysInputAdapter {
        private readonly Namer _namer;

        public NamerKeysInputAdapter(Namer namer) {
            this._namer = namer;
        }

        public List<Keys> IgnoreList = new List<Keys>();
        public bool Update() {
            var state = Keyboard.GetState();
            foreach (var key in state.GetPressedKeys().Where(x => !IgnoreList.Contains(x))) {
                if (key == Keys.Up) _namer.ChangeLetter(-1);
                if (key == Keys.Down) _namer.ChangeLetter(1);
                if (key == Keys.Left) _namer.moveSelector(-1);
                if (key == Keys.Right) _namer.moveSelector(1);
                if (key == Keys.Space) return true;
            }
            IgnoreList = state.GetPressedKeys().ToList();
            return false;
        }

        public Texture2D trTex;
        public Texture2D trTexD;

        public void LoadContent(ContentManager content, string tTexName, string tDTexName) {
            trTex = content.Load<Texture2D>(tTexName);
            trTexD = content.Load<Texture2D>(tDTexName);  
        }

        public void DrawNamer(SpriteBatch spriteBatch, SpriteFont font, Vector2 position, Color color) {
            spriteBatch.Draw(trTex, new Rectangle((int)position.X - 1 + 30 * _namer.pointer , (int)position.Y - 12, 21, 13), color);
            spriteBatch.Draw(trTexD, new Rectangle((int)position.X - 1 + 30 * _namer.pointer, (int)position.Y + 35, 21, 13), color);

            spriteBatch.DrawString(font,_namer.SubmitName(),position,color);
        }

        public string GetName() {
            return _namer.SubmitName();
        }

    }
}
