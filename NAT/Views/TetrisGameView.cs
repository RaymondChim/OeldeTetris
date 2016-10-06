using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using NAT.Models;
using NAT.Views;
using NAT.Services;
//using NAT.Models.Race;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System;
namespace NAT.Views
{
    class View : IView
    {
        //Глобальный контент
        private SoundEffect booting;
        private Texture2D background;
        private Texture2D fieldG;
        private Texture2D GLfieldG;
        private Texture2D sideFieldG;
        private Texture2D GLsideFieldG;
        private Texture2D tileY;
        private Texture2D tileG;
        private Texture2D textBorderY;
        private Texture2D GLtextBorderY;
        private Texture2D sideFieldY;
        private Texture2D GLsideFieldY;
        private Texture2D fieldY;
        private Texture2D GLfieldY;
        private Texture2D GLtileG;
        private Texture2D GLtileY;
        private Texture2D screenY;
        private Texture2D screenG;
        private Texture2D gameoverMsg;
        private SpriteFont text;
        private Color yel = new Color(0xff, 0xcb, 0x28);
        private Color grn = new Color(0x24,0xff,0x51);
        private readonly GameMain _GameMain;
        private Scores highscoreTable;
        
        //Разрешение
        private int resX = 1920;
        private int resY = 1080;
       
        //Оффсеты
        private int resOffset = 490;
        private int screenOffset = 60;
        private int backTopOffset = -15;
        private int backScreenOffset = 20;
        private int topOffset = 118;
        
        //Блок "Зачем это нужно?"
        private int sizeModifier = 1;
        private int modeTest = 0;
        private bool tehEnd = false;
        
        //Эмпирическией размер кирпичей
        private int brickSizeX = 37;
        private int brickSizeY = 39;
        
        //Режимы
        int backMode, frontMode;

        //Цветник
        Texture2D frontField = null;
        Texture2D backField = null;
        Texture2D GLfrontField = null;
        Texture2D GLbackField = null;
        Texture2D frontBrick = null;
        Texture2D backBrick = null;
        Texture2D GLfrontBrick = null;
        Texture2D GLbackBrick = null; //?
        Texture2D sideFieldOn = null;
        Texture2D sideFieldOff = null;
        Texture2D GLsideFieldOn = null;
        Texture2D textFieldOn = null;
        Texture2D textFieldOff = null;
        Texture2D GLtextFieldOn = null;
        Texture2D Screen = null;
        Color backColor, frontColor;


        public View(GameMain game)
        {
            _GameMain = game;
        }

        public void LoadContent()
        {
            background = _GameMain.Content.Load<Texture2D>("tetris_screen_test");
            textBorderY = _GameMain.Content.Load<Texture2D>("textBorderY"); 
            GLtextBorderY = _GameMain.Content.Load<Texture2D>("GLtextBorderY"); 
            sideFieldY = _GameMain.Content.Load<Texture2D>("sideFieldY"); 
            GLsideFieldY = _GameMain.Content.Load<Texture2D>("GLsideFieldY"); 
            fieldY = _GameMain.Content.Load<Texture2D>("fieldY"); 
            GLfieldY = _GameMain.Content.Load<Texture2D>("GLfieldY");
            sideFieldG = _GameMain.Content.Load<Texture2D>("sideFieldG");
            GLsideFieldG = _GameMain.Content.Load<Texture2D>("GLsideFieldG");
            fieldG = _GameMain.Content.Load<Texture2D>("fieldG");
            GLfieldG = _GameMain.Content.Load<Texture2D>("GLfieldG");
            text = _GameMain.Content.Load<SpriteFont>("text");
            tileY = _GameMain.Content.Load<Texture2D>("tileA");
            tileG = _GameMain.Content.Load<Texture2D>("tileG");
            GLtileY = _GameMain.Content.Load<Texture2D>("GLtileY");
            GLtileG = _GameMain.Content.Load<Texture2D>("GLtileG");
            screenG = _GameMain.Content.Load<Texture2D>("screenG");
            screenY = _GameMain.Content.Load<Texture2D>("screenY");
            gameoverMsg = _GameMain.Content.Load<Texture2D>("gmvr");
            booting = _GameMain.Content.Load<SoundEffect>("start");
            _GameMain.graphics.PreferredBackBufferWidth = resX;
            _GameMain.graphics.PreferredBackBufferHeight = resY;
            _GameMain.Window.Position = new Point(0, 0);
            _GameMain.graphics.ApplyChanges();
            sizeModifier = 4000 / resX;

        }
        //Под снос:
        public void TestDisplay()
        {
            // 77.5 x 77.5 - cube or 37.2 x 37.2 

        }

