using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Bullet
    {
        Texture2D bulletTex;
        
        public Vector2 bulletPos;
        public static Rectangle bulletRec;
        public Rectangle bulletHitBox;
        public bool bulletUsed = false;


        public Bullet(Texture2D bulletTex,Vector2 bulletPos)
        {
            this.bulletTex = bulletTex;
            this.bulletPos = bulletPos;
            bulletRec = new Rectangle(0, 0, 67, 70); 
            bulletHitBox = new Rectangle((int)bulletPos.X, (int)bulletPos.Y, 20, 100);
            this.bulletUsed = false;
        }

        public void Update(Vector2 playerPos, GameTime gameTime) // behöver player position för att skjuta från rätt ställe
        {
            
            if (bulletUsed == false)
            {
                bulletPos.Y -= 80;
                bulletPos.X = playerPos.X + 20;
                bulletHitBox.Y = (int)bulletPos.Y;
                bulletHitBox.X = (int)bulletPos.X;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(bulletTex, bulletPos, bulletRec, Color.White);
        }
    }
}
