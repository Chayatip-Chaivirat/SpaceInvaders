using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    internal class Start
    {
        Texture2D startButtonTex;
        Vector2 position;
        public bool startButtonClicked = false;
        Rectangle startButtonRec;

        public Start(Texture2D startButtonTex, int x, int y)
        {
            this.startButtonTex = startButtonTex;
            this.position = new Vector2(x, y);
            this.startButtonRec = new Rectangle(x,y, startButtonTex.Width, startButtonTex.Height);
        }

        public void Clicked()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && startButtonRec.Contains(Mouse.GetState().Position))
            {
                // Click to start the game and remove the start screen
                this.startButtonTex = null;
                this.startButtonRec = Rectangle.Empty;
                startButtonClicked = true;

            }
        }

        public void Update()
        {
            // Any update logic for the start screen can go here
        }

        public void Draw(SpriteBatch sb)
        {
            if (startButtonTex != null)
            {
                sb.Draw(startButtonTex, position, Color.White);
            }
                
        }
    }
}
