using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Player king;
    private Player jess;
    private Player keith;
    private Player player;
    private Deck deck;
    private Discard discard;
    private Card topCard;
    private int currentTurn;


    // Start is called before the first frame update
    void Start()
    {
        deck = new Deck();
        king = new Player(deck, true, 4);
        jess = new Player(deck, false, 1);
        player = new Player(deck, false, 2);
        keith = new Player(deck, false, 3);
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
        for(int i = 0; i < 3; i++)
        {
            king.drawCard();
            jess.drawCard();
            keith.drawCard();
            player.drawCard();
        }
    }
}
