using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NAT.Models;

namespace NAT.Models {
    public class GameModel : IGameModel {

        public Map[] Maps;
        public int Score { get; set; }
        private int _CurrentMapId = 0;
        public int CurrentMapId {
            get {
                return _CurrentMapId;
            }
            set {
                _CurrentMapId = value;
            }
        }

        public int CurrentScore {
            get {
                return Score;
            }
        }

        public Action GameOver { get; set; }

        public Action<int> MapLocked { get; set; }

        public Action<int> MapUnlocked { get; set; }

        public GameModel() {
            Score = 0;
            Maps = new Map[] { new Map(), new Map() };
            var rnd = new Random();
            Maps[0].CurrentBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
            Maps[1].CurrentBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
            Maps[0].NextBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
            Maps[1].NextBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
        }

        public Brick[] BrickMap(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].AllBricks.ToArray();
        }

        public Block CreateBlock(char BlockIndex) {
            switch (BlockIndex) {
                case 'I':
                    Block I = new Block(new Brick[]
                                        { new Brick(4, 0),
                                          new Brick(4, 1),
                                          new Brick(4, 2),
                                          new Brick(4, 3)},
                                          BlockIndex,
                                          new Brick(4, 1));
                    return I;
                #region other_switch
                case 'Z':
                    Block Z = new Block(new Brick[]
                                        { new Brick(4, 0),
                                          new Brick(4, 1),
                                          new Brick(5, 1),
                                          new Brick(5, 2)},
                                          BlockIndex,
                                          new Brick(5, 1));
                    return Z;
                case 'S':
                    Block S = new Block(new Brick[]
                                        { new Brick(5, 0),
                                          new Brick(4, 1),
                                          new Brick(5, 1),
                                          new Brick(4, 2)},
                                          BlockIndex,
                                          new Brick(5, 1));
                    return S;
                case 'J':
                    Block J = new Block(new Brick[]
                                        { new Brick(4, 0),
                                          new Brick(4, 1),
                                          new Brick(4, 2),
                                          new Brick(5, 2)},
                                          BlockIndex,
                                          new Brick(4, 1), 2);
                    return J;
                case 'L':
                    Block L = new Block(new Brick[]
                                        { new Brick(4, 2),
                                          new Brick(5, 0),
                                          new Brick(5, 1),
                                          new Brick(5, 2)},
                                          BlockIndex,
                                          new Brick(5, 1));
                    return L;
                case 'T':
                    Block T = new Block(new Brick[]
                                        { new Brick(5, 0),
                                          new Brick(4, 1),
                                          new Brick(5, 1),
                                          new Brick(5, 2)},
                                          BlockIndex,
                                          new Brick(5, 1));
                    return T;
                case 'O':
                    Block O = new Block(new Brick[]
                                        { new Brick(4, 0),
                                          new Brick(4, 1),
                                          new Brick(5, 0),
                                          new Brick(5, 1)},
                                          BlockIndex,
                                          new Brick(4, 0));
                    return O;
                default:
                    throw new ArgumentException("Invalid Block Index");
            }
            #endregion
        }

        public void FlipCurrentBlock(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            if (Maps[mapId].CurrentBlock.BlockIndex == 'O') return;
            Flip(mapId);
        }

