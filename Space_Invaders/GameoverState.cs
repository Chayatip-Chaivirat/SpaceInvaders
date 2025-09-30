using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Space_Invaders
{
    internal class GameoverState
    {
        Texture2D gameoverTex;
        Rectangle gameoverRec;
        Vector2 gameoverPos;
        Random randomPos;
        int stopX;
        int stopY;
        public GameoverState(Texture2D gameoverTex, Vector2 gameoverPos)
        {
            randomPos = new Random();
            this.gameoverTex = gameoverTex;
            this.gameoverRec = new Rectangle(0, 0, gameoverTex.Width, gameoverTex.Height);
            stopX = 700 - gameoverTex.Width; //Window.ClientBounds.Width doesn't work here
            stopY = 950 - gameoverTex.Height; //Window.ClientBounds.Height doesn't work here

            int randX = randomPos.Next(0, stopX);
            int randY = randomPos.Next(0, stopY);
            this.gameoverPos = new Vector2(randX, randY);
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gameoverTex, gameoverPos, Color.GreenYellow);
        }
    }
}
