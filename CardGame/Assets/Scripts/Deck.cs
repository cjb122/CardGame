using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck 
{
    private Stack<Card> deck;

    public Deck()
    {
        deck = new Stack<Card>(); 
        initializeDeck();
        int i = 0;
        
        //DEBUG
        /*
        foreach(Card d in deck)
        {
            Debug.Log(i + " Number: " + d.getNumber() + " Suit: " + d.getSuit() + " Rule: " + d.getRule());
            i++;
        }
        */    
    }


    private void initializeDeck()
    {
        List<Card> temp = new List<Card>();
        for(int i = 1; i <= 13; i++)
        {
            for(int j = 1; j <= 4; j++)
            {
                temp.Add(new Card(i, j));
            }
        }

        deck = shuffle(temp);
    }

    private Stack<Card> shuffle(List<Card> d)
    {
        int tempCard;
        Stack<Card> tempStack = new Stack<Card>();
        for(int i = 0; i < 52; i++)
        {
            tempCard = Random.Range(0, 52 - i);
            tempStack.Push(d[tempCard]);
            d.RemoveAt(tempCard);
        }
        return tempStack;
    }

    public Card drawCard()
    {
        if(deck.Count != 0)
            return deck.Pop();
        return null;
    }
}
