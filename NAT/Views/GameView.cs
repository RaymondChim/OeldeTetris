using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NAT.Models;
using NAT.Views;
using NAT.Services;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System;

namespace NAT.Views
{
    class View : IGameView
    {
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
        private SpriteFont text;
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
        //Эмпирическией размер кирпичей
        private int brickSizeX = 37;
        private int brickSizeY = 39;


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
            _GameMain.graphics.PreferredBackBufferWidth = resX;
            _GameMain.graphics.PreferredBackBufferHeight = resY;
            _GameMain.Window.Position = new Point(0, 0);
            _GameMain.graphics.ApplyChanges();
            sizeModifier = 4000 / resX;

        }
        public void TestDisplay()
        {
            // 77.5 x 77.5 - cube or 37.2 x 37.2 
            // Я пишу топорный костыль, потом нужно пересчитывать в зависимости от разрешения
            /*
            _GameMain.GraphicsDevice.Clear(Color.White);
            _GameMain.spriteBatch.Begin();
            _GameMain.spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), new Rectangle(69,79,593,558), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset, topOffset, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37, topOffset + 37, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 * 2, topOffset + 37 * 2, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 * 3, topOffset + 37 * 3, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 * 4, topOffset + 37 * 4, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 * 5, topOffset + 37 * 5, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 * 6, topOffset + 37 * 6, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 * 7, topOffset + 37 * 7, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 * 8, topOffset + 37 * 8, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 * 9, topOffset + 37 * 9, 37, 37), Color.White);
            _GameMain.spriteBatch.DrawString(text, "Score", new Vector2(963, 153), Color.Black);
            _GameMain.spriteBatch.DrawString(text, "Speed", new Vector2(963, 277), Color.Black);
            
            if (modeTest == 1)
            { _GameMain.spriteBatch.Draw(red, new Rectangle(1379, 174, 38, 48), Color.White); }
            else
            { _GameMain.spriteBatch.Draw(red, new Rectangle(1379, 275, 38, 48), Color.White); }*/
            MouseState mouseState = Mouse.GetState();
            Vector2 coor = new Vector2(mouseState.X, mouseState.Y);
            //_GameMain.spriteBatch.Draw(loader, coor, Color.White);
            var x = Keyboard.GetState();
            var b = x.GetPressedKeys();
            //foreach (Keys e in b) { Debug.WriteLine(e); }

            _GameMain.spriteBatch.DrawString(text, Mouse.GetState().Position.ToString(), new Vector2(50, 50), Color.Black);
            //_GameMain.spriteBatch.DrawString(text, sizeModifier.ToString(), new Vector2(100, 100), Color.Black);
            _GameMain.spriteBatch.End();
        }
        public void Init(Scores scores) {
            highscoreTable = scores;
        }
        public void Display(IGameModel _model)
        {
            // Пока var, если собъётся - напиши через блоки и брики
            // 0 - жёлтый, 1 - зелёный
            _GameMain.GraphicsDevice.Clear(Color.White);
            _GameMain.spriteBatch.Begin();
            _GameMain.spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), /*new Rectangle(69,79,593,558), */Color.White);
            Texture2D frontField = null;
            Texture2D backField = null;
            Texture2D GLfrontField = null;
            Texture2D GLbackField = null;
            Texture2D frontBrick = null;
            Texture2D backBrick = null;
            Texture2D GLfrontBrick = null;
            Texture2D GLbackBrick = null;
            Texture2D sideFieldOn = null;
            Texture2D sideFieldOff = null;
            Texture2D GLsideFieldOn = null;
            Texture2D textFieldOn = null;
            Texture2D textFieldOff = null;
            Texture2D GLtextFieldOn = null;
            Texture2D Screen = null;
            int backMode;
            int frontMode = _model.CurrentMapId;
            int score = _model.CurrentScore;
            var mapFront = _model.BrickMap(frontMode);

            if (frontMode == 1)
            {
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
            _GameMain.spriteBatch.Draw(Screen, new Rectangle(519, 87, 680, 855), Color.White);
            var mapBack = _model.BrickMap(backMode);
            Block currentBlockFront = _model.GetCurrentBlock(frontMode);
            Block currentBlockBack = _model.GetCurrentBlock(backMode);
            //Debug.WriteLine(lenFront, "boom");
            //Вывод заднего слоя блоков
            drawBrickMap(mapBack, backBrick, backScreenOffset, backTopOffset, 0.45f);
            drawBlock(currentBlockBack, backBrick, backScreenOffset, backTopOffset, 0.45f);

            //Задний стакан
            _GameMain.spriteBatch.Draw(backField, new Rectangle(backScreenOffset+resOffset + screenOffset - 7, backTopOffset+ topOffset - 6, 772 / 2, 1593 / 2), Color.White *0.45f);
            //Вывод переднего слоя блоков
            //519 87
            drawBlock(currentBlockFront, frontBrick, 0, 0, 1f);
            drawBlock(currentBlockFront, GLfrontBrick, 0, 0, 0.55f);
            drawBrickMap(mapFront, frontBrick, 0, 0, 1f);
            drawBrickMap(mapFront, GLfrontBrick, 0, 0, 0.55f);
            //Передний стакан
            _GameMain.spriteBatch.Draw(frontField, new Rectangle(resOffset + screenOffset - 7, topOffset - 6, 772 / 2, 1593 / 2), Color.White);
            _GameMain.spriteBatch.Draw(GLfrontField, new Rectangle(resOffset + screenOffset - 6, topOffset - 6, 772 / 2, 1593 / 2), Color.White);


            Block nextBlockFront = _model.GetNextBlock(frontMode);
            Block nextBlockBack = _model.GetNextBlock(backMode);
            //Следующий задний
            int phX, phY;
            phX = 959 - screenOffset - resOffset - 100;
            phY = 674 - topOffset + 25;
            drawBlock(nextBlockBack, backBrick, phX+ backScreenOffset, phY + backTopOffset, 0.45f);
            drawBlock(nextBlockFront, frontBrick, phX, phY, 1f);
            drawBlock(nextBlockFront, GLfrontBrick, phX, phY, 0.55f);

            _GameMain.spriteBatch.Draw(sideFieldOff, new Rectangle(959 + backScreenOffset, 674 + backTopOffset, 378 / 2, 394 / 2), Color.White*0.45f);
            _GameMain.spriteBatch.Draw(sideFieldOn, new Rectangle(959, 674, 378/2, 394/2), Color.White);
            _GameMain.spriteBatch.Draw(GLsideFieldOn , new Rectangle(959, 674, 378 / 2, 394 / 2), Color.White*0.60f);
            DisplayScores(highscoreTable, _model);
            _GameMain.spriteBatch.End();

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

        public void DisplayScores(Scores scores, IGameModel _model) {
            int startX = 969, startY = 441;
            var scoreArray = scores.Heaver.Union(new ScoreHeaver[] { new ScoreHeaver("XXX",_model.CurrentScore)}).OrderByDescending(x => x.Score).Take(10).ToList();
            for (int i = 0; i < scoreArray.Count() ; i++) {
                _GameMain.spriteBatch.DrawString(text, scoreArray[i].Name + "    " + scoreArray[i].Score, new Vector2(startX, startY + 15 * i), Color.Black);
            }
            /*
            for (int i = 0; i < 10; i++) {
                _GameMain.spriteBatch.DrawString(text, "Score", new Vector2(startX, startY+15*i), Color.Black);
            }*/
            
 //969 441
        }

        public Keys[] UpdateuserInput()
        {
            var KeyArray = Keyboard.GetState().GetPressedKeys();
            return KeyArray;
        }

        public void DisplayGameOver() {
            //TODO : DO
        }

    }
}


