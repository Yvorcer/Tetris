using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

class Block 
{
    private Texture2D blockimg;
    public bool[,] blockmat;
    public Vector2 position, startposi;
    private Random ranblock = new Random();
    public Color blockcolor;
    public BlockInfo choblock;
    public TetrisGrid Grid;
    private InputHelper input;
    public int IsA = 0;
    public float time = 0, speedup = 1, down = 0;

    private SoundEffect rotationSound;
    private SoundEffect inputSound;
    private SoundEffect brickHitSound;

    public Block()
    {
        blockimg = TetrisGame.ContentManager.Load<Texture2D>("block");

        // Load in Sounds
        rotationSound = TetrisGame.ContentManager.Load<SoundEffect>("Sounds/Rotation");
        inputSound = TetrisGame.ContentManager.Load<SoundEffect>("Sounds/inputSound");
        brickHitSound = TetrisGame.ContentManager.Load<SoundEffect>("Sounds/BrickHit");


        int Index = ranblock.Next(7);
        choblock = new BlockInfo(Index);
        blockmat = choblock.blockformation;
        position = choblock.StartPosi;
        startposi = choblock.StartPosi;
        blockcolor = choblock.blockcolor;
        input = new InputHelper();
    }

    private void Movement()
    {
        time += 1 + (Grid.level * 2 / 10);
        if (time > 60)
        {
            position.Y++;
            time = 0;
            IsA = 0;
        }
        if (input.KeyPressed(Keys.A))
        {
            position.X--;
            IsA = 1;
            inputSound.Play(volume: 0.1f, pitch: 0.0f, pan: 0.0f);
        }
        if (input.KeyPressed(Keys.D))
        {
            position.X++;
            IsA = 2;
            inputSound.Play(volume: 0.1f, pitch: 0.0f, pan: 0.0f);
        }
        if (input.KeyDown(Keys.S))
        {
            down += 1;
            if(down > 10)
            {
                position.Y++;
                IsA = 0;
                inputSound.Play(volume: 0.1f, pitch: 0.0f, pan: 0.0f);
                down = 0;
            }
        }
    }

    private void Rotation()
    { 
        if (input.KeyPressed(Keys.W))
        {
            ///this is a rotation, place sound here
            rotationSound.Play();
            var roGrid = blockmat;
            blockmat = new bool[,]
            {
                {roGrid[3,0], roGrid[2,0], roGrid[1,0], roGrid[0,0] },
                {roGrid[3,1], roGrid[2,1], roGrid[1,1], roGrid[0,1] },
                {roGrid[3,2], roGrid[2,2], roGrid[1,2], roGrid[0,2] },
                {roGrid[3,3], roGrid[2,3], roGrid[1,3], roGrid[0,3] },
            };
            IsA = 3;
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
                    brickHitSound.Play();
                }
                if (blockmat[L, W] && position.X + W < 0)
                {
                    position.X++;
                    brickHitSound.Play();
                }                
            }            
        }
    }

    internal void Update(GameTime gameTime, object effect)
    {
        throw new NotImplementedException();
    }

    public void Newblock(BlockInfo blockInfo)
    {
        blockmat = blockInfo.blockformation;
        position = blockInfo.StartPosi;
        startposi = blockInfo.StartPosi;
        blockcolor = blockInfo.blockcolor;
    }

    public void Update(GameTime gameTime)
    {
        Rotation();
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