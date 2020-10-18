using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class GameOverState
{
    private Texture2D gameOverScreen_Background;
    public SpriteFont fontBig;
    public TetrisGrid grid;
    public Score score;

    public void Draw(SpriteBatch spriteBatch)
    {
        gameOverScreen_Background = TetrisGame.ContentManager.Load<Texture2D>("Assets/GameOverScreen");

        spriteBatch.Begin();
        spriteBatch.Draw(gameOverScreen_Background, new Vector2(0, 0), Color.White);
        spriteBatch.DrawString(fontBig, "Score: " , new Vector2(200, 550), Color.Black);  //+ grid.level
        spriteBatch.DrawString(fontBig, "Level: ", new Vector2(500, 550), Color.Black); //+score.scoreaem
        spriteBatch.End();
    }


}