        public void Init(Scores scores) {
            highscoreTable = scores;
            //booting.Play(0.35f,0f,0f);
        }

        public void Display(IModel _model)
        {

            // Пока var, если собъётся - напиши через блоки и брики
            // 0 - жёлтый, 1 - зелёный

            _GameMain.GraphicsDevice.Clear(Color.White);
            _GameMain.spriteBatch.Begin();
            _GameMain.spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), /*new Rectangle(69,79,593,558), */Color.White);

            //Под снос:
            #region oldColoring
            /*
            if (frontMode == 1)
            {
                backColor = yel;
                frontColor = grn;
                frontBrick = tileG;
                Screen = screenG;
                GLfrontBrick = GLtileG;
                backBrick = tileY; 
                frontField = fieldG;
                GLfrontField = GLfieldY;
                backField = fieldY; 
                GLbackField = GLfieldY;// plc
                sideFieldOn = sideFieldG;
                GLsideFieldOn = GLsideFieldG;
                sideFieldOff = sideFieldY; //plc
                textFieldOn = textBorderY;
                textFieldOff = textBorderY; // plc
                GLtextFieldOn = GLtextBorderY;                
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(1379, 174, 38, 48), Color.White); // layer marker
                backMode = 0;
            }
            else
            {
                backColor = grn;
                frontColor = yel;
                frontBrick = tileY;
                Screen = screenY;
                GLfrontBrick = GLtileY;
                backBrick = tileG; 
                frontField = fieldY;
                GLfrontField = GLfieldY;
                backField = fieldG; 
                GLbackField = GLfieldY;
                sideFieldOn = sideFieldY;
                GLsideFieldOn = GLsideFieldY;
                sideFieldOff = sideFieldG; 
                textFieldOn = textBorderY; 
                textFieldOff = textBorderY; // plc
                GLtextFieldOn = GLtextBorderY;
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(1379, 275, 38, 48), Color.White); // layer marker
                backMode = 1;
            }*/
            #endregion oldColoring 


            if (_model is ITetrisGameModel)
            {
                tetrisDisplay(_model as ITetrisGameModel);
            }
            if (_model is IRaceGameModel) {
                raceDisplay(_model as IRaceGameModel);
            }

           