        public void Flip(int mapId) {
            Block shape = new Block(GetCurrentBlock(mapId));
            Brick crutch = new Brick(-1, -1);
            Brick[,] matrix = new Brick[5, 5];
            Brick[,] new_matrix = new Brick[5, 5];
            for (int y = 0; y < 5; y++) {
                for (int x = 0; x < 5; x++) {
                    foreach (Brick br in shape.Bricks) {
                        if (x == (br.Xpos - shape.Bind.Xpos + 2)
                            && y == (br.Ypos - shape.Bind.Ypos + 2)) {
                            matrix[y, x] = br;
                            break;
                        } else {
                            matrix[y, x] = crutch;
                        }
                    }

                }
            }
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    new_matrix[j, 4 - i] = matrix[i, j];
                    foreach (Brick br in shape.Bricks) {
                        if (br.Xpos == new_matrix[j, 4 - i].Xpos
                            && br.Ypos == new_matrix[j, 4 - i].Ypos) {
                            new_matrix[j, 4 - i].Xpos += ((4 - i) - j);
                            new_matrix[j, 4 - i].Ypos += (j - i);
                        }
                    }
                }
            }
            FlipEnable(mapId, shape);
        }

        public bool FlipEnable(int mapId, Block shape) {
            if (Maps[mapId].CurrentBlock.Bricks.Any(x => x.Ypos >= 19))
                return false;
            for (int i = 0; i < shape.Bricks.Length; i++) {
                if (shape.Bricks[i].Ypos < 0
                    || shape.Bricks[i].Ypos > 19
                    || shape.Bricks[i].Xpos < 0
                    || shape.Bricks[i].Xpos > 9) {
                    Debug.WriteLine("Can't flip shape. Border locked.");
                    return true;
                }
            }
            foreach (Brick filled in Maps[mapId].AllBricks) {
                for (int i = 0; i < shape.Bricks.Length; i++) {
                    if (filled.Ypos == shape.Bricks[i].Ypos
                        && filled.Xpos == shape.Bricks[i].Xpos) {
                        #region comments
                        //Maps[0].CurrentBlock = crutch_shape;
                        //shape = crutch_shape;
                        //Debug.WriteLine("Shape indexes");
                        //for (int j = 0; j < Maps[0].CurrentBlock.Bricks.Length; j++) {
                        //    Debug.WriteLine(Maps[0].CurrentBlock.Bricks[j].Xpos + "   " + Maps[0].CurrentBlock.Bricks[j].Ypos);
                        //}
                        #endregion
                        Debug.WriteLine("Can't flip shape. Brick locked.");
                        return false;
                    }
                }
            }
            Maps[mapId].CurrentBlock = shape;
            return true;
        }


        public Block GetCurrentBlock(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].CurrentBlock;
        }
        public Block GetNextBlock(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].NextBlock;
        }

        public void MoveCurrentBlock(int direction, int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            Block shape = new Block(GetCurrentBlock(mapId));
            shape.Bricks = shape.Bricks.Select(x => new Brick(x.Xpos + direction, x.Ypos)).ToArray();

            if (
                shape.Bricks.Any(x => Maps[mapId].AllBricks.Count(y => y.Xpos == x.Xpos && y.Ypos == x.Ypos) != 0) ||
                shape.Bricks.Any(x => x.Xpos < 0 || x.Xpos > 9 || x.Ypos < 0 || x.Ypos > 19)
                ) { } else {
                Maps[mapId].CurrentBlock = shape;
                shape.Bind.Xpos += direction;
            }

        }

        public bool MoveCurrentBlockDown(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            Block shape = new Block(GetCurrentBlock(mapId));
            for (int i = 0; i < Maps[mapId].CurrentBlock.Bricks.Count(); i++) {
                shape.Bricks[i].Ypos += 1;
            }
            shape.Bind.Ypos += 1;
            return FlipEnable(mapId, shape);
        }

        public bool[,] BricksField(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            Brick[] bricks_arr = Maps[mapId].AllBricks.ToArray();
            bool[,] matrix = new bool[10, 20];
            for (int i = 0; i < bricks_arr.Length; i++) {
                matrix[bricks_arr[i].Xpos, bricks_arr[i].Ypos] = true;
            }
            return matrix;
        }

        public void CheckLineField(int mapId) {
            foreach (Brick br in Maps[mapId].AllBricks) {
                Debug.Write(br.Xpos + " " + br.Ypos + "\n");
            }

            for (var i = 0; i < 20; i++) {
                var query = Maps[mapId].AllBricks.Where(x => (x.Ypos == i) && !x.Tags.Contains("liquid"));
                if (query.Count() == 10) {
                    Score += 100;
                    Maps[mapId].AllBricks.RemoveAll(x => query.Contains(x));
                    Maps[mapId].AllBricks = Maps[mapId].AllBricks.Select(
                        x => x.Ypos <= i ? new Brick(x.Xpos, x.Ypos, new string[] { "liquid" }) : x
                        ).ToList();
                    SetMapLocked(true, mapId);
                }
            }
        }

        private void CheckLineFalling(int mapId) {
            Maps[mapId].AllBricks =
                Maps[mapId].AllBricks.OrderByDescending(x => x.Ypos)
                    .Select(x => {
                        if (x.Tags.Contains("liquid")) {
                            if (Maps[mapId].AllBricks.Any(y => y.Xpos == x.Xpos && y.Ypos == x.Ypos + 1) || x.Ypos == 19) {
                                if (Maps[mapId].AllBricks.Any(y => y.Xpos == x.Xpos && y.Ypos == x.Ypos + 1 && !y.Tags.Contains("liquid")) || x.Ypos == 19) {
                                    x.Tags.Remove("liquid");
                                    return x;
                                }
                            }
                            return new Brick(x.Xpos, x.Ypos + 1, new string[] { "liquid" });
                        } else return x;
                    })
                    .ToList();
            if (Maps[mapId].AllBricks.Count(x => x.Tags.Contains("liquid")) == 0)
                SetMapLocked(false, mapId);
            else SetMapLocked(true, mapId);

        }

        private bool CheckGameEnd() {
            return Maps.Any(x => x.AllBricks.Count(y => y.Ypos <= 1) != 0);
        }

        private void SetMapLocked(bool locked, int mapId) {
            if (Maps[mapId].IsLocked != locked) {
                if (locked) MapLocked?.Invoke(mapId);
                else MapUnlocked?.Invoke(mapId);
                Maps[mapId].IsLocked = locked;
            }
        }
 
        private char[] FuckingLetters = new char[] { 'I', 'O', 'L', 'J', 'S', 'Z', 'T' };
        public void ProccessTurn(int mapId) {
            #region comments
            //throw new NotImplementedException();
            //Maps[0].AllBricks.OrderBy(x => x.Xpos).ThenBy(x => x.Ypos);
            //Debug.WriteLine(Object);
            //Maps[0].AllBricks.Where(x => x.Xpos == 1 && x.Ypos == 1);
            //Block map1block = GetCurrentBlock(0);
            //Block map2block = GetCurrentBlock(1);
            //DebugInitMaps();
            /*for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 10; j++) {
                    Maps[0].AllBricks.Add(new Brick(i, j));
                }
            }*/

            #region DebugMeths
            // DebugCheckMoving('I');    //Проверка движения и поворота shape-ов
            // DebugInitMaps();          //Создать немного заполненную карту
            // DebugWriteMatrix();       //Вывод матрицы. Заполненное поле.
            #endregion
            #endregion
            if (mapId >= Maps.Count() || mapId < 0) throw new ArgumentException("Invalid map Index");
            var targetMap = Maps[mapId];

            CheckLineFalling(mapId);
            CheckLineField(mapId);

            if (CheckGameEnd()) {
                GameOver?.Invoke();
                return;
            }

            if (Maps[mapId].IsLocked) return;
            
            if (MoveCurrentBlockDown(mapId) && Maps[mapId].CurrentBlockPassTurns == 0) {
                //ok .. Crutch
            } else {
                Score += 10;
                Maps[mapId].CurrentBlockPassTurns++;
                if (Maps[mapId].CurrentBlockPassTurns < 1)
                    return;
                Maps[mapId].CurrentBlockPassTurns = 0;
                Maps[mapId].addBlockToMap();
                var rnd = new Random();
                var sym = FuckingLetters.OrderBy(x => rnd.Next()).Last();
                Maps[mapId].CurrentBlock = Maps[mapId].NextBlock;
                Maps[mapId].NextBlock = CreateBlock(sym);
            }

        }
    }
}
