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
        matOffset = Constants.CARDSIZE;
    }

    public bool drawCard()
    {
        Card c = deck.drawCard();
        if (!Object.Equals(c, null))
        {
            hand.Add(c);
            FindObjectOfType<AudioController>().playSound(0);
            if (playerName == "King" || playerName == "Player")
            {
                c.moveCard(mat.transform.position.x - (Constants.XMAT / 2) + matOffset, mat.transform.position.y);
                if (matOffset > Constants.XMAT - (Constants.CARDSIZE * 4))
                    resizeHand();
            }
            else
            {
                c.rotateCard();
                c.moveCard(mat.transform.position.x, mat.transform.position.y - (Constants.YMAT / 2) + matOffset);
                if (matOffset > Constants.YMAT - (Constants.CARDSIZE * 4))
                    resizeHandY();
            }

            //if (isMainPlayer)
            c.flipCard();

            matOffset += Constants.CARDSIZE;
            return true;
        }
        else
            return false;
        
    }

    public void discardCard(Card c)
    {
        hand.RemoveAll(r => r.getSuit() + " " + r.getNumber() == c.getSuit() + " " + c.getNumber());
        if (hand.Count * Constants.CARDSIZE > Constants.XMAT - (Constants.CARDSIZE * 4))
            resizeHand();
        else
            reorganizeHand(Constants.CARDSIZE, false);
    }

    public void reorganizeHand(float offset, bool layered)
    {
        matOffset = Constants.CARDSIZE;
        float index = 1;
        foreach (Card c in this.getHand().ToArray())
        {
            if (playerName == "King" || playerName == "Player")
            {
                c.moveCard(mat.transform.position.x - (Constants.XMAT / 2)
                            + matOffset, mat.transform.position.y);
                if (layered)
                    c.setLayer(index);
            }
            else
            {
                c.rotateCard();
                c.moveCard(mat.transform.position.x, mat.transform.position.y - (Constants.YMAT / 2) + matOffset);
                if (layered)
                    c.setLayer(index);
            }
            
            matOffset += offset;
            index++;
        }
    }

    public void resizeHand()
    {
        int count = 0;
        float tempOff = (Constants.XMAT - (Constants.CARDSIZE * count)) / hand.Count;
        while(tempOff * hand.Count >= Constants.XMAT - (Constants.CARDSIZE*2))
        {
            tempOff -= 0.001f;
            count++;
            
            //Infintie Loop Failsafe
            if (count > 600000)
                break;
        }
           
        if (tempOff > 1)
            reorganizeHand(Constants.CARDSIZE, false);
        else
            reorganizeHand(tempOff, true);
    }
    public void resizeHandY()
    {
        int count = 0;
        float tempOff = (Constants.YMAT - (Constants.CARDSIZE * count)) / hand.Count;
        while (tempOff * hand.Count >= Constants.YMAT - (Constants.CARDSIZE * 2))
        {
            tempOff -= 0.001f;
            count++;

            //Infintie Loop Failsafe
            if (count > 600000)
                break;
        }

        if (tempOff > 1)
            reorganizeHand(Constants.CARDSIZE, false);
        else
            reorganizeHand(tempOff, true);
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

    public Card getRandomCard()
    {
        int cardToReturn = Random.Range(0, hand.Count);

        return hand[cardToReturn];

    }
}
