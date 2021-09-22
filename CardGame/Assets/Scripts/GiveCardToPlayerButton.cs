using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCardToPlayerButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Game g = GameObject.FindObjectOfType<Game>();

        if (g.getCurrentTurn() == 2 && g.getHaveToPickButton() && g.getPickPlayer())
        {
            g.setHaveToPickButton(false);
            g.setPickPlayer(false);

            Card c = g.getDiscard().getTopCard();

            if (c.getNumber() == "7")
            {
                if (g.players.captionText.text == "Jess" && g.getTurnDirection() == -1)
                {
                    GameActions.giveOutCard("Jess", g.getKing(), g.getJess(), g.getKeith(), g.getPlayer());
                    g.displayText.text = "You gave Jess the " + g.getDeck().peekCard().number 
                        + " of " + g.getDeck().peekCard().suit + "!";
                    g.instantiated = true;
                }
                else if (g.players.captionText.text == "Keith" && g.getTurnDirection() == 1)
                {
                    GameActions.giveOutCard("Keith", g.getKing(), g.getJess(), g.getKeith(), g.getPlayer());
                    g.displayText.text = "You gave Keith the " + g.getDeck().peekCard().number
                        + " of " + g.getDeck().peekCard().suit + "!";
                    g.instantiated = true;
                }
                else
                    GameActions.punishPlayer("Handout", g.displayText, g.getPlayer());

                c.setPlayedCorrectly(true);
                g.setHasPlayed(true);
            }
        }
        else
            GameActions.punishPlayer("Draw", g.displayText, g.getPlayer());
    }
}

