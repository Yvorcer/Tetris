using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class BlockInfo
{
    public bool[,] blockformation;
    public Vector2 StartPosi;
    public Color blockcolor;

    public BlockInfo(int Index)
    {
        switch (Index)
        {
            case 0:
                Oblock();
                break;
            case 1:
                Sblock();
                break;
            case 2:
                Zblock();
                break;
            case 3:
                Tblock();
                break;
            case 4:
                Lblock();
                break;
            case 5:
                Jblock();
                break;
            case 6:
                Iblock();
                break;
        }
    }

    private void Oblock()
    {
        blockformation = new bool[,]
        {
            {false, false, false, false },
            {false, true, true, false },
            {false, true, true, false },
            {false, false, false, false },
        };
        StartPosi = new Vector2(3, -1);
        blockcolor = Color.Red;
    }

    private void Sblock()
    {
        blockformation = new bool[,]
        {
            {false, true, false, false },
            {false, true, true, false },
            {false, false, true, false },
            {false, false, false, false },
        };
        StartPosi = new Vector2(3, 0);
        blockcolor = Color.Orange;
    }

    private void Zblock()
    {
        blockformation = new bool[,]
        {
            {false, false, true, false },
            {false, true, true, false },
            {false, true, false, false },
            {false, false, false, false },
        };
        StartPosi = new Vector2(3, 0);
        blockcolor = Color.Yellow;
    }

    private void Tblock()
    {
        blockformation = new bool[,]
        {
            {false, true, false, false },
            {false, true, true, false },
            {false, true, false, false },
            {false, false, false, false },
        };
        StartPosi = new Vector2(3, 0);
        blockcolor = Color.Green;
    }

    private void Lblock()
    {
        blockformation = new bool[,]
        {
            {false, true, false, false },
            {false, true, false, false },
            {false, true, true, false },
            {false, false, false, false },
        };
        StartPosi = new Vector2(3, 0);
        blockcolor = Color.Blue;
    }

    private void Jblock()
    {
        blockformation = new bool[,]
        {
            {false, false, true, false },
            {false, false, true, false },
            {false, true, true, false },
            {false, false, false, false },
        };
        StartPosi = new Vector2(3, 0);
        blockcolor = Color.Purple;
    }

    private void Iblock()
    {
        blockformation = new bool[,]
        {
            {false, true, false, false },
            {false, true, false, false },
            {false, true, false, false },
            {false, true, false, false },
        };
        StartPosi = new Vector2(3, 0);
        blockcolor = Color.Magenta;
    }
}