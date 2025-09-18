
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Enemy
    {
        Texture2D enemyTex;
        Vector2 pos;
        public Rectangle enemyRec;
        public bool enemyIsAlive = true;
        public Rectangle enemyHitBox;

        public Enemy(Texture2D enemyTex, int x, int y )
        {
            this.enemyTex = enemyTex;
            this.pos = new Vector2 (x, y);
            enemyRec = new Rectangle(0, 0, 100, 90);
            this.enemyIsAlive = true;
            enemyHitBox = new Rectangle(x, y, 100, 90);
        }


        public void Update()
        {
           
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(enemyTex, pos, enemyRec, Color.White);
        }
    }
}
