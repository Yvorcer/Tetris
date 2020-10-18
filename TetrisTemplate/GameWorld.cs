using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        TitleSceen,
        Playing,
        HelpScreen,
        GameOverScreen
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
    SpriteFont fontBig;

    private SoundEffect selectSound;
    private SoundEffect gameOverSound;

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

    TitleScreenState title;
    GameOverState over;
    HelpState help;

    bool pause = false, gohold = false;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.GameOverScreen;

        // Load in the Fonts
        font = TetrisGame.ContentManager.Load<SpriteFont>("Fonts/8-BitFontSmall");
        fontBig = TetrisGame.ContentManager.Load<SpriteFont>("fonts/8-BitFontBig");

        // Load in the Sounds
        selectSound = TetrisGame.ContentManager.Load<SoundEffect>("Sounds/Select");
        gameOverSound = TetrisGame.ContentManager.Load<SoundEffect>("Sounds/GameOver");

        nextBlock = new NextBlock();
        grid = new TetrisGrid();
        block = new Block();
        score = new Score();
        hold = new Hold();

        //States
        title = new TitleScreenState();
        over = new GameOverState();
        help = new HelpState();

        block.Grid = grid;
        grid.Gblock = block;
        score.grid = grid;
        score.font = font;
        hold.font = font;
        title.fontBig = fontBig;
        over.fontBig = fontBig;
        help.fontBig = fontBig;
        
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Space))
        {
            selectSound.Play();
            if (pause)
                pause = false;
            else
                pause = true;
        }
        if (inputHelper.KeyPressed(Keys.F))
        {
            selectSound.Play();
            gohold = true;
        }

        //Switching GameState 
        if (gameState == GameState.TitleSceen)
        {
            if (inputHelper.KeyPressed(Keys.Enter))
            {
                selectSound.Play();
                gameState = GameState.Playing;
            }
        }
        if (gameState == GameState.TitleSceen)
        {
            if (inputHelper.KeyPressed(Keys.H))
            {
                selectSound.Play();
                gameState = GameState.HelpScreen;
            }
        }
        if (gameState == GameState.HelpScreen)
        {
            if (inputHelper.KeyPressed(Keys.Back))
            {
                selectSound.Play();
                gameState = GameState.TitleSceen;
            }
        }
        if (gameState == GameState.GameOverScreen)
        {
            if (inputHelper.KeyPressed(Keys.Enter))
            {
                selectSound.Play();
                gameState = GameState.TitleSceen;
            }
        }
    }

    public void Update(GameTime gameTime)
    {
        if (gameState == GameState.Playing)
        {
            if (pause)
                return;
            if (grid.GameOver())
            {
                return;
            }
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

            // Tetris Song only play when in the GameState.Play
                Song themeSong = TetrisGame.ContentManager.Load<Song>("Sounds/TetrisSong");
                MediaPlayer.Volume += -0.9f;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(themeSong);
        }

    }
        

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (gameState == GameState.TitleSceen)
        {
            title.Draw(spriteBatch);
        }
        if (gameState == GameState.HelpScreen)
        {
            help.Draw(spriteBatch);
        }
        if (gameState == GameState.Playing)
        {
            spriteBatch.Begin();
            grid.Draw(gameTime, spriteBatch);
            block.Draw(spriteBatch);
            nextBlock.Draw(spriteBatch);
            score.Draw(spriteBatch);
            hold.Draw(spriteBatch);
            spriteBatch.End();
        }
        if (gameState == GameState.GameOverScreen)
        {
            over.Draw(spriteBatch);
        }
   
    }

    public void Reset()
    {
    }

}
