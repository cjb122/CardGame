using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Game g = GameObject.FindObjectOfType<Game>();
        if (g.getCurrentTurn() == 2)
        {
            GameActions.giveOutCard("Player", g.getKing(), g.getJess(), g.getKeith(), g.getPlayer());
            g.setText("You had to draw a card");
        }
        else
            GameActions.punishPlayer("Draw", g.displayText, g.getPlayer());
    }
}
