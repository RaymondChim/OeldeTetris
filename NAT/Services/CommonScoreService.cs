using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.IO;

namespace NAT.Services {
    public class CommonScoreService : IScoreService {

        private const string SCORE_FILENAME = @"scores.xml";

        public void AddScore(Tuple<string, int> score) {
            try { 
                var serializer = new XmlSerializer(typeof(Scores));
                var readStream = new FileStream(SCORE_FILENAME, FileMode.OpenOrCreate);
                var scores = serializer.Deserialize(readStream) as Scores;
                readStream.Close();
                var writeStream = new FileStream(SCORE_FILENAME, FileMode.Open);
                scores.Heaver = scores.Heaver.Union(new ScoreHeaver[] { new ScoreHeaver(score.Item1,score.Item2) }).ToArray();
                serializer.Serialize(writeStream,scores);
                writeStream.Close();
            }catch(Exception) {
                // it is not ok guys
            }
        }

        public Scores GetScores() {
            try { 
                var serializer = new XmlSerializer(typeof(Scores));
                var stream = new FileStream(SCORE_FILENAME, FileMode.Open);
                var scores = serializer.Deserialize(stream) as Scores;
                stream.Close();
                return scores;
            }catch(Exception) {
                return new Scores();
            }
        }
    }
}
