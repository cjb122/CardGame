using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Player object for each individual player
    private Player king;
    private Player jess;
    private Player keith;
    private Player player;

    // Card objects
    private Deck deck;
    private Discard discard;
    private Card topCard;

    // Buttons
    public Button drawCard;

    /*
     * Turn order:
     * Jess - 1
     * Player - 2
     * Keith - 3
     * King - 4
     */
    private int currentTurn;


    // Start is called before the first frame update
    void Start()
    {
        deck = new Deck();
        king = new Player(deck, true, false, 4, "King");
        jess = new Player(deck, false, false, 1, "Jess");
        player = new Player(deck, false, true, 2, "Player");
        keith = new Player(deck, false, false, 3, "Keith");
        deal();
        discard = new Discard(deck.drawCard());
        currentTurn = 1;
    }

    public bool playCard(Card c, string player)
    {
        Card top = discard.getTopCard();
        if (top.getSuit() == c.getSuit() || top.getNumber() == c.getNumber())
        {
            discard.discardCard(c);
            return true;
        }
        else
            return false;
    }

    private void deal()
    {
        for(int i = 0; i < 3; i++)
        {
            king.drawCard();
            jess.drawCard();
            keith.drawCard();
            player.drawCard();
        }
    }

    public void giveOutCard(string p)
    {
        if (p == king.getName())
            king.drawCard();
        else if (p == jess.getName())
            jess.drawCard();
        else if (p == keith.getName())
            keith.drawCard();
        else if (p == player.getName())
            player.drawCard();
    }

    public void playerCardButton()
    {
        if(Input.GetMouseButtonDown(0))
        {
            giveOutCard("Player");
        }
    }
}
