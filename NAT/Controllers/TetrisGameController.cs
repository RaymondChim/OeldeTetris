using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NAT.Models;
using NAT.Services;
using NAT.Views;

namespace NAT.Controllers {
    public class TetrisGameController : ControllerBase<ITetrisGameModel,ITetrisGameView>, IGameController {

        private List<Keys> PossibleIgnoreKeys {
            get {
                return new List<Keys>() { Keys.Space , Keys.Up};
            } }

        public bool ProcessTurns { get; set; } = true;

        public void Render() {
            _view.Display(_model);
        }

        public new void Start() {
            base.Start();

            _model.GameOver += () =>{
                _view.DisplayGameOver();
                ProcessTurns = false;
            };

            _model.MapLocked += (mapId) => {
                GameTurnDelta[mapId] = 70;
            };
            _model.MapUnlocked += (mapId) =>{
                GameTurnDelta[mapId] = 500;
            };


        }

        public new void Update(GameTime _time) {
            if (!ProcessTurns) return;

            base.Update(_time);

            if (_GameTurnTimer[0] >= GameTurnDelta[0]) {
                _model.ProccessTurn(0);
                _GameTurnTimer[0] = 0;
            }

            if (_GameTurnTimer[1] >= GameTurnDelta[1]) {
                _model.ProccessTurn(1);
                _GameTurnTimer[1] = 0;
            }

        }

        protected override void ProcessInput(Keys key) {
            if (IgnoreKeys.Contains(key)) return;
            if (!ProcessTurns) return;

            if (key == Keys.Left) _model.MoveCurrentBlock(-1, _model.CurrentMapId);
            if (key == Keys.Right) _model.MoveCurrentBlock(1, _model.CurrentMapId);
            if (key == Keys.Up) _model.FlipCurrentBlock(_model.CurrentMapId);
            if (key == Keys.Down) _model.ProccessTurn(_model.CurrentMapId);

            if (key == Keys.Space) _model.CurrentMapId = _model.CurrentMapId == 0 ? 1 : 0;

            if(PossibleIgnoreKeys.Contains(key))IgnoreKeys.Add(key);

        }
    }
}
