using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class HelpState
{
    public SpriteFont fontBig;

    public void Draw(SpriteBatch spritebatch)
    {
        spritebatch.Begin();
        spritebatch.DrawString(fontBig, "HELPSTATE Press BackSpace to return to Title", new Vector2(25, 300), Color.IndianRed);
        spritebatch.End();
    }
    
}
