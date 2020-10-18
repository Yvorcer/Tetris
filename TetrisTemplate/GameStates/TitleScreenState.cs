using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TitleScreenState
{
    private Texture2D titleScreen_Background;
    public SpriteFont fontBig;

    public void Draw(SpriteBatch spriteBatch)
    {
        titleScreen_Background = TetrisGame.ContentManager.Load<Texture2D>("Assets/TetrisTitleScreen");

        spriteBatch.Begin();
        spriteBatch.Draw(titleScreen_Background, new Vector2(0, 0), Color.White);
        spriteBatch.DrawString(fontBig, "Press 'Enter' To Continue", new Vector2(25, 450), Color.IndianRed);
        spriteBatch.End();
    }
}

