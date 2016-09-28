using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NAT.Models;
using NAT.Views;
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
        private Texture2D textBorderY;
        private Texture2D GLtextBorderY;
        private Texture2D sideFieldY;
        private Texture2D GLsideFieldY;
        private Texture2D fieldY;
        private Texture2D GLfieldY;
        private SpriteFont text;
        private readonly GameMain _GameMain;
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
            background = _GameMain.Content.Load<Texture2D>("tetris_screen2");
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
            Texture2D sideFieldOn = null;
            Texture2D sideFieldOff = null;
            Texture2D GLsideFieldOn = null;
            Texture2D textFieldOn = null;
            Texture2D textFieldOff = null;
            Texture2D GLtextFieldOn = null;
            int backMode;
            int frontMode = _model.CurrentMapId;
            int score = _model.CurrentScore;
            var mapFront = _model.BrickMap(frontMode);

            if (frontMode == 1)
            {
                frontBrick = tileY; 
                backBrick = tileY; //plc
                frontField = fieldG;
                GLfrontField = GLfieldY;
                backField = fieldY; //plc
                GLbackField = GLfieldY;// plc
                sideFieldOn = sideFieldY;
                GLsideFieldOn = GLsideFieldY;
                sideFieldOff = sideFieldY; //plc
                textFieldOn = textBorderY;
                textFieldOff = textBorderY; // plc
                GLtextFieldOn = GLtextBorderY;                
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(1379, 174, 38, 48), Color.White);
                backMode = 0;
            }
            else
            {
                frontBrick = tileY; 
                backBrick = tileY; //plc
                frontField = fieldY;
                GLfrontField = GLfieldY;
                backField = fieldG; //plc
                GLbackField = GLfieldY;// plc
                sideFieldOn = sideFieldY;
                GLsideFieldOn = GLsideFieldY;
                sideFieldOff = sideFieldG; //plc
                textFieldOn = textBorderY;
                textFieldOff = textBorderY; // plc
                GLtextFieldOn = GLtextBorderY;
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(1379, 275, 38, 48), Color.White);
                backMode = 1;
            }
            var mapBack = _model.BrickMap(backMode);
            int lenFront = mapFront.Length;
            int lenBack = mapBack.Length;
            Block currentBlockFront = _model.GetCurrentBlock(frontMode);
            Block currentBlockBack = _model.GetCurrentBlock(backMode);
            int lenBlockFront = currentBlockFront.Bricks.Length;
            int lenBlockBack = currentBlockBack.Bricks.Length;
            //Debug.WriteLine(lenFront, "boom");
            //Вывод заднего слоя блоков
            for (int i = lenBack - 1; i >= 0; i--)
            {
                int x, y;
                x = mapBack[i].Xpos;
                y = mapBack[i].Ypos;
                _GameMain.spriteBatch.Draw(backBrick, new Rectangle(backScreenOffset+resOffset + screenOffset + brickSizeX * x, backTopOffset+topOffset + brickSizeY * y, brickSizeX, brickSizeY), Color.White * 0.45f);
            }
            for (int i = lenBlockFront - 1; i >= 0; i--)
            {
                int x, y;
                x = currentBlockBack.Bricks[i].Xpos;
                y = currentBlockBack.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(frontBrick, new Rectangle(backScreenOffset + resOffset + screenOffset + brickSizeX * x, backTopOffset+ topOffset + brickSizeY * y, brickSizeX, brickSizeY), Color.White * 0.45f);
            }
            //Задний стакан
            _GameMain.spriteBatch.Draw(backField, new Rectangle(backScreenOffset+resOffset + screenOffset - 7, backTopOffset+ topOffset - 6, 772 / 2, 1593 / 2), Color.White *0.45f);
            //Вывод переднего слоя блоков
            for (int i = lenBlockBack-1; i >= 0; i--)
            {
                int x, y;
                x = currentBlockFront.Bricks[i].Xpos;
                y = currentBlockFront.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(resOffset + screenOffset + brickSizeX * x, topOffset + brickSizeY * y, brickSizeX, brickSizeY), Color.White);
            }
            ///Вывод переднего курблока

            for (int i = lenFront - 1; i >= 0; i--)
            {   //TODO: грамотный вывод слоёв (опасити на фронт, возможно ~(Color.White*0.7f))
                int x, y;
                x = mapFront[i].Xpos;
                y = mapFront[i].Ypos;
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(resOffset + screenOffset + brickSizeX * x, topOffset + brickSizeY * y, brickSizeX, brickSizeY), Color.White);
            }

            //Передний стакан
            _GameMain.spriteBatch.Draw(frontField, new Rectangle(resOffset + screenOffset - 7, topOffset - 6, 772 / 2, 1593 / 2), Color.White);
            _GameMain.spriteBatch.Draw(GLfrontField, new Rectangle(resOffset + screenOffset - 6, topOffset - 6, 772 / 2, 1593 / 2), Color.White);


            Block nextBlockFront = _model.GetNextBlock(frontMode);
            Block nextBlockBack = _model.GetNextBlock(backMode);
            int lenNextBlockFront = currentBlockFront.Bricks.Length;
            int lenNextBlockBack = currentBlockBack.Bricks.Length;
            //Следующий задний
            for (int i = lenNextBlockBack - 1; i >= 0; i--)
            {
                int x, y;
                x = nextBlockBack.Bricks[i].Xpos;
                y = nextBlockBack.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(/*resOffset + screenOffset*/961 + brickSizeX * x, /*topOffset*/675 + brickSizeY * y, brickSizeX, brickSizeY), Color.White);
            }

            //Следующий передний 961 675
            for (int i = lenNextBlockFront - 1; i >= 0; i--)
            {
                int x, y;
                x = nextBlockFront.Bricks[i].Xpos;
                y = nextBlockFront.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(tileY, new Rectangle(/*resOffset + screenOffset*/700 + brickSizeX * x, /*topOffset*/800 + brickSizeY * y, brickSizeX, brickSizeY), Color.White*0.45f);
            }
            _GameMain.spriteBatch.Draw(sideFieldOff, new Rectangle(959 + backScreenOffset, 674 + backTopOffset, 378 / 2, 394 / 2), Color.White*0.45f);
            _GameMain.spriteBatch.Draw(sideFieldOn, new Rectangle(959, 674, 378/2, 394/2), Color.White);
            _GameMain.spriteBatch.Draw(GLsideFieldOn , new Rectangle(959, 674, 378 / 2, 394 / 2), Color.White);
            /*

            
          
            for (int i = lenBlockBack; i > 0; i--){
                int x, y;
                x = currentBlockFront.Bricks[i].Xpos;
                y = currentBlockFront.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + brickSizeX * x, topOffset + brickSizeY * y, brickSizeX, brickSizeY), Color.White);
            }
            */
            _GameMain.spriteBatch.End();

        }
        public Keys[] UpdateuserInput()
        {
            var KeyArray = Keyboard.GetState().GetPressedKeys();
            //Debug.WriteLine(KeyArray.Length);
            return KeyArray;
        }

        public void DisplayGameOver() {
            //TODO : DO
        }

    }
}


