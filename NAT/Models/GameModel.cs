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
        }

        public Brick[] BrickMap(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].AllBricks.ToArray();
        }

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

        public bool FlipEnable(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            if (true) return true;
            return false;
        }

        public void FlipI(int mapId) {
            Block shape = GetCurrentBlock(mapId);
            if (shape.Bricks[0].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1;  shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos -= 1;  shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos -= 2;  shape.Bricks[3].Ypos -= 2;
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos += 2;
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos += 2;
            } else if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos -= 2;
            }
        }
        public void FlipZ(int mapId) {
            Block shape = GetCurrentBlock(mapId);
            if (shape.Bricks[0].Ypos < shape.Bind.Ypos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos -= 2;
            } else if (shape.Bricks[0].Xpos > shape.Bind.Xpos) {
                shape.Bricks[0].Xpos += 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos -= 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos -= 2; shape.Bricks[3].Ypos += 2;
            } else if (shape.Bricks[0].Ypos > shape.Bind.Ypos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos -= 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos += 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos += 2;
            } else if (shape.Bricks[0].Xpos < shape.Bind.Xpos) {
                shape.Bricks[0].Xpos -= 1; shape.Bricks[0].Ypos += 1;
                shape.Bricks[2].Xpos += 1; shape.Bricks[2].Ypos -= 1;
                shape.Bricks[3].Xpos += 2; shape.Bricks[3].Ypos -= 2;
            }
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

                    break;
                case 'S':

                    break;
                case 'J':

                    break;
                case 'L':

                    break;
                case 'T':

                    break;
                case 'O':
                    return;
            }
        }

        public Block GetCurrentBlock(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].CurrentBlock;
        }

        public Block GetNextBlock(int mapId) {
            
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            return Maps[mapId].CurrentBlock;
        }

        public void MoveCurrentBlock(int direction, int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            //Maps[mapId].CurrentBlock.Bricks =
            //    Maps[mapId].CurrentBlock.Bricks.Select(x => new Brick(x.Xpos + direction, x.Ypos)).ToArray();
            for(int i = 0; i < Maps[mapId].CurrentBlock.Bricks.Length; i++) 
                Maps[mapId].CurrentBlock.Bricks[i].Xpos += direction;
        }

        public void MoveCurrentBlockDown(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            for (int i = 0; i < Maps[mapId].CurrentBlock.Bricks.Count(); i++) {
                Maps[mapId].CurrentBlock.Bricks[i].Ypos += 1;
            }
        }

        public bool[][] BricksField(int mapId) {
            if (mapId >= Maps.Count()) throw new ArgumentException("Invalid Map Index");
            Brick[] bricks_arr = Maps[mapId].AllBricks.ToArray();
            bool[][] matrix = new bool[20][];
            for (int i = 0; i < matrix.Length; i++) {
                matrix[i] = new bool[10];
            }

            for (int i = 0; i < bricks_arr.Length; i++) {
                matrix[bricks_arr[i].Xpos][bricks_arr[i].Ypos] = true;
            }
            return matrix;
        }

        //Когда CurrentBlock коснулся блока или чего-то еще, он должен еще один шаг отстоять, должен быть блоком.
        //Еще один ход он может двинуться

        //Для всех карт опускает блок, если блок упал, то выпускаем бычка, 
        //и просчитываем линии.
        //Если линия заполнена, то убираем ее, добавляем очки, сдвигаем всех вниз
        //Генерировать новый next
        //Поле 10 на 20

        

        public void ProccessTurn() {
            //throw new NotImplementedException();
            //Maps[0].AllBricks.OrderBy(x => x.Xpos).ThenBy(x => x.Ypos);
            //Debug.WriteLine(Object);
            //Maps[0].AllBricks.Where(x => x.Xpos == 1 && x.Ypos == 1);
            Block map1block = GetCurrentBlock(0);
            Block map2block = GetCurrentBlock(1);


            DebugInitMaps();    //Создать немного заполненную карту
            DebugWriteMatrix(); //Вывод матрицы. Заполненное поле.
        }



        //Debug methods
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
            bool[][] r = BricksField(0);
            for (int i = 0; i < 20; i++) {
                Debug.WriteLine(" ");
                for (int j = 0; j < 10; j++) {
                    Debug.Write(r[i][j] + "  ");
                    if (r[i][j]) Debug.Write(" ");
                }
            }
            Debug.WriteLine(" ");
            Debug.WriteLine(" ");
        }
    }
}
