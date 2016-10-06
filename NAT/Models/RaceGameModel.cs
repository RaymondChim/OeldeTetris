using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAT.Models.Race;
using System.Diagnostics;

namespace NAT.Models {
    public class RaceGameModel : IRaceGameModel {
        public int counter = 0;
        public int gap = 0;
        public Race.Map[] Maps;
        public Car Ferrari { get; set; } //Потому что Ferrari для пиздатых мужиков
        public int Score { get; set; }
        public Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));

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
           // Maps[Ferrari.mapId].addCarToMap(Ferrari);
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

        public void ChangeCarMap() {
            if (Ferrari.mapId >= 2) throw new ArgumentException("Invalid Map Index");
            int[] id = { 1, 0 }; //Многоходовочка
            DeleteCarFromMap(Ferrari.mapId);
            Ferrari.mapId = id[Ferrari.mapId];
            Maps[Ferrari.mapId].addCarToMap(Ferrari);
        }

        public void DeleteCarFromMap(int mapId) {
            const int Harlamov = 17;
            foreach (Brick br in Maps[mapId].AllBricks) {
                if (br.Ypos >= Harlamov) {                        
                    foreach (Brick carbr in Ferrari.Bricks) {
                        if (br.Xpos == carbr.Xpos && br.Ypos == carbr.Ypos) {
                            Maps[mapId].AllBricks.Remove(br);
                        }
                    }
                }
            }
        }


        public void AddNewWall(int mapId) {
            if (counter == 15 && Ferrari.mapId == mapId) {
                Maps[mapId].NextBlock = new Race.Block(new Brick[10]);
                for (int i = 0; i < 10; i++) {
                    Maps[mapId].NextBlock.Bricks[i] = new Brick(i, 0);
                }
                Maps[mapId].addBlockToMap();
                return;
            }
            const int EMPTY_BRICS_IN_WALL = 3;
            const int AMOUNT_BRICKS_IN_WALL = 7;
            int FirstEmptyCell = rand.Next(AMOUNT_BRICKS_IN_WALL);
            Maps[mapId].NextBlock = new Race.Block(new Brick[7]);
            int j = -1;
            for (int i = 0; i < 7; i++) {
                if(FirstEmptyCell == i) {
                    j += EMPTY_BRICS_IN_WALL+1;
                } else {
                    j++;
                }
                Maps[mapId].NextBlock.Bricks[i] = new Brick(j, 0);
            }
            Maps[mapId].addBlockToMap();
        }

        private void AddNewWalls() {
            if (counter == 14) {
                gap -= 3;
            }
            
            AddNewWall(0);
            AddNewWall(1);
            if (counter == 15) {
                counter = 0;
            }
            counter++;
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

        public Action GameEnd { get; set; }
        

        //Добавить проверку коллизий и не только
        public void MoveCar(int direction) {
            if (direction != -1 && direction != 1 ) throw new ArgumentException("Invalid direction value");
            if (StopMove(direction)) {
                //GameEnd?.Invoke();
                return;
            }
            Car Lamborghini = new Car(Ferrari); //Ламбо для пацанов

            foreach (Brick br in Lamborghini.Bricks) {
                br.Xpos += direction;
            }
            if (CheckColision(Lamborghini.mapId, Lamborghini)) return;
            Ferrari = Lamborghini;
        }

        public bool StopMove(int direction) {
            foreach (Brick br in Ferrari.Bricks) {
                if ((br.Xpos == 0 && direction == -1)
                     || (br.Xpos == 9 && direction == 1)
                     || CheckColision(Ferrari.mapId, Ferrari)) {
                    return true;
                }
            }
            return false;
        }

        //Оно работает, не лезь со своими лямбдами!!!
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

        // Здесь тоже без лямбд хорошо
        public void MoveBlockDown(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            DeleteBlock(mapId);
            foreach (Race.Block bl in Maps[mapId].Walls) {
                foreach (Brick br in bl.Bricks) {
                    if (br.Ypos + 1 <= 19) {
                        br.Ypos++;
                    } 
                }
            }
        }

        // И тут!!!
        private void DeleteBlock(int mapId) {
            foreach (Race.Block bl in Maps[mapId].Walls) {
                if (bl.Bricks[0].Ypos == 19) {
                    foreach (Brick br in bl.Bricks) {
                        Maps[mapId].AllBricks.Remove(br);
                    }
                    Maps[mapId].Walls.Remove(bl);
                    return;
                }
            }
        }

        private void MovesBlockDown() {
            MoveBlockDown(0);
            MoveBlockDown(1);
        }

        
        public void ProcessTurn(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");

            if (CheckColision(Ferrari.mapId, Ferrari)) {
                //GameEnd?.Invoke();
                return;
            }
            if (!CheckColision(mapId, Ferrari)) {
                //Play
                Score += 10;
                MovesBlockDown();
                if(gap == 5) {
                    gap = 0;
                    AddNewWalls();
                }
                else {
                    gap++;
                }
            }
        }
    }
}
