using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Player
    {
        public Texture2D playerTexture;
        public Vector2 pos1 = Vector2.Zero;
        public Player(Texture2D tex, Vector2 startPos)
        {
            this.playerTexture = tex;
            this.pos1 = startPos;
        }

        public void Update(int windowWidth)
        {
            pos1.X = pos1.X + 2;
            int stopX = windowWidth - playerTexture.Width;
            if (pos1.X > stopX)
                {
                pos1.X = stopX;
            }


        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(playerTexture, pos1, Color.Blue);
        }
    }
}
