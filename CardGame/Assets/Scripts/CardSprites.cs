using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprites : MonoBehaviour
{
    public Sprite[] spades;
    public Sprite[] hearts;
    public Sprite[] diamonds;
    public Sprite[] clubs;
    public Sprite top;

    public Sprite getSpades(int num)
    {
        if (num > 13 || num < 0)
        {
            return null;
        }
        return spades[num];
    }

    public Sprite getHearts(int num)
    {
        if (num > 13 || num < 0)
        {
            return null;
        }
        return hearts[num];
    }

    public Sprite getDiamonds(int num)
    {
        if (num > 13 || num < 0)
        {
            return null;
        }
        return diamonds[num];
    }

    public Sprite getClubs(int num)
    {
        if (num > 13 || num < 0)
        {
            return null;
        }
        return clubs[num];
    }

    public Sprite getTop()
    {
        return top;
    }
}
