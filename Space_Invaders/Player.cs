using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


internal class Player
{
    public Texture2D playerTexture;
    public Vector2 pos1 = Vector2.Zero;
    public int Lives = 5;


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
        { // player position (x) decreases moving left
            pos1.X -= speed * 0.046f;
        }
        if (state.IsKeyDown(Keys.Right))
        { // player position (x) increases moving left
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


    public void Draw(SpriteBatch sb)
    {
        sb.Draw(playerTexture, pos1, Color.Blue);
    }
}

