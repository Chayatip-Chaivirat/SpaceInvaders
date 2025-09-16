using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Bullet
    {
        Texture2D bulletTex;
        Vector2 bulletPos;
        Rectangle bulletRec;

        public Bullet(Texture2D bulletTex,Vector2 bulletPos)
        {
            this.bulletTex = bulletTex;
            this.bulletPos = bulletPos;
            bulletRec = new Rectangle(0, 0, 67, 70);   
        }

        public void Update()
        {
            bulletPos.Y -= 7;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(bulletTex, bulletPos, bulletRec, Color.White);
        }
    }
}
