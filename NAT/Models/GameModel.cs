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
        public int Score {get;set; }
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

        public GameModel() {
            Score = 0;
            Maps = new Map[] { new Map(), new Map() };
            var rnd = new Random();
            Maps[0].CurrentBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
            //Maps[0].CurrentBlock = CreateBlock('I');
            Maps[1].CurrentBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
            Maps[0].NextBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
            Maps[1].NextBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
        }

        public Brick[] BrickMap(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].AllBricks.ToArray();
        }
        
        //Нумерация слева направо, сверху вниз
        public Block CreateBlock(char BlockIndex) {
            //I Z S J L T O Смотри картинку 13.png в дискорде
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
                                          new Brick(4, 1),2);
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

        //Вращение происходит ТОЛЬКО вправо
        public void FlipCurrentBlock(int mapId) {
            //I Z S J L T O Смотри картинку 13.png в дискорде
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
           // FlipFigure(Maps[mapId].CurrentBlock,mapId);
            char BlockIndex = Maps[mapId].CurrentBlock.BlockIndex;
            switch (BlockIndex) {
                case 'I':
                    FlipI(mapId);
                    break;
                case 'Z':
                    FlipZ(mapId);
                    break;
                case 'S':
                    FlipS(mapId);
                    break;
                case 'J':
                    FlipJ(mapId);
                    break;
                case 'L':
                    FlipL(mapId);
                    break;
                case 'T':
                    FlipT(mapId);
                    break;
                case 'O':
                    return;
            }
        }

        public void FlipI(int mapId) {
            Block shape = new Block(GetCurrentBlock(mapId));
            if (shape.Bricks[0].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos -= 2;
                FlipEnable(mapId, shape);
                return;
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos -= 2;
                FlipEnable(mapId, shape);
                return;
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos += 2;
                FlipEnable(mapId, shape);
                return;
            } else if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos += 2;
                FlipEnable(mapId, shape);
                return;
            }
        }
        public void FlipZ(int mapId) {
            Block shape = new Block(GetCurrentBlock(mapId));
            if (shape.Bricks[3].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Ypos += 2;
                shape.Bricks[1].Xpos += 1; shape.Bricks[1].Ypos += 1;
                shape.Bricks[3].Xpos += 1; shape.Bricks[3].Ypos -= 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[3].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 2;
                shape.Bricks[1].Xpos += 1; shape.Bricks[1].Ypos -= 1;
                shape.Bricks[3].Xpos -= 1; shape.Bricks[3].Ypos -= 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[3].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Ypos -= 2;
                shape.Bricks[1].Xpos -= 1; shape.Bricks[1].Ypos -= 1;
                shape.Bricks[3].Xpos -= 1; shape.Bricks[3].Ypos += 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[3].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 2;
                shape.Bricks[1].Xpos -= 1; shape.Bricks[1].Ypos += 1;
                shape.Bricks[3].Xpos += 1; shape.Bricks[3].Ypos += 1;
                FlipEnable(mapId, shape);
            }
        }
        public void FlipS(int mapId) {
            Block shape = new Block(GetCurrentBlock(mapId));
            if (shape.Bricks[0].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[1].Xpos += 1; shape.Bricks[1].Ypos += 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos -= 0;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[1].Xpos += 1; shape.Bricks[1].Ypos -= 1;
                shape.Bricks[3].Xpos -= 0; shape.Bricks[3].Ypos -= 2;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[1].Xpos -= 1; shape.Bricks[1].Ypos -= 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos += 0;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[1].Xpos -= 1; shape.Bricks[1].Ypos += 1;
                shape.Bricks[3].Xpos += 0; shape.Bricks[3].Ypos += 2;
                FlipEnable(mapId, shape);
            }
        }
        public void FlipJ(int mapId) {
            Block shape = new Block(GetCurrentBlock(mapId));
            if (shape.Bricks[0].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos -= 0;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos += 0; shape.Bricks[3].Ypos -= 2;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos += 0;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos -= 0; shape.Bricks[3].Ypos += 2;
                FlipEnable(mapId, shape);
            }
        }
        public void FlipL(int mapId) {
            Debug.WriteLine("Here");
            Block shape = new Block(GetCurrentBlock(mapId));
            if (shape.Bricks[1].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 0; shape.Bricks[0].Ypos -= 2;
                shape.Bricks[1].Xpos += 1; shape.Bricks[1].Ypos += 1;
                shape.Bricks[3].Xpos -= 1; shape.Bricks[3].Ypos -= 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[1].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 2; shape.Bricks[0].Ypos += 0;
                shape.Bricks[1].Xpos -= 1; shape.Bricks[1].Ypos += 1;
                shape.Bricks[3].Xpos += 1; shape.Bricks[3].Ypos -= 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[1].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 0; shape.Bricks[0].Ypos += 2;
                shape.Bricks[1].Xpos -= 1; shape.Bricks[1].Ypos -= 1;
                shape.Bricks[3].Xpos += 1; shape.Bricks[3].Ypos += 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[1].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 2; shape.Bricks[0].Ypos -= 0;
                shape.Bricks[1].Xpos += 1; shape.Bricks[1].Ypos -= 1;
                shape.Bricks[3].Xpos -= 1; shape.Bricks[3].Ypos += 1;
                FlipEnable(mapId, shape);
            }
        }
        public void FlipT(int mapId) {
            Block shape = new Block(GetCurrentBlock(mapId));
            if (shape.Bricks[0].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[1].Xpos += 1; shape.Bricks[1].Ypos += 1;
                shape.Bricks[3].Xpos += 1; shape.Bricks[3].Ypos -= 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[1].Xpos += 1; shape.Bricks[1].Ypos -= 1;
                shape.Bricks[3].Xpos -= 1; shape.Bricks[3].Ypos -= 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[1].Xpos -= 1; shape.Bricks[1].Ypos -= 1;
                shape.Bricks[3].Xpos -= 1; shape.Bricks[3].Ypos += 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[1].Xpos -= 1; shape.Bricks[1].Ypos += 1;
                shape.Bricks[3].Xpos += 1; shape.Bricks[3].Ypos += 1;
                FlipEnable(mapId, shape);
            }
        }

        public bool FlipEnable(int mapId, Block shape) {
            if (Maps[mapId].CurrentBlock.Bricks.Any(x => x.Ypos >= 19))
                return false;
            for (int i = 0; i < shape.Bricks.Length; i++) {
                if (shape.Bricks[i].Ypos < 0
                    || shape.Bricks[i].Ypos > 19
                    || shape.Bricks[i].Xpos < 0
                    || shape.Bricks[i].Xpos > 9) {
                    #region comments
                    //Maps[0].CurrentBlock = crutch_shape;
                    //shape = crutch_shape;
                    //Debug.WriteLine("Shape indexes");
                    //for (int j = 0; j < Maps[0].CurrentBlock.Bricks.Length; j++) {
                    //    Debug.WriteLine(Maps[0].CurrentBlock.Bricks[j].Xpos + "   " + Maps[0].CurrentBlock.Bricks[j].Ypos);
                    //}
                    #endregion
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
       

        public void FlipFigure(Block shape,int mapId) {
            var t_Shape = new Block(shape);

            var minX = t_Shape.Bricks.Min(x => x.Xpos);
            var maxX = t_Shape.Bricks.Max(x => x.Xpos);
            var minY = t_Shape.Bricks.Min(x => x.Ypos);
            var maxY = t_Shape.Bricks.Max(y => y.Ypos);

            var Matrix = new Brick[ 
                maxX - minX + 1, 
                maxY - minY + 1
                ];
            foreach (var brick in t_Shape.Bricks)
                Matrix[brick.Xpos - minX, brick.Ypos - minY] = brick;

            var T_Matrix = new Brick[Matrix.GetLength(1), Matrix.GetLength(0)];

            for (var x = 0; x < Matrix.GetLength(0); x++)
                for (var y = 0; y < Matrix.GetLength(1); y++)
                    T_Matrix[y, x] = Matrix[ Matrix.GetLength(0) - x - 1, y];

            var bricks = new List<Brick>();
            for (var x = 0; x < T_Matrix.GetLength(0); x++)
                for (var y = 0; y < T_Matrix.GetLength(1); y++) { 
                    if (T_Matrix[x, y] == null) continue;
                    bricks.Add(new Brick(x + minX, y + minY));
                }

            var bindBlock = t_Shape.Bricks[t_Shape.BindBlockIndex];
            bricks = bricks.Select(x => new Brick(x.Xpos + (bindBlock.Xpos - minX) , x.Ypos + (bindBlock.Ypos - minY)  )).ToList();
            t_Shape.Bricks = bricks.ToArray();

            if(t_Shape.Bricks.Any(x => 
                x.Xpos > 9  ||
                x.Xpos < 0 ||
                x.Ypos > 19 ||
                x.Ypos < 0 ||
                Maps[mapId].AllBricks.Any(y => y.Xpos == x.Xpos && y.Ypos == x.Ypos)
                )) {
            }else {
                shape.Bricks = t_Shape.Bricks;
            }

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
            for (int i = 0; i < Maps[mapId].CurrentBlock.Bricks.Count(); i++) {
                shape.Bricks[i].Xpos += direction;
            }
            shape.Bind.Xpos += direction;
            FlipEnable(mapId, shape);
        }

        //Block shape = new Block(GetCurrentBlock(mapId));
        //FlipEnable(mapId, shape);

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

        //Плохой, плохой, очень плохой метод (но рабочий) - НЕТ!
        public void CheckLineField(int mapId) {
            foreach (Brick br in Maps[mapId].AllBricks) {
                Debug.Write(br.Xpos + " " + br.Ypos + "\n");
            }

            for( var i = 0; i < 20; i++) {
                var query = Maps[mapId].AllBricks.Where(x => (x.Ypos == i));
                if(query.Count() == 10) {
                    Score += 100;
                    Maps[mapId].AllBricks.RemoveAll(x => query.Contains(x));
                }
            }


            //TODO: add score
        }

        private void CheckLineFalling(int mapId) {
            for (var i = 19; i >= 0; i--) {
                if (Maps[mapId].AllBricks.Count(x => x.Ypos == i) == 0) {
                    Maps[mapId].AllBricks = Maps[mapId].AllBricks.Select(x => x.Ypos <= i ? new Brick(x.Xpos, x.Ypos + 1) : x).ToList();
                }
            }
        }

        private bool CheckGameEnd() {
            return Maps.Any(x => x.AllBricks.Count(y => y.Ypos <= 1) != 0);
        }

        //Когда CurrentBlock коснулся блока или чего-то еще, он должен еще один шаг отстоять, должен быть блоком.
        //Еще один ход он может двинуться
        //Для всех карт опускает блок, если блок упал, то выпускаем бычка, 
        //и просчитываем линии.
        //Если линия заполнена, то убираем ее, добавляем очки, сдвигаем всех вниз
        //Генерировать новый next
        //Поле 10 на 20   
        private char[] FuckingLetters = new char[] { 'I','O','L','J','S','Z','T' };
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
            if(mapId >= Maps.Count() || mapId < 0) throw new ArgumentException("Invalid map Index");
            var targetMap = Maps[mapId];

            CheckLineFalling(mapId);
            Debug.WriteLine(Score);
            if (CheckGameEnd()){ 
                GameOver?.Invoke();
                return;
            }

            if (MoveCurrentBlockDown(mapId) || Maps[mapId].CurrentBlockPassTurns != 0) {
                //ok ..
            }else {
                Score += 10;
                Maps[mapId].CurrentBlockPassTurns++;
                if (Maps[mapId].CurrentBlockPassTurns < 1) return;
                Maps[mapId].CurrentBlockPassTurns = 0;
                Maps[mapId].addBlockToMap();
                var rnd = new Random();
                var sym = FuckingLetters.OrderBy(x => rnd.Next()).Last();
                Maps[mapId].CurrentBlock = Maps[mapId].NextBlock;
                Maps[mapId].NextBlock = CreateBlock(sym);
                CheckLineField(mapId);
            }
            
        }

        
        //Debug methods
        public void DebugCheckMoving(char Shapename) {
            DebugInitMaps();
            Debug.WriteLine("1");
            DebugWriteMatrix();
            Maps[0].CurrentBlock = CreateBlock(Shapename);
            Maps[0].addBlockToMap();
            for (int i = 0; i < 4; i++)
                Debug.WriteLine("Block to GameModel index " + i + ": " + Maps[0].CurrentBlock.Bricks[i].Xpos + " " + Maps[0].CurrentBlock.Bricks[i].Ypos);

            Debug.WriteLine("2");
            DebugWriteMatrix();
            for (int i = 0; i < 4; i++)
                Debug.WriteLine("Block to GameModel index " + i + ": " + Maps[0].CurrentBlock.Bricks[i].Xpos + " " + Maps[0].CurrentBlock.Bricks[i].Ypos);

            MoveCurrentBlockDown(0);
            MoveCurrentBlock(1, 0);
            for (int i = 0; i < 4; i++)
                Debug.WriteLine("Block to GameModel index " + i + ": " + Maps[0].CurrentBlock.Bricks[i].Xpos + " " + Maps[0].CurrentBlock.Bricks[i].Ypos);

            Debug.WriteLine("3");
            DebugWriteMatrix();
            MoveCurrentBlockDown(0);
            Debug.WriteLine("4");
            DebugWriteMatrix();
            for (int i = 0; i < 4; i++)
                Debug.WriteLine("Block to GameModel index " + i + ": " + Maps[0].CurrentBlock.Bricks[i].Xpos + " " + Maps[0].CurrentBlock.Bricks[i].Ypos);
            for (int i = 0; i < 4; i++)
                Debug.WriteLine("Block to GameModel Bind index " + i + ": " + Maps[0].CurrentBlock.Bind.Xpos + " " + Maps[0].CurrentBlock.Bind.Ypos);
            FlipCurrentBlock(0);
            Debug.WriteLine("5");
            DebugWriteMatrix();
            for (int i = 0; i < 4; i++)
                Debug.WriteLine("Block to GameModel index " + i + ": " + Maps[0].CurrentBlock.Bricks[i].Xpos + " " + Maps[0].CurrentBlock.Bricks[i].Ypos);
            for (int i = 0; i < 4; i++)
                Debug.WriteLine("Block to GameModel Bind index " + i + ": " + Maps[0].CurrentBlock.Bind.Xpos + " " + Maps[0].CurrentBlock.Bind.Ypos);
            FlipCurrentBlock(0);
            Debug.WriteLine("6");
            DebugWriteMatrix();
            Debug.WriteLine("7");
            FlipCurrentBlock(0);
            DebugWriteMatrix();
            Debug.WriteLine("8");
            FlipCurrentBlock(0);
            DebugWriteMatrix();
        }
        public void DebugInitMaps() {
            for (int i = 0; i < 9; i++) {
                Maps[0].AllBricks.Add(new Brick(19, i));
            }
            for (int i = 1; i < 8; i++) {
                Maps[0].AllBricks.Add(new Brick(18, i));
            }
            for (int i = 3; i < 10; i++) {
                Maps[0].AllBricks.Add(new Brick(17, i));
            }
            for (int i = 0; i < 5; i++) {
                Maps[0].AllBricks.Add(new Brick(16, i));
            }
            Maps[0].AllBricks.ToArray();
        }
        public void DebugWriteMatrix() {
            Debug.WriteLine(" ");
            Debug.WriteLine(" ");
            bool[,] r = BricksField(0);
            for (int i = 0; i < 20; i++) {
                Debug.WriteLine(" ");
                for (int j = 0; j < 10; j++) {
                    Debug.Write(r[i,j] + "   ");
                    if (r[i,j]) Debug.Write(" ");
                }
                Debug.WriteLine(" ");
            }
            Debug.WriteLine(" ");
            Debug.WriteLine(" ");
        }
    }
}
