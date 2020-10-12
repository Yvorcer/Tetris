using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }


    public bool[,] boolgrid;
    public Color[,] colorgrid;
    private Texture2D block;
    public Vector2 position;
    public Block Gblock;
    public bool newblock = false;
    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        position = Vector2.Zero;
        block = TetrisGame.ContentManager.Load<Texture2D>("block");
        boolgrid = new bool[,]
        {
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
            {false, false, false, false, false, false, false, false, false, false },
        };
        colorgrid = new Color[20, 10];
        for (int L = 0; L < 20; L++)
        {
            for (int W = 0; W < 10; W++)
            {
                colorgrid[L, W] = Color.White;
            }
        }
        Clear();
    }

    /// <summary>
    /// merge's the block into the grid
    /// </summary>
    private void Merge()
    {
        for (int L = 0; L < 4; L++)
        {
            for (int W = 0; W < 4; W++)
            {
                int x = Convert.ToInt32(W + Gblock.position.X);
                int y = Convert.ToInt32(L + Gblock.position.Y);
                if (Gblock.blockmat[L, W] && y >= 0)
                {
                    boolgrid[y, x] = Gblock.blockmat[L, W];
                    colorgrid[y, x] = Gblock.blockcolor;
                }
            }
        }
    }

    /// <summary>
    /// detects a block's collision with the bottom or another block
    /// </summary>
    /// <returns></returns>
    private bool Collision()
    {
        bool collosion = false;
        for (int L = 0; L < 4; L++)
        {
            for (int W = 0; W < 4; W++)
            {
                if (Gblock.blockmat[L, W] &&
                    (L + Gblock.position.Y >= 20 ||
                    boolgrid[Convert.ToInt32(L + Gblock.position.Y), Convert.ToInt32(W + Gblock.position.X)]))
                {
                    collosion = true;
                }
            }
        }
        return collosion;
    }

    /// removes a full row of blocks
    private void Removerow()
    {
        for (int L = 0; L < 20; L++)
        {
            bool complete = true;
            for (int W = 0; W < 10; W++)
            {
                if (boolgrid[L, W] == false)
                {
                    complete = false;
                    break;
                }
            }
            if (complete)
            {
                for (int Le = (L - 1); Le > 0; Le--)
                {
                    for (int W = 0; W < 10; W++)
                    {
                        boolgrid[Le + 1, W] = boolgrid[Le, W];
                        colorgrid[Le + 1, W] = colorgrid[Le, W];
                    }
                }
            }
        }
    }

    /// Makes sure that when a blockhits the top row of the grid the game stops
    public bool GameOver()
    {
        int L = 0;
        bool gameover = false;
        for (int W = 0; W < 10; W++)
        {
            if (boolgrid[L, W])
            {
                gameover = true;
                break;
            }
        }
        return gameover;
    }
    
    public void Update()
    {
        if (Gblock.IsA == 1 && Collision())
        {
            Gblock.position.X++;
        }
        if (Gblock.IsA == 2 && Collision())
        {
            Gblock.position.X--;
        }
        if (Gblock.IsA == 3 && Collision())
        {
            Turnback();
        }
        if (Collision())
        {
            /// this is a block that hits the ground/e previous block
            /// place sound here
            Gblock.position.Y--;
            Merge();
            if (GameOver())
                return;
            Removerow();
            Gblock.position = Gblock.choblock.StartPosi;
            newblock = true;
        }
    }

    ///a turn function if the turning of a block causes a collision
    private void Turnback()
    {
        var roGrid = Gblock.blockmat;
        Gblock.blockmat = new bool[,]
        {
                {roGrid[0,3], roGrid[1,3], roGrid[2,3], roGrid[3,3] },
                {roGrid[0,2], roGrid[1,2], roGrid[2,2], roGrid[3,2] },
                {roGrid[0,1], roGrid[1,1], roGrid[2,1], roGrid[3,1] },
                {roGrid[0,0], roGrid[1,0], roGrid[2,0], roGrid[3,0] },
        };
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        position = Vector2.Zero;
        for (int L = 0; L < 20; L++)
        {
            for (int W = 0; W < 10; W++)
            {
                if (W == 0)
                    position.X = 0;
                if (boolgrid[L, W])
                {
                    spriteBatch.Draw(block, position, colorgrid[L, W]);
                }
                else
                    spriteBatch.Draw(emptyCell, position, Color.White);
                position.X += block.Width;
            }
            position.Y += block.Height;
        }
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}

