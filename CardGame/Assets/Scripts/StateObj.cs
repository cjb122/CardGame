using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateObj : MonoBehaviour
{
    public Card cardPlayed;
    public Card topCard;
    public string postAction;
    public int reward;

    public StateObj() {}
    public StateObj(Card cardPlayed, Card topCard, string postAction, int reward)
    {
        this.cardPlayed = cardPlayed;
        this.topCard = topCard;
        this.postAction = postAction;
        this.reward = reward;
    }


    public Card getCardPlayed()
    {
        return cardPlayed;
    }

    public Card getTopCard()
    {
        return topCard;
    }

    public string getPostAction()
    {
        return postAction;
    }

    public int getReward()
    {
        return reward;
    }

    public bool stateMatch(Card cardPlayed, Card topCard, string postAction, int reward)
    {
        if (this.cardPlayed == cardPlayed &&
            this.topCard == topCard &&
            this.postAction == postAction &&
            this.reward == reward)
            return true;

        return false;
    }

    public int stateAproxMatch(Card cardPlayed, Card topCard, string postAction)
    {
        int total = 0;
        if (this.cardPlayed == cardPlayed)
            total += 1;
        else
            total -= 1;

        if (this.topCard == topCard)
            total += 1;
        else
            total -= 1;

        if (this.postAction == postAction) 
            total += 1;
        else
            total -= 1;

        return total;
    }
}
