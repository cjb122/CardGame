using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool playCard(Card c)
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
        for(int i = 0; i < 13; i++)
        {
            king.drawCard();
            jess.drawCard();
            keith.drawCard();
            player.drawCard();
        }
    }
}
