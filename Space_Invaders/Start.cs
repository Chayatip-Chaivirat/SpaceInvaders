using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Start
    {
        Texture2D backgroundPicture;
        Texture2D spriteSheet;
        Rectangle sourceRec;
        Vector2 position;

        public Start(Texture2D backgroundPicture, int x, int y)
        {
            this.backgroundPicture = backgroundPicture;
            this.position = new Vector2(x, y);
            this.spriteSheet = spriteSheet;
            this.sourceRec = sourceRec;
        }
        public void Update()
        {
            // Any update logic for the start screen can go here
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(backgroundPicture, position, sourceRec, Color.White);
            sb.Draw(spriteSheet, position, sourceRec, Color.White);
        }
    }
}
