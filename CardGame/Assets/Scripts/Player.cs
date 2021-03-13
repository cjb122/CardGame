using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> hand;
    private Deck deck;
    private bool isDealer;
    private int turn;
    public Player(Deck d, bool i, int t)
    {
        hand = new List<Card>();
        deck = d;
        isDealer = i;
        turn = t;
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
