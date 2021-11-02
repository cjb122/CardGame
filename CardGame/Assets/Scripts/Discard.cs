using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour
{
    private Stack<Card> discard;
    private Card topCard;
    private GameObject loc;
    
    public Discard(Card c)
    {
        discard = new Stack<Card>();
        discard.Push(c);
        topCard = c;
        loc = GameObject.FindGameObjectsWithTag("Discard")[0];
        topCard.flipCard();
        topCard.moveCard(loc.transform.position.x, loc.transform.position.y);
    }
        
    public Card getTopCard()
    {
        return topCard;
    }

    public void discardCard(Card c)
    {
        discard.Push(c);
        FindObjectOfType<AudioController>().playSound(0);
        if (c.faceDown)
            c.flipCard();
        
        c.moveCard(loc.transform.position.x, loc.transform.position.y);
        topCard.getSpriteRenderer().sortingLayerName = "Default";
        c.getSpriteRenderer().sortingLayerName = "TopCard";
        Debug.Log(c.getNumber() + " " + c.getSuit() + " " + c.getSpriteRenderer().sortingLayerName);
        topCard = c;
        if (topCard.rotated)
            topCard.rotateCardBack();
    }
}
