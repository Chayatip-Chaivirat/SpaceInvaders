using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Bullet
    {
        Texture2D bulletTex;
        Vector2 bulletPos;
        public static Rectangle bulletRec;
        public Rectangle bulletHitBox;
        public bool bulletUsed = false;
        Vector2 playerPos;


        public Bullet(Texture2D bulletTex,Vector2 bulletPos)
        {
            this.bulletTex = bulletTex;
            this.bulletPos = bulletPos;
            bulletRec = new Rectangle(0, 0, 67, 70); 
            bulletHitBox = new Rectangle((int)bulletPos.X, (int)bulletPos.Y, 20, 50);
        }

        public void Update(Vector2 playerPos) // behöver player position för att skjuta från rätt ställe
        {
            bulletPos.X = playerPos.X + 20;
            if (bulletUsed == false)
            {
                //bulletPos.X = playerPos.X;
                //bulletPos.Y -= 5;
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
