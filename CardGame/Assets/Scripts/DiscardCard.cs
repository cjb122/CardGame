using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCard : MonoBehaviour
{
    //private Player currentPlayer;
    //private Card card;
    public Player currentPlayer;
    public Card card;
    private void OnMouseDown()
    {
        if (currentPlayer.getName() == "Player")
            currentPlayer.discardCard(card);
    }

    public Player getCurrentPlayer()
    {
        return currentPlayer;
    }

    public void setCurrentPlayer(Player p)
    {
        Debug.Log(p.getName());
        currentPlayer = p;
    }

    public Card getCard()
    {
        return card;
    }

    public void setCard(Card c)
    {
        card = c;
    }
}
