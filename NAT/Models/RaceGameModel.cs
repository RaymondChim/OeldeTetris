using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAT.Models.Race;

namespace NAT.Models {
    class RaceGameModel {
        public int gap = 0;
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

        // TODO: Посмотреть Роберту
        public void ChangeCarMap() {
            if (Ferrari.mapId >= 2) throw new ArgumentException("Invalid Map Index");
            int[] id = { 1, 0 }; //Многоходовочка
            DeleteCarFromMap(Ferrari.mapId);
            Ferrari.mapId = id[Ferrari.mapId];
            Maps[Ferrari.mapId].addCarToMap(Ferrari);

            //РОБЕРТ ПОСМОТРИ СЮДА
            if (CheckColision(Ferrari.mapId, Ferrari))
            {
                GameOver?.Invoke();
            }
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

        //Использовать Maps[mapId].NextBlock && addBlockToMap()
        //проверить
        public void AddNewWall(int mapId) {
            const int EMPTY_BRICS_IN_WALL = 3;
            const int AMOUNT_BRICKS_IN_WALL = 7;
            Random rand = new Random();
            int FirstEmptyCell = rand.Next(AMOUNT_BRICKS_IN_WALL);
            Maps[mapId].NextBlock = new Race.Block(new Brick[7]);
            int j = 0;
            for (int i = 0; i < 7; i++) {
                if(FirstEmptyCell == i) {
                    j += EMPTY_BRICS_IN_WALL;
                } else {
                    j++;
                }
                Maps[mapId].NextBlock.Bricks[i] = new Brick(j, 0);
            }
            Maps[mapId].addBlockToMap();
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
            if (direction != -1 || direction != 1 ) throw new ArgumentException("Invalid direction value");
            if (StopMove(direction)) return;
            Car Lamborghini = new Car(Ferrari); //Ламбо для пацанов

            foreach (Brick br in Lamborghini.Bricks) {
                br.Xpos += direction; //Если что-то не работает, то смотри deepcopy
            }
            if (CheckColision(Lamborghini.mapId, Lamborghini)) return;
            Ferrari = Lamborghini;
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
        public bool CheckColision(int mapId, Car Lamborghini) {
            const int Harlamov = 17;
            foreach (Brick br in Maps[mapId].AllBricks) {
                if (br.Ypos >= Harlamov) {
                    foreach (Brick carbr in Lamborghini.Bricks) {
                        if (carbr.Xpos == br.Xpos && carbr.Ypos == br.Ypos) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // TODO: Проверить
        public void MoveBlockDown(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            for (int i = 0; i < Maps[mapId].Walls.Count(); i++) {
                foreach (Brick br in Maps[mapId].Walls[i].Bricks) {
                    if (br.Ypos + 1 <= 19) {
                        br.Ypos++;
                    } else {
                        Maps[mapId].Walls.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void ProcessTurn(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");

            /*if (CheckGameEnd())
            {
                GameOver?.Invoke();
                return;
            }*/
            if (!CheckColision(mapId, Ferrari)) {
                //Play
                Score += 10;
                MoveBlockDown(mapId);
                if(gap == 4) {
                    gap = 0;
                    AddNewWall(mapId);
                } else {
                    gap++;
                }

            }
        }
    }
}
