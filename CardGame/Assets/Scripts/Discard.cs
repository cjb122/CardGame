using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour
{
    private Stack<Card> discard;
    private Card topCard;
    
    public Discard(Card c)
    {
        discard = new Stack<Card>();
        discard.Push(c);
        topCard = c;
    }
        
    public Card getTopCard()
    {
        return topCard;
    }

    public void discardCard(Card c)
    {
        discard.Push(c);
        topCard = c;
    }
}
