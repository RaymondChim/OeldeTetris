using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace NAT.Services {

    [Serializable]
    public class ScoreHeaver {
        [XmlAttribute("Name")]
        public string Name;
        [XmlAttribute("Count")]
        public int Score;

        public ScoreHeaver(string _Name,int _Score) {
            Score = _Score;
            Name = _Name;
        }

        public ScoreHeaver() {}
    }

    [Serializable]
    [XmlRoot("ScoreTable")]
    public class Scores {
        [XmlArray("Scores")]
        [XmlArrayItem("Score")]
        public ScoreHeaver[] Heaver;

        public Scores() { }
    }

    public interface IScoreService {
        Scores GetScores();
        void AddScore(Tuple<string, int> score);
    }
}
