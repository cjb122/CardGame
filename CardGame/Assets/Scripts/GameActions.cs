using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameActions
{
    //This is a perfect AI... it knows too much
    public static bool npcCheckCard(Player p, Text displayText, Discard discard, int turnDirection, int currentTurn)
    {
        bool hasPlayed = false;
        foreach (Card c in p.getHand().ToArray())
        {
            if (playCard(c, p, currentTurn, turnDirection, discard))
            {
                discard.discardCard(c);
                p.discardCard(c);
                c.setPlayedCorrectly(true);
                hasPlayed = true;
                displayText.text = p.getName() + " played the " + c.getNumber() + " of " + c.getSuit() + "s!";
                break;
            }
        }

        if (!hasPlayed)
        {
            p.drawCard();
            displayText.text = p.getName() + " had to draw a card!";
            hasPlayed = true;
        }

        return hasPlayed;
    }

    //8s change the direction of the turns
    public static int setTurn(int currentTurn, int turnDirection)
    {
        currentTurn += turnDirection;
        if (turnDirection == 1 && currentTurn >= 5)
            currentTurn = 1;
        else if (turnDirection == -1 && currentTurn <= 0)
            currentTurn = 4;

        return currentTurn;
    }

    //Called if players exceed 10 seconds on their turn
    public static void forceDraw(int currentTurn, Player king, Player jess, Player keith, Player player, Text displayText)
    {
        if (currentTurn == king.getTurn())
            punishNPC("Time", king, displayText);
        else if (currentTurn == jess.getTurn())
            punishNPC("Time", jess, displayText);
        else if (currentTurn == keith.getTurn())
            punishNPC("Time", keith, displayText);
        else if (currentTurn == player.getTurn())
            punishPlayer("Time", displayText, player);
    }

    public static bool checkPlayerCard(Player player, int currentTurn, Discard discard, Text displayText, int turnDirection)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            //Check that the card clicked is in the player's hand
            foreach (Card c in player.getHand().ToArray())
            {
                if (hit.collider.gameObject == c.getCardObj())
                {
                    //Make sure it is the player's turn
                    if (currentTurn == player.getTurn())
                    {
                        //Make sure the player can actually play the card
                        if (playCard(c, player, currentTurn, turnDirection, discard))
                        {
                            discard.discardCard(c);
                            player.discardCard(c);
                            displayText.text = "You played the " + c.getNumber() + " of " + c.getSuit() + "s!";

                            //Return false if the player still needs to pick a button
                            if (c.getNumber() == "Jack" || c.getNumber() == "7")
                                return false;
                            else
                                return true;
                        }
                        //Player tried to play an incorrect card
                        else
                        {
                            punishPlayer("Wrong Card", displayText, player);
                            return false;
                        }
                    }
                    //Player tried to play a card when it wasn't their turn
                    else
                    {
                        punishPlayer("Play", displayText, player);
                        return false;
                    }

                }
            }
        }

        return false;
    }
    public static bool playCard(Card c, Player player, int currentTurn, int turnDirection, Discard discard)
    {
        Game g = GameObject.FindObjectOfType<Game>();
        Card top = discard.getTopCard();
        if (top.getSuit() == c.getSuit() || top.getNumber() == c.getNumber())
        {
            if (c.getNumber() == "8")
                g.setTurnDirection();
            else if (c.getNumber() == "Ace")
            {
                currentTurn = setTurn(currentTurn, turnDirection);
                g.setCurrentTurn(currentTurn);
            }
            else if(c.getNumber() == "Jack")
            {
                if (g.getCurrentTurn() == 2)                
                {
                    g.setHaveToPickButton(true);
                    g.setPickSuit(true);
                    g.setGameTimer(0);
                }
            }
            else if (c.getNumber() == "7")
            {
                if (g.getCurrentTurn() == 2)
                {
                    g.setHaveToPickButton(true);
                    g.setPickPlayer(true);
                    g.setGameTimer(0);
                }
            }
            return true;
        }
        else
            return false;
    }
    
    public static void deal(Player king, Player jess, Player keith, Player player)
    {
        for (int i = 0; i < 6; i++)
        {
            king.drawCard();
            jess.drawCard();
            keith.drawCard();
            player.drawCard();
        }
    }


    public static bool giveOutCard(string p, Player king, Player jess, Player keith, Player player)
    {
        if (p == king.getName())
            king.drawCard();
        else if (p == jess.getName())
            jess.drawCard();
        else if (p == keith.getName())
            keith.drawCard();
        else if (p == player.getName())
            player.drawCard();

        return true;
    }

    public static Player getPlayer(string p, Player king, Player jess, Player keith, Player player)
    {
        if (p == king.getName())
            return king;
        else if (p == jess.getName())
            return jess;
        else if (p == keith.getName())
            return keith;
        else if (p == player.getName())
            return player;
        return null;
    }

    // Decision tree of what hints to give a player if they break a rule
    public static void punishPlayer(string rule, Text displayText, Player player)
    {
        if (rule == "Draw")
        {
            displayText.text = "It's not your turn!";
            player.drawCard();
            player.drawCard();
        }
        else if (rule == "Play")
        {
            displayText.text = "It's not your turn!";
            player.drawCard();
        }
        else if (rule == "Wrong Card")
        {
            displayText.text = "That card won't work!";
            player.drawCard();
            GameObject.FindObjectOfType<Game>().setHasPlayed(true);
        }
        else if (rule == "Time")
        {
            displayText.text = "You took too long!";
            player.drawCard();
        }
        else if (rule == "Handout")
        {
            displayText.text = "Wrong player!";
            player.drawCard();
        }
    }

    // Decision tree of what hints to give a player if an npc breaks a rule
    public static void punishNPC(string rule, Player p, Text displayText)
    {
        if (rule == "Draw")
        {
            displayText.text = p.getName() + " tried to play when it wasn't their turn!";
            p.drawCard();
            p.drawCard();
        }
        else if (rule == "Play")
        {
            displayText.text = p.getName() + " tried to play when it wasn't their turn!";
            p.drawCard();
        }
        else if (rule == "Wrong Card")
        {
            displayText.text = p.getName() + " tried to play an incorrect card!";
            p.drawCard();
            GameObject.FindObjectOfType<Game>().setHasPlayed(true);
        }
        else if (rule == "Time")
        {
            displayText.text = p.getName() + " took too long!";
            p.drawCard();
        }
    }

    // When a Jack is played, the npc can change suit
    // Currently, they pick the suit they have the most of in their hand
    public static void npcChangeSuit(Player p, bool instantiated, Discard discard)
    {
        string suit = "";
        int largest = 0;
        int spadeCount = 0;
        int diamondCount = 0;
        int heartCount = 0;
        int clubCount = 0;

        foreach (Card c in p.getHand().ToArray())
        {
            if (c.getSuit() == "Spade")
            {
                spadeCount++;
                if (spadeCount > largest)
                {
                    suit = "Spade";
                    largest = spadeCount;
                }
            }
            else if (c.getSuit() == "Heart")
            {
                heartCount++;
                if (heartCount > largest)
                {
                    suit = "Heart";
                    largest = heartCount;
                }
            }
            else if (c.getSuit() == "Diamond")
            {
                diamondCount++;
                if (diamondCount > largest)
                {
                    suit = "Diamond";
                    largest = diamondCount;
                }
            }
            else if (c.getSuit() == "Club")
            {
                clubCount++;
                if (clubCount > largest)
                {
                    suit = "Club";
                    largest = clubCount;
                }
            }
        }

        if (suit == "Club" && !instantiated)
        {
            discard.discardCard(new Card(14, 4));
            instantiated = true;
        }
        else if (suit == "Diamond" && !instantiated)
        {
            discard.discardCard(new Card(14, 3));
            instantiated = true;
        }
        else if (suit == "Heart" && !instantiated)
        {
            discard.discardCard(new Card(14, 2));
            instantiated = true;
        }
        else if (suit == "Spade" && !instantiated)
        {
            discard.discardCard(new Card(14, 1));
            instantiated = true;
        }
    }
}
