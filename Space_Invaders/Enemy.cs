
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Enemy
    {
        Texture2D enemyTex;
        public Vector2 enemyPos;
        public Rectangle enemyRec;
        public bool enemyIsAlive = true;
        public Rectangle enemyHitBox;


        public bool lifeLost = false;
        public static int speed = 1;
        public static int moveDown = 20;

        public Enemy(Texture2D enemyTex, int x, int y)
        {
            this.enemyTex = enemyTex;
            this.enemyPos = new Vector2(x, y);

            enemyRec = new Rectangle(0, 0, 100, 90);
            this.enemyIsAlive = true;
            enemyHitBox = new Rectangle(x, y, 100, 90);
     

            
        }



        public void Update()
        {
            if (enemyIsAlive == true)
            {
                enemyHitBox.X = (int)enemyPos.X;
                enemyHitBox.Y = (int)enemyPos.Y;
                enemyHitBox.Width = enemyRec.Width;
                enemyHitBox.Height = enemyRec.Height;

                // Go down to player 
                int stopY = 800 - enemyTex.Height;
                if ((int)enemyPos.Y < stopY)
                {
                    enemyPos.Y += 1f;
                }

                enemyPos.X += speed;

                if (enemyPos.X <= 0 || enemyPos.X >= 950 - enemyTex.Width)
                {
                    speed *= -1;

                }

            }
        }

            public void MoveDown()
            {
                enemyPos.Y += moveDown;
        }



            //bool movingRight = true;

            //int stopY = 750 - enemyTex.Height;
            //int stopX = 950 - enemyTex.Width;

            //enemyPos.X += speed;

            //if ((int)enemyPos.X > stopX)
            //{
            //    speed = -1;
            //}
            //if ((int)enemyPos.X < 0)
            //    speed = 1;

            //{
            //    enemyPos.Y += 1;
            //}

        

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(enemyTex, enemyPos, enemyRec, Color.White);
        }
    }
}
