
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Enemy
    {
        Texture2D enemyTex;
        public Vector2 pos;
        Rectangle enemyRec;

        public Enemy(Texture2D enemyTex)
        {
            this.enemyTex = enemyTex;
            enemyRec = new Rectangle(0, 0, 100, 90);
        }

        public void Update()
        {
            pos.Y += 1;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(enemyTex, new Vector2(25, 25), enemyRec, Color.White);
        }
    }
}
