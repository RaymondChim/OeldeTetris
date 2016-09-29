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
    public class GameController : IGameController {

        public int GameTurnDelta { get; private set; } = 500;
        private int _GameTurnTimer = 0;

        public int GameInputDelta { get; private set; } = 75;
        private int _GameInputTimer = 0;

        public IGameModel _model { get; private set; }
        public IGameView _view { get; private set; }

        public IScoreService _scoreService { get; private set; }

        private List<Keys> IgnoreKeys  = new List<Keys>();
        private List<Keys> PossibleIgnoreKeys = new List<Keys>() { Keys.Space , Keys.Up};

        public bool ProcessTurns { get; set; } = true;

        public void Init(IGameModel _model, IGameView _view) {
            this._model = _model;
            this._view = _view;
            _scoreService = new CommonScoreService();
        }

        public void Render() {
            _view.Display(_model);
        }

        public void Start() {
            _view.LoadContent();
            _view.Init(_scoreService.GetScores());

            _model.GameOver += () =>{
                _view.DisplayGameOver();
                ProcessTurns = false;
            };
        }

        public void Update(GameTime _time) {
            if (!ProcessTurns) return;

            _GameTurnTimer += _time.ElapsedGameTime.Milliseconds;
            _GameInputTimer += _time.ElapsedGameTime.Milliseconds;

            if(_GameInputTimer >= GameInputDelta) {
                var input = _view.UpdateuserInput();
                foreach (var key in input)
                    ProcessInput(key);
                _GameInputTimer = 0;
                IgnoreKeys = IgnoreKeys.Where(x => input.Contains(x)).ToList();
            }

            if (_GameTurnTimer >= GameTurnDelta) {
                _model.ProccessTurn(0);
                _model.ProccessTurn(1);
                _GameTurnTimer = 0;
            }

        }

        private void ProcessInput(Keys key) {
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
