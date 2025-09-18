using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


internal class Title
{
    public Texture2D titleTex;
    public Vector2 pos1 = Vector2.Zero;


    public Title(Texture2D tex, Vector2 startPos)
    {
        this.titleTex = tex;
        this.pos1 = startPos;


    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(titleTex, pos1, Color.White);
    }
}