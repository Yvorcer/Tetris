using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Score
{
    private int scoreamm, nexLevel;
    public TetrisGrid grid;
    public SpriteFont font;

    public Score()
    {
        scoreamm = 0;
        nexLevel = 100;
    }

    private void AddScore()
    {
        if (grid.rowrem)
        {
            while(grid.counter > 0)
            {
                if(grid.counter == 1)
                {
                    scoreamm += grid.level * 40;
                    grid.counter = 0;
                }
                if (grid.counter == 2)
                {
                    scoreamm += grid.level * 100;
                    grid.counter = 0;
                }
                if (grid.counter == 3)
                {
                    scoreamm += grid.level * 300;
                    grid.counter = 0;
                }
                if (grid.counter >= 4)
                {
                    scoreamm += grid.level * 12000;
                    grid.counter -= 4;
                }
            }
            grid.rowrem = false;

        }
    }

    private void LevelUp()
    {
        ///Hier in de If moet net geluid voor een LVL-up als dat word toegevoegd
        if(scoreamm >= nexLevel)
        {
            grid.level += 1;
            nexLevel += 3 * nexLevel;
        }
    }

    public void Update()
    {
        AddScore();
        LevelUp();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "Score: " + scoreamm, new Vector2(390, 180), Color.Black);
        spriteBatch.DrawString(font, "Level: " + grid.level, new Vector2(390, 210), Color.Black);
        spriteBatch.DrawString(font, "Next Level At: " + nexLevel, new Vector2(390, 240), Color.Black);
    }
}
