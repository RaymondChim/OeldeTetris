using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using System.Xml.Serialization;
using System.IO;

namespace NAT.Services {
    public class BootingAnimationService : IBootingAnimationService {

        private const string DEFAULT_BOOTING_FILE = "booting.xml";

        [XmlRoot("BootingLogs")]
        public class BootingLogs {
            [XmlAttribute]
            public string DelaySyms;
            [XmlAttribute]
            public int Delay;
            [XmlElement]
            public string message;
        } 


        public readonly string _bootingFilename;
        private string CurrentString { get; set; } = "";
        private BootingLogs _Logs { get; set; } = new BootingLogs();
        private int LastCarriagePosition = 0;
        private int _timer = 0; 

        public BootingAnimationService() {
            this._bootingFilename = DEFAULT_BOOTING_FILE;
            LoadBootingFile();
        }

        public BootingAnimationService(string filename) {
            this._bootingFilename = filename;
            LoadBootingFile();
        }

        private void LoadBootingFile() {
            var serializer = new XmlSerializer(typeof(BootingLogs));
            try {
                _Logs = serializer.Deserialize(new FileStream(_bootingFilename, FileMode.Open)) as BootingLogs;
            } catch {
                // not today
            }
        }

        public string GetCurrentString() {
            return CurrentString;
        }

        private bool isTimerElapsed() {
            return _timer >= _Logs.Delay;
        }

        public bool Update(GameTime _time) {
            _timer += _time.ElapsedGameTime.Milliseconds;
            for (var i = LastCarriagePosition; i < _Logs.message.Length ; i++) {
                if (_Logs.DelaySyms.Contains(_Logs.message[i])) {
                    if (isTimerElapsed()) {
                        CurrentString += _Logs.message[i];
                        _timer = 0;
                    }else return false; // wating
                } else CurrentString += _Logs.message[i];
                LastCarriagePosition++;
            }return true; 
        }

    }
}
