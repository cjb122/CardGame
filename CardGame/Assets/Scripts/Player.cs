using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    private List<Card> hand;
    private Deck deck;

    public Player(Deck d)
    {
        hand = new List<Card>();
        deck = d;
    }

    public List<Card> getHand()
    {
        return hand;
    }

    public void drawCard()
    {
        Card c = deck.drawCard();
        hand.Add(c);
    }

    public void discardCard(Card c)
    {
        
        hand.Remove(c);
    }
}
