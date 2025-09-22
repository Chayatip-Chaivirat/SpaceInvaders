
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Enemy
    {
        Texture2D enemyTex;
        Vector2 enemyPos;
        public Rectangle enemyRec;
        public bool enemyIsAlive = true;
        public Rectangle enemyHitBox;

        public Enemy(Texture2D enemyTex, int x, int y )
        {
            this.enemyTex = enemyTex;
            this.enemyPos = new Vector2 (x, y);
            enemyRec = new Rectangle(0, 0, 100, 90);
            this.enemyIsAlive = true;
            enemyHitBox = new Rectangle(x, y, 100, 90);
        }


        public void Update()
        {
           enemyHitBox.X = (int)enemyPos.X;
           enemyHitBox.Y = (int)enemyPos.Y;
           enemyHitBox.Width = enemyRec.Width;
           enemyHitBox.Height = enemyRec.Height;

            // Go down to player 
            int stopY = 750 - enemyTex.Height;
            if ((int) enemyPos.Y < stopY)
            {
                enemyPos.Y += 1;
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(enemyTex, enemyPos, enemyRec, Color.White);
        }
    }
}
