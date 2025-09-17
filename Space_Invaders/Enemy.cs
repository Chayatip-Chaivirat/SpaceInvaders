
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Enemy
    {
        Texture2D enemyTex;
        Vector2 pos;
        public static Rectangle enemyRec;

        public Enemy(Texture2D enemyTex, int x, int y )
        {
            this.enemyTex = enemyTex;
            this.pos = new Vector2 (x, y);
            enemyRec = new Rectangle(0, 0, 100, 90);
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
