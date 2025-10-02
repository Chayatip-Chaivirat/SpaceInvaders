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
        public Rectangle bulletHitBox;
        public bool bulletUsed = false;


        public Bullet(Texture2D bulletTex,Vector2 bulletPos)
        {
            this.bulletTex = bulletTex;
            this.bulletPos = new Vector2(bulletPos.X + 20, bulletPos.Y);
            bulletHitBox = new Rectangle((int)this.bulletPos.X, (int)this.bulletPos.Y, bulletTex.Width, bulletTex.Height);
            this.bulletUsed = false;
        }

        public void Update(Vector2 playerPos, GameTime gameTime) // Player position to set the bullet position when fired
        {
            if (bulletUsed == false)
            {
                bulletPos.Y -= 10;

                bulletHitBox.Y = (int)bulletPos.Y;
                bulletHitBox.X = (int)bulletPos.X;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(bulletTex, bulletPos, Color.White);
        }
    }
}
