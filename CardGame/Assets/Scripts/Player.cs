using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Hand - Player's personal hand - each player has a seperate one
    // Deck - Deck every player draws from, shared across players
    private List<Card> hand;
    private static Deck deck;

    // Is dealer - determines if player is dealer, only King should have this set to True
    // Is mainPlayer = determines if the player is the human controlled player 
    // Turn - The player's turn
    private bool isDealer;
    private bool isMainPlayer;
    private int turn;

    private string playerName;
    private GameObject mat;
    private float matOffset;
    public Player(Deck d, bool i, bool p, int t, string n)
    {
        hand = new List<Card>();
        deck = d;
        isDealer = i;
        isMainPlayer = p;
        turn = t;
        playerName = n;
        mat = GameObject.FindGameObjectsWithTag(playerName)[0];
        matOffset = 0f;
    }

    public void drawCard()
    {
        Card c = deck.drawCard();
        if (!Object.Equals(c, null))
        {
            hand.Add(c);

            matOffset += c.getCardObj().GetComponent<SpriteRenderer>().bounds.size.x;

            if (playerName == "King" || playerName == "Player")
            {
                c.moveCard(mat.transform.position.x - (mat.GetComponent<SpriteRenderer>().bounds.size.x / 2)
                            + matOffset, mat.transform.position.y);
            }
            else
            {
                c.rotateCard();
                c.moveCard(mat.transform.position.x, mat.transform.position.y -
                    (mat.GetComponent<SpriteRenderer>().bounds.size.y / 2) + matOffset);
            }

            if (isMainPlayer)
                c.flipCard();
        }
    }

    public void discardCard(Card c)
    {
        hand.Remove(c);
    }

    // Getters and Setters
    public void setName(string n)
    {
        playerName = n;
    }

    public string getName()
    {
        return playerName;
    }

    public void setIsDealer(bool d)
    {
        isDealer = d;
    }
    public bool getIsDealer()
    {
        return isDealer;
    }

    public void setIsMainPlayer(bool i)
    {
        isMainPlayer = i;
    }

    public bool getIsMainPlayer()
    {
        return isMainPlayer;
    }

    public void setTurn(int t)
    {
        turn = t;
    }

    public int getTurn()
    {
        return turn;
    }

    public void setMat(GameObject m)
    {
        mat = m;
    }

    public GameObject getMat()
    {
        return mat;
    }
    public void setFloatOffset(float o)
    {
        matOffset = o;
    }

    public float getFloatOffset()
    {
        return matOffset;
    }
    public List<Card> getHand()
    {
        return hand;
    }
}
