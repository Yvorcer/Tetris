using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Block
{
    private Texture2D blockimg;
    public bool[,] blockmat;
    public Vector2 position;
    private Random ranblock = new Random();
    public Color blockcolor;
    public BlockInfo choblock;
    public TetrisGrid Grid;
    private InputHelper input;
    public int IsA = 0;
    public float time = 0, speedup;

    public Block()
    {
        blockimg = TetrisGame.ContentManager.Load<Texture2D>("block");
        int Index = ranblock.Next(7);
        choblock = new BlockInfo(Index);
        blockmat = choblock.blockformation;
        position = choblock.StartPosi;
        blockcolor = choblock.blockcolor;
        input = new InputHelper();
    }

    private void Movement()
    {
        time += 1 * speedup;
        if(time > 60)
        {
            position.Y++;
            time = 0;
            speedup += 0.006f;
            IsA = 0;
        }
        if (input.KeyPressed(Keys.A))
        {
            position.X--;
            IsA = 1;
        }
        if (input.KeyPressed(Keys.D))
        {
            position.X++;
            IsA = 2;
        }
        if (input.KeyPressed(Keys.S))
        {
            position.Y++;
            IsA = 0;
        }
        if (input.KeyPressed(Keys.W))
        {
            var roGrid = blockmat;
            blockmat = new bool[,]
            {
                {roGrid[3,0], roGrid[2,0], roGrid[1,0], roGrid[0,0] },
                {roGrid[3,1], roGrid[2,1], roGrid[1,1], roGrid[0,1] },
                {roGrid[3,2], roGrid[2,2], roGrid[1,2], roGrid[0,2] },
                {roGrid[3,3], roGrid[2,3], roGrid[1,3], roGrid[0,3] },
            };
            IsA = 0;
        }
    }

    private void Colission()
    {
        for (int L = 0; L < 4; L++)
        {
            for (int W = 0; W < 4; W++)
            {
                if(blockmat[L, W] && position.X + W > 9)
                {
                    position.X--;
                }
                if (blockmat[L, W] && position.X + W < 0)
                {
                    position.X++;
                }
            }
        }
    }

    public void Newblock(BlockInfo blockInfo)
    {
        blockmat = blockInfo.blockformation;
        position = blockInfo.StartPosi;
        blockcolor = blockInfo.blockcolor;
    }

    public void Update(GameTime gameTime)
    {
        Movement();
        Colission();
        input.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int L = 0; L < 4; L++)
        {
            for (int W = 0; W < 4; W++)
            {
                if (blockmat[L, W])
                    spriteBatch.Draw(blockimg, new Vector2((position.X + W) * blockimg.Width, (position.Y + L) * blockimg.Height), blockcolor);
            }
        }
    }
}