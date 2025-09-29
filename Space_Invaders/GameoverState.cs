using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class GameoverState
    {
        Texture2D gameoverTex;
        Rectangle gameoverRec;
        Vector2 gameoverPos;
        public GameoverState(Texture2D gameoverTex, Vector2 gameoverPos)
        {
            this.gameoverTex = gameoverTex;
            this.gameoverRec = new Rectangle(0, 0, 800, 600);
            this.gameoverPos = gameoverPos;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gameoverTex, gameoverPos, Color.White);
        }
    }
}
