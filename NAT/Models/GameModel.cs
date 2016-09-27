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
                return 0;
            }
        }

        public GameModel() {
            Maps = new Map[] { new Map(), new Map() };
            var rnd = new Random();
            Maps[0].CurrentBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
            Maps[1].CurrentBlock = CreateBlock(FuckingLetters.OrderBy(x => rnd.Next()).Last());
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
                                        { new Brick(0, 5),
                                          new Brick(1, 5),
                                          new Brick(2, 5),
                                          new Brick(3, 5)}, 
                                          BlockIndex, 
                                          new Brick(1, 5));
                    return I;
                #region other_switch
                case 'Z':
                    Block Z = new Block(new Brick[]
                                        { new Brick(0, 4),
                                          new Brick(0, 5),
                                          new Brick(1, 5),
                                          new Brick(1, 6)},
                                          BlockIndex,
                                          new Brick(1, 5));
                    return Z;
                case 'S':
                    Block S = new Block(new Brick[]
                                        { new Brick(1, 4),
                                          new Brick(0, 5),
                                          new Brick(1, 5),
                                          new Brick(0, 6)},
                                          BlockIndex,
                                          new Brick(1, 5));
                    return S;
                case 'J':
                    Block J = new Block(new Brick[]
                                        { new Brick(0, 4),
                                          new Brick(0, 5),
                                          new Brick(0, 6),
                                          new Brick(1, 6)},
                                          BlockIndex,
                                          new Brick(0, 5));
                    return J;
                case 'L':
                    Block L = new Block(new Brick[]
                                        { new Brick(1, 4),
                                          new Brick(1, 5),
                                          new Brick(1, 6),
                                          new Brick(0, 6)},
                                          BlockIndex,
                                          new Brick(1, 5));
                    return L;
                case 'T':
                    Block T = new Block(new Brick[]
                                        { new Brick(1, 4),
                                          new Brick(0, 5),
                                          new Brick(1, 5),
                                          new Brick(1, 6)},
                                          BlockIndex,
                                          new Brick(1, 5));
                    return T;
                case 'O':
                    Block O = new Block(new Brick[]
                                        { new Brick(0, 4),
                                          new Brick(0, 5),
                                          new Brick(1, 4),
                                          new Brick(1, 5)},
                                          BlockIndex,
                                          new Brick(0, 4));
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
            char BlockIndex = GetCurrentBlock(mapId).BlockIndex;
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

        public bool FlipEnable(int mapId, Block shape) {
            for (int i = 0; i < shape.Bricks.Length; i++) {
                if (shape.Bricks[i].Xpos < 0
                    || shape.Bricks[i].Xpos > 19
                    || shape.Bricks[i].Ypos < 0
                    || shape.Bricks[i].Ypos > 9) {
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
                    if (filled.Xpos == shape.Bricks[i].Xpos
                        && filled.Ypos == shape.Bricks[i].Ypos) {
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
        public void FlipI(int mapId) {
            Block shape = new Block(GetCurrentBlock(mapId));
            if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1;  shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos -= 1;  shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos -= 2;  shape.Bricks[3].Ypos -= 2;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos += 2;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos += 2;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos -= 2;
                FlipEnable(mapId, shape);
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
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos += 0; shape.Bricks[3].Ypos -= 2;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos -= 0;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos -= 0; shape.Bricks[3].Ypos += 2;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos += 0;
                FlipEnable(mapId, shape);
            }
        }
        public void FlipL(int mapId) {
            Block shape = new Block(GetCurrentBlock(mapId));
            if (shape.Bricks[0].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos += 2; shape.Bricks[2].Ypos += 0;
                shape.Bricks[3].Xpos += 1; shape.Bricks[3].Ypos -= 1;
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos += 0; shape.Bricks[2].Ypos -= 2;
                shape.Bricks[3].Xpos -= 1; shape.Bricks[3].Ypos -= 1; 
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos -= 2; shape.Bricks[2].Ypos -= 0;
                shape.Bricks[3].Xpos -= 1; shape.Bricks[3].Ypos += 1; 
                FlipEnable(mapId, shape);
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos -= 0; shape.Bricks[2].Ypos += 2;
                shape.Bricks[3].Xpos += 1; shape.Bricks[3].Ypos += 1; 
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
                shape.Bricks[i].Ypos += direction;
            }
            shape.Bind.Ypos += direction;
            FlipEnable(mapId, shape);
        }

        //Block shape = new Block(GetCurrentBlock(mapId));
        //FlipEnable(mapId, shape);

        public bool MoveCurrentBlockDown(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            Block shape = new Block(GetCurrentBlock(mapId));
            for (int i = 0; i < Maps[mapId].CurrentBlock.Bricks.Count(); i++) {
                shape.Bricks[i].Xpos += 1;
            }
            shape.Bind.Xpos += 1;
            return FlipEnable(mapId, shape);
        }

        public bool[,] BricksField(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            Brick[] bricks_arr = Maps[mapId].AllBricks.ToArray();
            bool[,] matrix = new bool[20, 10];
            for (int i = 0; i < bricks_arr.Length; i++) {
                matrix[bricks_arr[i].Xpos, bricks_arr[i].Ypos] = true;
            }
            return matrix;
        }

        //Плохой, плохой, очень плохой метод (но рабочий)
        public void CheckLineField(int mapId) {
            bool[,] field = BricksField(mapId);
            Maps[mapId].AllBricks = Maps[mapId].AllBricks.OrderBy(x => x.Xpos).ThenBy(x => x.Ypos).ToList();
            int counter;
            foreach (Brick br in Maps[mapId].AllBricks) {
                Debug.Write(br.Xpos + " " + br.Ypos + "\n");
            }

            for (int i = 0; i < 20; i++) {
                counter = 0;
                if (Maps[mapId].AllBricks.Count(x => (x.Xpos == i)) == 10) {
                    foreach (Brick br in Maps[mapId].AllBricks) {
                        if (br.Xpos == i) break;
                        counter++;
                    }
                    Maps[mapId].AllBricks.RemoveRange(counter, 10);
                    foreach (Brick br in Maps[mapId].AllBricks) {
                        if (br.Xpos >= i) break;
                        br.Xpos += 1;
                    }
                }
            }
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

           
            if (MoveCurrentBlockDown(mapId)) {
                //ok ..
            }else {
                Maps[mapId].addBlockToMap();
                var rnd = new Random();
                var sym = FuckingLetters.OrderBy(x => rnd.Next()).Last();
                Maps[mapId].CurrentBlock = CreateBlock(sym);
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
