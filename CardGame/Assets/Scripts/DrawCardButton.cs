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
            g.giveOutCard("Player");
            g.setText("You had to draw a card");
        }
        else
            g.punishPlayer("Draw");
    }
}
