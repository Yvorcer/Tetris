using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Hold
{
    private bool[,] blockmat;
    private Vector2 position, blockposi;
    private Texture2D block;
    private Color blockcolor;
    public bool holding;
    public SpriteFont font;

    public Hold()
    {
        block = TetrisGame.ContentManager.Load<Texture2D>("block");
        position = new Vector2(13, 10);
        blockmat = new bool[,]
        {
            {false, false, false, false },
            {false, false, false, false },
            {false, false, false, false },
            {false, false, false, false },
        };
        holding = false;
    }

    public void HolBlock(Block block)
    {
        bool[,] tempblockmat = block.blockmat;
        Vector2 tempblockposi = block.startposi;
        Color tempblockcolor = block.blockcolor;

        if (holding)
        {
            block.blockmat = blockmat;
            block.position = blockposi;
            block.startposi = blockposi;
            block.blockcolor = blockcolor;
        }

        blockmat = tempblockmat;
        blockposi = tempblockposi;
        blockcolor = tempblockcolor;
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
        spriteBatch.DrawString(font, "Held Block", new Vector2(390, 420), Color.Black);
        spriteBatch.DrawString(font, "Next Block", new Vector2(390, 30), Color.Black);
    }
}

