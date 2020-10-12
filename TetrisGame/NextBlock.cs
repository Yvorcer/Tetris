using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class NextBlock
{
    private bool[,] blockmat;
    private Random ranblock = new Random();
    public BlockInfo nexblock;
    private Vector2 position;
    private Texture2D block;
    private Color blockcolor;

    public NextBlock()
    {
        block = TetrisGame.ContentManager.Load<Texture2D>("block");
        int index = ranblock.Next(7);
        nexblock = new BlockInfo(index);
        position = new Vector2(13, 2);
        blockmat = nexblock.blockformation;
        blockcolor = nexblock.blockcolor;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int L = 0; L < 4; L++)
        {
            for (int W = 0; W < 4; W++)
            {
                if (blockmat[L, W])
                    spriteBatch.Draw(block, new Vector2((position.X + W) * block.Width, (position.Y + L) * block.Height), blockcolor);
                else
                    spriteBatch.Draw(block, new Vector2((position.X + W) * block.Width, (position.Y + L) * block.Height), Color.White);
            }
        }
    }
}