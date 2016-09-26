using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NAT.Models;
using NAT.Models.GameCore;
using System.Diagnostics;
using System.Windows;


namespace NAT.Views {
    public interface IGameView {
        // событие "отрабатывающее" пи нажатии клавиши пользователем
        //Action<string> KeyPressed { get; }

        // Отображаеть модель
        void Display(IGameModel _model);
        void TestDisplay();
        // тут вьюшка должна прочекать пользовательский ввод и вызвать событие если надо
        void UpdateuserInput();
    }

    class View : IGameView
    {
        private Texture2D background;
        private Texture2D loader;
        private Texture2D red;
        private SpriteFont text;
        private readonly GameMain _GameMain;
        private int resX = 1920;
        private int resY = 1080;
        private int resOffset = 489;
        private int screenOffset = 60;
        private int topOffset = 119;
        private int sizeModifier = 1;
        private int modeTest = 1;
        private int brickSize = 37;


        public View(GameMain game)
        {
            _GameMain = game;
        }

        public void LoadContent()
        {
            background = _GameMain.Content.Load<Texture2D>("tetris_screen2");
            loader = _GameMain.Content.Load<Texture2D>("loader");
            text = _GameMain.Content.Load<SpriteFont>("text");
            red = _GameMain.Content.Load<Texture2D>("red");
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
            _GameMain.GraphicsDevice.Clear(Color.White);
            _GameMain.spriteBatch.Begin();
            _GameMain.spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), /*new Rectangle(69,79,593,558), */Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset+screenOffset, topOffset, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset+37, topOffset+37, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 *2, topOffset + 37 *2, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 *3, topOffset + 37 *3, 37, 37), Color.White);
            _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + 37 *4, topOffset + 37 *4, 37, 37), Color.White);
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
            { _GameMain.spriteBatch.Draw(red, new Rectangle(1379, 275, 38, 48), Color.White); }
            MouseState mouseState = Mouse.GetState();
            Vector2 coor = new Vector2(mouseState.X, mouseState.Y);
            //_GameMain.spriteBatch.Draw(loader, coor, Color.White);
            var x = Keyboard.GetState();
            var b = x.GetPressedKeys();
            foreach (Keys e in b) { Debug.WriteLine(e); }
            
            _GameMain.spriteBatch.DrawString(text, Mouse.GetState().Position.ToString(), new Vector2(50, 50), Color.Black);
            //_GameMain.spriteBatch.DrawString(text, sizeModifier.ToString(), new Vector2(100, 100), Color.Black);
            _GameMain.spriteBatch.End();
        }
        public void Display(IGameModel _model) {
            // Пока var, если собъётся - напиши через блоки и брики
            _GameMain.GraphicsDevice.Clear(Color.White);
            _GameMain.spriteBatch.Begin();
            _GameMain.spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), /*new Rectangle(69,79,593,558), */Color.White);
            int backMode;
            int frontMode = _model.CurrentMapId;
            int score = _model.CurrentScore;
            var mapFront = _model.BrickMap(frontMode);
            if (frontMode == 1)
            { _GameMain.spriteBatch.Draw(red, new Rectangle(1379, 174, 38, 48), Color.White);
               backMode = 0;
            }
            else
            {
                _GameMain.spriteBatch.Draw(red, new Rectangle(1379, 275, 38, 48), Color.White);
                backMode = 1;
            }
            var mapBack = _model.BrickMap(backMode);
            int lenFront = mapFront.Length;
            int lenBack = mapBack.Length;
            for (int i = lenFront; i > 0; i--)
            {   //TODO: грамотный вывод слоёв (опасити на фронт, возможно ~(Color.White*0.7))
                int x, y;
                x = mapFront[i].Xpos;
                y = mapFront[i].Ypos;
                _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + brickSize*x, topOffset+brickSize*y, brickSize, brickSize), Color.White);
            }
            for (int i = lenBack; i > 0; i--) {
                int x, y;
                x = mapBack[i].Xpos;
                y = mapBack[i].Ypos;
                _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + brickSize * x, topOffset + brickSize * y, brickSize, brickSize), Color.White);
            }
            Block currentBlockFront = _model.GetCurrentBlock(frontMode);
            Block currentBlockBack = _model.GetCurrentBlock(backMode);
            int lenBlockFront = currentBlockFront.Bricks.Length;
            int lenBlockBack = currentBlockBack.Bricks.Length;
            for (int i = lenBlockFront; i > 0; i--){
                int x, y;
                x = currentBlockFront.Bricks[i].Xpos;
                y = currentBlockFront.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + brickSize * x, topOffset + brickSize * y, brickSize, brickSize), Color.White);
            }
            for (int i = lenBlockBack; i > 0; i--){
                int x, y;
                x = currentBlockFront.Bricks[i].Xpos;
                y = currentBlockFront.Bricks[i].Ypos;
                _GameMain.spriteBatch.Draw(red, new Rectangle(resOffset + screenOffset + brickSize * x, topOffset + brickSize * y, brickSize, brickSize), Color.White);
            }

        }
        public void UpdateuserInput() { }
    }
}
