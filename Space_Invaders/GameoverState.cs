using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Space_Invaders
{
    internal class GameoverState
    {
        public Texture2D gameoverTex;
        public Rectangle gameoverRec;
        public Vector2 gameoverPos;
        Random randomPos;
        int stopX;
        int stopY;

        Point frameSize = new Point(110, 100);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(5, 1);
        int timeSinceLastFrame = 0;
        int millisecondPerFrame = 50;
        public GameoverState(Texture2D gameoverTex, Vector2 gameoverPos)
        {
            randomPos = new Random();
            this.gameoverTex = gameoverTex;
            this.gameoverRec = new Rectangle(0,0,frameSize.X,frameSize.Y);
            stopX = 700 - gameoverTex.Width; //Window.ClientBounds.Width doesn't work here
            stopY = 950 - gameoverTex.Height; //Window.ClientBounds.Height doesn't work here

            int randX = randomPos.Next(0, stopX);
            int randY = randomPos.Next(0, stopY);
            this.gameoverPos = new Vector2(randX, randY);
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondPerFrame)
            {
                timeSinceLastFrame = 0;
            }
            ++currentFrame.X;
            if (currentFrame.X >= sheetSize.X)
            {
                currentFrame.X = 0;
                ++currentFrame.Y;
                if (currentFrame.Y >= sheetSize.Y)
                {
                    currentFrame.Y = 0;
                }
            }
            gameoverRec = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gameoverTex, gameoverPos, gameoverRec, Color.GreenYellow, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
