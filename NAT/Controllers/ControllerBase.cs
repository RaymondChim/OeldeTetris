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
    public abstract class ControllerBase<MT,VT> 
        where MT : class, IModel
        where VT : class, IView {

        public MT _model { get; protected set; }
        public VT _view { get; protected set; }

        public IScoreService _scoreService { get; protected set; }

        protected List<Keys> IgnoreKeys = new List<Keys>();

        public virtual int[] GameTurnDelta { get; protected set; } = new int[] { 500, 500 };
        protected int[] _GameTurnTimer = new int[] { 0, 0 };

        protected virtual int minTurnDelta { get; set; } = 250;
        protected virtual int startTurnDelta { get; set; } = 500;

        protected virtual int GameTurnDecreaseIndex { get; set; } = 15000;
        protected virtual int GameInputDecreaseIndex { get; set; } = 8000;

        public virtual int GameInputDelta { get; protected set; } = 75;
        protected int _GameInputTimer = 0;


        public void Init(IModel model, IView view) {
            if (!(model is MT)) throw new ArgumentException("invalid model type sorry");
            if (!(view is VT)) throw new ArgumentException("invalid view type sorry");

            this._model = model as MT;
            this._view = view as VT;
            _scoreService = new CommonScoreService();
        }

        private int defInputDelta;

        public void Start() {
            defInputDelta = GameInputDelta;
            _view.LoadContent();
            _view.Init(_scoreService.GetScores());
        }

        public void Update(GameTime _time) {
            _GameTurnTimer[0] += _time.ElapsedGameTime.Milliseconds;
            _GameTurnTimer[1] += _time.ElapsedGameTime.Milliseconds;

            _GameInputTimer += _time.ElapsedGameTime.Milliseconds;

            if (_GameInputTimer >= GameInputDelta) {
                var input = _view.UpdateuserInput();
                foreach (var key in input)
                    ProcessInput(key);
                _GameInputTimer = 0;
                IgnoreKeys = IgnoreKeys.Where(x => input.Contains(x)).ToList();
            }

            GameTurnDelta = GameTurnDelta
            .Select(x =>
               x < minTurnDelta ? x :
                startTurnDelta - (int)Math.Floor(Math.Sqrt(_model.CurrentScore / GameTurnDecreaseIndex)) < minTurnDelta ? minTurnDelta : startTurnDelta - (int)Math.Floor(Math.Sqrt(_model.CurrentScore / GameTurnDecreaseIndex)))
            .ToArray();
            GameInputDelta = defInputDelta - (int)Math.Floor(Math.Sqrt(_model.CurrentScore / GameInputDecreaseIndex)) < 5 ? 5 : 75 - (int)Math.Floor(Math.Sqrt(_model.CurrentScore / GameInputDecreaseIndex));
        }

        protected abstract void ProcessInput(Keys key);

    }
}