            if (tehEnd) {
                _GameMain.spriteBatch.Draw(fieldY, new Rectangle(618, 426, 201, 100), Color.White);
            }
            _GameMain.spriteBatch.End();

        }


        public void DisplayScores(Scores scores, ITetrisGameModel _model, int startX, int startY, Color color)
        {
            var scoreArray = scores.Heaver.Union(new ScoreHeaver[] { new ScoreHeaver("XXX", _model.CurrentScore) }).OrderByDescending(x => x.Score).Take(10).ToList();
            for (int i = 0; i < scoreArray.Count(); i++)
            {
                _GameMain.spriteBatch.DrawString(text, scoreArray[i].Name + "    " + scoreArray[i].Score, new Vector2(startX, startY + 17 * i), color);
            }
        }

        public Keys[] UpdateuserInput()
        {
            var KeyArray = Keyboard.GetState().GetPressedKeys();
            return KeyArray;
        }

        private void determinateColor()
        {
            if (frontMode == 1)
            {
                backColor = yel;
                frontColor = grn;
                frontBrick = tileG;
                Screen = screenG;
                GLfrontBrick = GLtileG;
                backBrick = tileY;
                frontField = fieldG;
                GLfrontField = GLfieldY;
                backField = fieldY;
                GLbackField = GLfieldY;// plc
                sideFieldOn = sideFieldG;
                GLsideFieldOn = GLsideFieldG;
                sideFieldOff = sideFieldY; //plc
                textFieldOn = textBorderY;
                textFieldOff = textBorderY; // plc
                GLtextFieldOn = GLtextBorderY;
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(1379, 174, 38, 48), Color.White); // layer marker
                backMode = 0;
            }
            else
            {
                backColor = grn;
                frontColor = yel;
                frontBrick = tileY;
                Screen = screenY;
                GLfrontBrick = GLtileY;
                backBrick = tileG;
                frontField = fieldY;
                GLfrontField = GLfieldY;
                backField = fieldG;
                GLbackField = GLfieldY;
                sideFieldOn = sideFieldY;
                GLsideFieldOn = GLsideFieldY;
                sideFieldOff = sideFieldG;
                textFieldOn = textBorderY;
                textFieldOff = textBorderY; // plc
                GLtextFieldOn = GLtextBorderY;
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(1379, 275, 38, 48), Color.White); // layer marker
                backMode = 1;
            }
        }

        private void drawBlock(Block curBlock, Texture2D tile, int offsetSide, int offsetTop, float opacity) {
            int x, y, len;
            len = curBlock.Bricks.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                x = curBlock.Bricks[i].Xpos;
                y = curBlock.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(tile, new Rectangle(offsetSide+resOffset + screenOffset + brickSizeX * x, offsetTop + topOffset + brickSizeY * y, brickSizeX, brickSizeY), Color.White*opacity);
            }
        }

        private void drawCar(Models.Race.Car car, Texture2D tile, int offsetSide, int offsetTop, float opacity) {
            int x, y, len;
            len = car.Bricks.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                x = car.Bricks[i].Xpos;
                y = car.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(tile, new Rectangle(offsetSide + resOffset + screenOffset + brickSizeX * x, offsetTop + topOffset + brickSizeY * y, brickSizeX, brickSizeY), Color.White * opacity);
            }
        }

        private void drawBrickMap(Brick[] map, Texture2D tile, int offsetSide, int offsetTop, float opacity) {
            int x, y, len;
            len = map.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                x = map[i].Xpos;
                y = map[i].Ypos;
                _GameMain.spriteBatch.Draw(tile, new Rectangle(offsetSide + resOffset + screenOffset + brickSizeX * x, offsetTop + topOffset + brickSizeY * y, brickSizeX, brickSizeY), Color.White * opacity);
            }
        }

        private void tetrisDisplay(ITetrisGameModel _model) {
            //Бэкмод объявлен наверху в глобальном скоупе
            frontMode = _model.CurrentMapId;
            //Бэкмод меняется здесь
            determinateColor();
            //Экран тут
            _GameMain.spriteBatch.Draw(Screen, new Rectangle(519, 87, 680, 855), Color.White);
            int score = _model.CurrentScore;
            var mapFront = _model.BrickMap(frontMode);
            var mapBack = _model.BrickMap(backMode);
            //Блоки тут
            Block currentBlockFront = _model.GetCurrentBlock(frontMode);
            Block currentBlockBack = _model.GetCurrentBlock(backMode);

            //Вывод заднего слоя блоков
            drawBrickMap(mapBack, backBrick, backScreenOffset, backTopOffset, 0.45f);
            drawBlock(currentBlockBack, backBrick, backScreenOffset, backTopOffset, 0.45f);
            //Задний стакан
            _GameMain.spriteBatch.Draw(backField, new Rectangle(backScreenOffset + resOffset + screenOffset - 7, backTopOffset + topOffset - 6, 772 / 2, 1593 / 2), Color.White * 0.45f);
            //Вывод переднего слоя блоков
            drawBlock(currentBlockFront, frontBrick, 0, 0, 1f);
            drawBlock(currentBlockFront, GLfrontBrick, 0, 0, 0.55f);
            drawBrickMap(mapFront, frontBrick, 0, 0, 1f);
            drawBrickMap(mapFront, GLfrontBrick, 0, 0, 0.55f);
            //Передний стакан
            _GameMain.spriteBatch.Draw(frontField, new Rectangle(resOffset + screenOffset - 7, topOffset - 6, 772 / 2, 1593 / 2), Color.White);
            _GameMain.spriteBatch.Draw(GLfrontField, new Rectangle(resOffset + screenOffset - 6, topOffset - 6, 772 / 2, 1593 / 2), Color.White);

            //Некстблоки тут
            Block nextBlockFront = _model.GetNextBlock(frontMode);
            Block nextBlockBack = _model.GetNextBlock(backMode);
            //Следующий задний
            int phX, phY;
            phX = 959 - screenOffset - resOffset - 100;
            phY = 674 - topOffset + 25;
            drawBlock(nextBlockBack, backBrick, phX + backScreenOffset, phY + backTopOffset, 0.45f);
            drawBlock(nextBlockFront, frontBrick, phX, phY, 1f);
            drawBlock(nextBlockFront, GLfrontBrick, phX, phY, 0.55f);
            //Рекорды тут
            DisplayScores(highscoreTable, _model, 969 + backScreenOffset + 7, 441 + backTopOffset, backColor * 0.20f);

            _GameMain.spriteBatch.Draw(sideFieldOff, new Rectangle(959 + backScreenOffset, 674 + backTopOffset - 245, 378 / 2, 394 / 2), Color.White * 0.45f);
            _GameMain.spriteBatch.Draw(sideFieldOn, new Rectangle(959, 674 - 245, 378 / 2, 394 / 2), Color.White);
            _GameMain.spriteBatch.Draw(GLsideFieldOn, new Rectangle(959, 674 - 245, 378 / 2, 394 / 2), Color.White * 0.60f);
            DisplayScores(highscoreTable, _model, 969 + 7, 441, frontColor);
            //Рамки под некстблоки
            _GameMain.spriteBatch.Draw(sideFieldOff, new Rectangle(959 + backScreenOffset, 674 + backTopOffset, 378 / 2, 394 / 2), Color.White * 0.45f);
            _GameMain.spriteBatch.Draw(sideFieldOn, new Rectangle(959, 674, 378 / 2, 394 / 2), Color.White);
            _GameMain.spriteBatch.Draw(GLsideFieldOn, new Rectangle(959, 674, 378 / 2, 394 / 2), Color.White * 0.60f);

        }


        private void raceDisplay(IRaceGameModel _model) {
            //Бэкмод объявлен наверху в глобальном скоупе
            frontMode = _model.Ferrari.mapId;
            //Бэкмод меняется здесь
            determinateColor();
            //Экран тут

            _GameMain.spriteBatch.Draw(Screen, new Rectangle(519, 87, 680, 855), Color.White);
            int score = _model.CurrentScore;
            var mapFront = _model.GetMap(frontMode);
            var mapBack = _model.GetMap(backMode);


            drawBrickMap(mapBack, backBrick, backScreenOffset, backTopOffset, 0.45f);
            _GameMain.spriteBatch.Draw(backField, new Rectangle(backScreenOffset + resOffset + screenOffset - 7, backTopOffset + topOffset - 6, 772 / 2, 1593 / 2), Color.White * 0.45f);

            drawBrickMap(mapFront, frontBrick, 0, 0, 1f);
            drawBrickMap(mapFront, GLfrontBrick, 0, 0, 0.55f);
            
            var FrontCar = _model.MainCar;
            drawCar(FrontCar, frontBrick, 0, 0, 1f);
            drawCar(FrontCar, GLfrontBrick, 0, 0, 0.45f);
            _GameMain.spriteBatch.Draw(frontField, new Rectangle(resOffset + screenOffset - 7, topOffset - 6, 772 / 2, 1593 / 2), Color.White);
            _GameMain.spriteBatch.Draw(GLfrontField, new Rectangle(resOffset + screenOffset - 6, topOffset - 6, 772 / 2, 1593 / 2), Color.White);

        }

        public void DisplayGameOver() {
            tehEnd = true;
        }

    }
}


