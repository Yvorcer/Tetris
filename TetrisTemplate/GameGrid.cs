using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class GameGrid
{
    public bool[,] boolgrid;
    public Color[,] colorgrid;
    private Texture2D block;
    public Vector2 position;
    public Block Gblock;
    public bool newblock = false;

    public GameGrid()
    {
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
        for(int L = 0; L < 20; L++)
        {
            for (int W = 0; W < 10; W++)
            {
                colorgrid[L, W] = Color.White;
            }
        }
    }

    private void Merge()
    {
        for (int L = 0; L < 4; L++)
        {
            for (int W = 0; W < 4; W++)
            {
                int x = Convert.ToInt32(W + Gblock.position.X);
                int y = Convert.ToInt32(L + Gblock.position.Y);
                if(Gblock.blockmat[L,W] && y >= 0)
                {
                    boolgrid[y, x] = Gblock.blockmat[L, W];
                    colorgrid[y, x] = Gblock.blockcolor;
                }
            }
        }
    }

    private bool Collision()
    {
        bool collosion = false;
        for (int L = 0; L < 4; L++)
        {
            for (int W = 0; W < 4; W++)
            {
                if (Gblock.blockmat[L, W] &&
                    (L + Gblock.position.Y >= 20 ||
                    boolgrid[Convert.ToInt32(L + Gblock.position.Y), Convert.ToInt32(W + Gblock.position.X)])){
                    collosion = true;
                }
            }
        }
        return collosion;
    }

    private void Removerow()
    {
        for(int L = 0; L < 20; L++)
        {
            bool complete = true;
            for(int W = 0; W < 10; W++)
            {
                if(boolgrid[L, W] == false)
                {
                    complete = false;
                    break;
                }
            }
            if (complete)
            {
                for(int Le = (L-1); Le > 0; Le--)
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
        if(Gblock.IsA == 1 && Collision())
        {
            Gblock.position.X++;
        }
        if (Gblock.IsA == 2 && Collision())
        {
            Gblock.position.X--;
        }
        if (Collision())
        {
            Gblock.position.Y--;
            Merge();
            if (GameOver())
                return;
            Removerow();
            Gblock.position = Gblock.choblock.StartPosi;
            newblock = true;
        }
    }

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
                position.X += block.Width;
            }
            position.Y += block.Height;
        }
    }
}
