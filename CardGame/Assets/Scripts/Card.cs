using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string number;
    private string suit;
    private string rule;
    private Sprite sprite;
    private Sprite topSprite;
    private CardSprites cardSpriteCollection;
    private SpriteRenderer spriteRenderer;
    private bool faceDown;
    GameObject cardObj;
    
    public Card(int n, int s)
    {
        //Initialize properties
        cardSpriteCollection = GameObject.FindObjectOfType<CardSprites>();
        this.number = setNumber(n);
        this.suit = setSuit(s);
        this.sprite = setSprite(n, s);
        this.rule = setRule(n);
        this.topSprite = cardSpriteCollection.getTop();
        this.faceDown = true;

        //Temporary for testing measures
        createCard();
    }

    private string setSuit(int s)
    {
        switch (s)
        {
            case 1:
                return "Spades";
            case 2:
                return "Heart";
            case 3:
                return "Diamond";
            case 4:
                return "Clubs";
        }

        return "Error";
    }

    private string setNumber(int n)
    {
        switch (n)
        {
            case 1:
                return "Ace";
            case 2:
                return "2";
            case 3:
                return "3";
            case 4:
                return "4";
            case 5:
                return "5";
            case 6:
                return "6";
            case 7:
                return "7";
            case 8:
                return "8";
            case 9:
                return "9";
            case 10:
                return "10";
            case 11:
                return "Jack";
            case 12:
                return "Queen";
            case 13:
                return "King";
        }


        return "Error";
    }

    // -1 due to the array being instanced at 0
    private Sprite setSprite(int n, int s)
    {
        if (s == 1)
            return cardSpriteCollection.getSpades(n-1);
        else if (s == 2)
            return cardSpriteCollection.getHearts(n-1);
        else if (s == 3)
            return cardSpriteCollection.getDiamonds(n-1);
        else if (s == 4)
            return cardSpriteCollection.getClubs(n-1);

        return null;
    }

    private string setRule(int n)
    {
        switch (n)
        {
            case 1:
                return "skip";
            case 7:
                return "draw";
            case 8:
                return "switch";
            case 11:
                return "suit";
            default:
                return "";
        }
    }

    public string getSuit()
    {
        return suit;
    }

    public string getNumber()
    {
        return number;
    }

    public string getRule()
    {
        return rule;
    }

    public void createCard()
    {
        cardObj = new GameObject(this.number + " " + this.suit);
        spriteRenderer = cardObj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = this.topSprite;
    }

    public void flipCard()
    {
        if (faceDown)
        {
            spriteRenderer.sprite = sprite;
            faceDown = false;
        }
        else
        {
            spriteRenderer.sprite = topSprite;
            faceDown = true;
        }
    }

    public void moveCard(float xPos, float yPos)
    {
        cardObj.transform.position = new Vector2(xPos, yPos);
    }

    public void rotateCard()
    {
        cardObj.transform.eulerAngles = Vector3.forward * 90;
    }

    public GameObject getCardObj()
    {
        return cardObj;
    }

    public void setCardObj(GameObject g)
    {
        cardObj = g;
    }
}
