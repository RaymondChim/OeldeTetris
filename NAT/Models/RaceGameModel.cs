using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAT.Models.Race;

namespace NAT.Models {
    class RaceGameModel {

        public Race.Map[] Maps;
        public Car Ferrari;             //Потому что Ferrari для пиздатых мужиков
        public int Score { get; set; }
        private int _CarMapId = 0;
        public int CarMapId {
            get {
                return _CarMapId;
            }
            set {
                _CarMapId = value;
            }
        }

        public RaceGameModel() {
            Score = 0;
            //Ferrari - только ручная работа
            Ferrari = new Car(new Brick[]  {new Brick(5, 17),
                                                 new Brick(4, 18),
                                                 new Brick(5, 18),
                                                 new Brick(6, 18),
                                                 new Brick(4, 19),
                                                 new Brick(6, 19)},
                                                 0);
            Maps = new Race.Map[] { new Race.Map(), new Race.Map() };
            Maps[Ferrari.mapId].addCarToMap(Ferrari);
        }

        // TODO: Проверить вхождение в стену при переходе на новую карту
        public void ChangeCarMap() {
            if (Ferrari.mapId >= 2) throw new ArgumentException("Invalid Map Index");
            int[] id = { 1, 0 }; //Многоходовочка
            DeleteCarFromMap(Ferrari.mapId);
            Ferrari.mapId = id[Ferrari.mapId];
            Maps[Ferrari.mapId].addCarToMap(Ferrari);
        }

        //Может не работать
        public void DeleteCarFromMap(int mapId) {
            const int Harlamov = 17;
            foreach (Brick br in Maps[mapId].AllBricks) {
                //Проверяем только 3 нижние строки
                if (br.Ypos >= Harlamov) {                        
                    foreach (Brick carbr in Ferrari.Bricks) {
                        if (br.Xpos == carbr.Xpos && br.Ypos == carbr.Ypos) {
                            Maps[mapId].AllBricks.Remove(br);
                        }
                    }
                }
                    
            }
        }

        public RaceGameModel(int Score) {
            this.Score = Score;
            Maps = new Race.Map[] { new Race.Map(), new Race.Map() };
        }

        public void AddNewWall(int mapId) {

        }

        public Brick[] GetMap(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].AllBricks.ToArray();
        }

        public Car MainCar {
            get {
                return Ferrari;
            }
        }

        public int CurrentScore {
            get {
                return Score;
            }
        }

        public Action GameOver { get; set; }


        //Добавить проверку коллизий и не только
        public void MoveCar(int direction) {
            
        }

        public bool StopMove(int direction) {
            foreach (Brick br in Ferrari.Bricks) {
                if ((br.Xpos == 0 && direction == -1) || (br.Xpos == 9 && direction == 1)) {
                    return true;
                } 
            }
            return false;
        }

        //Вроде работает
        public bool CheckColision(int mapId) {
            const int Harlamov = 17;
            foreach (Brick br in Maps[mapId].AllBricks) {
                if (br.Ypos >= Harlamov) {
                    foreach (Brick carbr in Ferrari.Bricks) {
                        if (carbr.Xpos == br.Xpos && carbr.Ypos == br.Ypos) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // TODO: Переписать
        // TODO: Проверить это говно (обожаю лямбды)
        public void MoveBlockDown(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            for (int i = 0; i < Maps[mapId].Walls.Length; i++) {
                for (int j = 0; j < Maps[mapId].Walls[i].Bricks.Length; j++) {
                    if ((Maps[mapId].Walls[i].Bricks[j].Ypos + 1) > 19) {
                        Maps[mapId].AllBricks.RemoveAll(x => Maps[mapId].AllBricks.Contains(Maps[mapId].Walls[i].Bricks[j]));
                        break;
                    }
                    Maps[mapId].Walls[i].Bricks[j].Ypos++;
                }
            }
        }

        public void ProcessTurn(int mapId) {

        }
    }
}
