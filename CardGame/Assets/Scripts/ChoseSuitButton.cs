using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseSuitButton : MonoBehaviour
{

    private void OnMouseDown()
    {
        Game g = GameObject.FindObjectOfType<Game>();
        if (g.getCurrentTurn() == 2 && g.getHaveToPickButton() && g.getPickSuit())
        {
            g.setHaveToPickButton(false);
            g.setPickSuit(false);

            Card c = g.getDiscard().getTopCard();
            
            if (c.getNumber() == "Jack")
            {
                if (g.suitDropdown.captionText.text == "Clubs")
                {
                    g.getDiscard().discardCard(new Card(14, 4));
                    g.instantiated = true;
                }
                else if (g.suitDropdown.captionText.text == "Diamonds")
                {
                    g.getDiscard().discardCard(new Card(14, 3));
                    g.instantiated = true;
                }
                else if (g.suitDropdown.captionText.text == "Hearts")
                {
                    g.getDiscard().discardCard(new Card(14, 2));
                    g.instantiated = true;
                }
                else if (g.suitDropdown.captionText.text == "Spades")
                {
                    g.getDiscard().discardCard(new Card(14, 1));
                    g.instantiated = true;
                }

                c.setPlayedCorrectly(true);
                g.setHasPlayed(true);
            }
        }
        else
            GameActions.punishPlayer("Draw", g.displayText, g.getPlayer());
    }
}
