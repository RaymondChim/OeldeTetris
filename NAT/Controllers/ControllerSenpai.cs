using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NAT.Controllers {

    public class ControllerSenpai {

        public static bool AnySelector(KeyValuePair<string, Tuple<IGameController, bool>> x) {
            return true;
        }

        public static bool ActiveSelector(KeyValuePair<string, Tuple<IGameController, bool>> x) {
            return x.Value.Item2;
        }

        public Dictionary<string, Tuple<IGameController, bool>> _controllers 
            = new Dictionary<string, Tuple<IGameController, bool>>();


        public ControllerSenpai(List<Tuple<string,IGameController>> controllers) {
            this._controllers = controllers.ToDictionary(x => x.Item1 ,x => new Tuple<IGameController,bool>(x.Item2,false));
        }

        public void SetControllerActive(string name, bool active) {
            if (!_controllers.ContainsKey(name)) return;
            else _controllers[name] = new Tuple<IGameController, bool>(_controllers[name].Item1, active);
        }

        public void SetAllControllersActive(bool active) {
            _controllers = _controllers.ToDictionary(x => x.Key, x => new Tuple<IGameController, bool>(x.Value.Item1,active));    
        }

        public void Update(GameTime _time, Func<KeyValuePair<string, Tuple<IGameController, bool>>,bool> selector) {
            foreach (var controller in GetBySelector(selector)) controller.Update(_time);
        }
        public void Render(Func<KeyValuePair<string, Tuple<IGameController, bool>>, bool> selector) {
            foreach (var controller in GetBySelector(selector)) controller.Render();
        }
        public void Start(Func<KeyValuePair<string, Tuple<IGameController, bool>>, bool> selector) {
            foreach (var controller in GetBySelector(selector)) controller.Start();
        }

        private IEnumerable<IGameController> GetBySelector(Func<KeyValuePair<string, Tuple<IGameController, bool>>, bool> selector) {
            return _controllers.Where(x => selector(x) ).Select(x => x.Value.Item1);
        }
    }
}
