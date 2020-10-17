using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    enum GameState
    {
        Playing,
        GameOver
    }

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;
    Block block;
    NextBlock nextBlock;
    Score score;
    Hold hold;
    bool pause = false, gohold = false;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        nextBlock = new NextBlock();
        grid = new TetrisGrid();
        block = new Block();
        score = new Score();
        hold = new Hold();
        block.Grid = grid;
        grid.Gblock = block;
        score.grid = grid;
        score.font = font;
        hold.font = font;
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Space))
        {
            if (pause)
                pause = false;
            else
                pause = true;
        }
        if (inputHelper.KeyPressed(Keys.F))
        {
            gohold = true;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (pause)
            return;
        if (grid.GameOver())
            return;
        block.Update(gameTime);
        grid.Update();
        score.Update();
        if (gohold)
        {
            hold.HolBlock(block);
            if (hold.holding == false)
            {
                block.Newblock(nextBlock.nexblock);
                nextBlock = new NextBlock();
                grid.newblock = false;
                hold.holding = true;
            }
            gohold = false;
        }
        if (grid.newblock)
        {
            block.Newblock(nextBlock.nexblock);
            nextBlock = new NextBlock();
            grid.newblock = false;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        block.Draw(spriteBatch);
        nextBlock.Draw(spriteBatch);
        score.Draw(spriteBatch);
        hold.Draw(spriteBatch);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}
