using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Invaders;


internal class Player
{
    public Texture2D playerTexture;
    public Vector2 pos1 = Vector2.Zero;


    public Player(Texture2D tex, Vector2 startPos)
    {
        this.playerTexture = tex;
        this.pos1 = startPos;
        
    }
   

    Rectangle startpos = new Rectangle(0, 0, 32, 32);
    int speed = 100;


    public void Update(int windowWidth)
    { // Check whihc keys are pressed
        KeyboardState state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.Left))
        { 
            pos1.X -= speed * 0.046f;
        }
        if (state.IsKeyDown(Keys.Right))
        { 
            pos1.X += speed * 0.046f;
            pos1.X += speed * 0.046f;
        }

        int stopX = windowWidth - playerTexture.Width;
        // stop at edges
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

