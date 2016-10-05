using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NAT.Models;
using NAT.Views;

namespace NAT.Controllers {
    class RaceGameController : ControllerBase<IRaceGameModel, IRaceGameView>, IGameController {

        public void Render() {
            _view.Display(_model);
        }

        public new void Start() {
            base.Start();
            _model.GameEnd += () => {
                _view.DisplayGameOver();
            };
        }

        public new void Update(GameTime _time) {
            base.Update(_time);

            if (_GameTurnTimer[0] >= GameTurnDelta[0]) {
                _model.ProcessTurn(0);
                _model.ProcessTurn(1);
            }

            _model.ProcessTurn(_model.Ferrari.mapId);
        }

        protected override void ProcessInput(Keys key) {
            if (key == Keys.Left) _model.MoveCar(-1);
            if (key == Keys.Right) _model.MoveCar(1);
            if (key == Keys.Space)
                _model.Ferrari.mapId = _model.Ferrari.mapId == 0 ? 1 : 0;
        }
    }
}
