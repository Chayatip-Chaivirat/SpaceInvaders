using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Invaders;


internal class Player
{
    public Texture2D playerTexture;
    public Vector2 pos1 = Vector2.Zero;
    int speed = 15;

    public Player(Texture2D tex, Vector2 startPos)
    {
        this.playerTexture = tex;
        this.pos1 = startPos;
        
    }

    public void Update(int windowWidth)
    { // Check for keyboard input
        KeyboardState state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
        { 
            pos1.X -= speed * 0.046f;
        }
        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
        { 
            pos1.X += speed * 0.046f;
            pos1.X += speed * 0.046f;
        }

        int stopX = windowWidth - playerTexture.Width;
        // Stop positions from going off screen
        if (pos1.X < 0)
        {
            pos1.X = 0;

        } 
        if (pos1.X > stopX)
        {
            pos1.X = stopX;
        }
    }

    public void Draw(SpriteBatch spritebatch)
    {
        spritebatch.Draw(playerTexture, pos1, Color.White);
    }
}

