using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Player object for each individual player
    private Player king;
    private Player jess;
    private Player keith;
    private Player player;

    // Card objects
    private Deck deck;
    private Discard discard;
    private Card topCard;

    /*
     * Turn order:
     * Jess - 1
     * Player - 2
     * Keith - 3
     * King - 4
     */
    private int currentTurn;
    private bool takingTurn;

    private float gameTimer;
    private int turnDirection;
    private bool hasPlayed;

    public Text displayText;

    // Start is called before the first frame update
    void Start()
    {
        deck = new Deck();
        king = new Player(deck, true, false, 4, "King");
        jess = new Player(deck, false, false, 1, "Jess");
        player = new Player(deck, false, true, 2, "Player");
        keith = new Player(deck, false, false, 3, "Keith");
        deal();
        discard = new Discard(deck.drawCard());
        currentTurn = 1;
        gameTimer = 0f;
        turnDirection = 1;
        hasPlayed = false;
        takingTurn = true;
    }

    void Update()
    {
       if (Input.GetMouseButtonDown(0))
       {
            if (checkPlayerCard())
                hasPlayed = true;
       }
       if (takingTurn)
       {
            gameTimer += Time.deltaTime;

            //NPCs take their turn
            if (currentTurn == king.getTurn() && !hasPlayed)
                hasPlayed = npcCheckCard(king);
            if (currentTurn == jess.getTurn() && !hasPlayed)
                hasPlayed = npcCheckCard(jess);
            if (currentTurn == keith.getTurn() && !hasPlayed)
                hasPlayed = npcCheckCard(keith);

            //Each player has 10 seconds to take their turn.
            //If they don't, they will be forced to draw a card and
            //their turn will be skipped
            if (gameTimer >= 10 && !hasPlayed)
            {
                gameTimer = 0;
                forceDraw();
                setTurn();
                hasPlayed = false;
                if(currentTurn != player.getTurn())
                {
                    takingTurn = false;
                    StartCoroutine(Wait());
                }
            }
            //If the current player has played a correct card or drawn a card, 
            //it moved to the next player's turn
            else if (hasPlayed)
            {
                gameTimer = 0;
                setTurn();
                hasPlayed = false;
                if (currentTurn != player.getTurn())
                {
                    takingTurn = false;
                    StartCoroutine(Wait());
                }
            }
        }
    }

    //This is a perfect AI... it knows too much
    public bool npcCheckCard(Player p)
    {
        bool hp = false;
        foreach (Card c in p.getHand().ToArray())
        {
            if (playCard(c, p.getName()))
            {
                discard.discardCard(c);
                p.discardCard(c);
                hp = true;
                displayText.text = p.getName() + " played the " + c.getNumber() + " of " + c.getSuit() + "s!"; 
                break;
            }
        }

        if (!hp)
        {
            p.drawCard();
            displayText.text = p.getName() + " had to draw a card!";
            hp = true;
        }

        return hp;
    }

    //8s change the direction of the turns
    public void setTurn()
    {
        currentTurn += turnDirection;
        if (turnDirection == 1 && currentTurn == 5)
            currentTurn = 1;
        else if (turnDirection == -1 && currentTurn == 0)
            currentTurn = 4;

        Debug.Log(currentTurn);
    }

    //Called if players exceed 10 seconds on their turn
    public void forceDraw()
    {
        if (currentTurn == king.getTurn())
            punishNPC("Time", king);
        else if (currentTurn == jess.getTurn())
            punishNPC("Time", jess);
        else if (currentTurn == keith.getTurn())
            punishNPC("Time", keith);
        else if (currentTurn == player.getTurn())
            punishPlayer("Time");
    }

    public bool checkPlayerCard()
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
                        if (playCard(c, "Player"))
                        {
                            discard.discardCard(c);
                            player.discardCard(c);
                            displayText.text = "You played the " + c.getNumber() + " of " + c.getSuit() + "s!";
                            return true;
                        }
                        //Player tried to play an incorrect card
                        else
                        {
                            punishPlayer("Wrong Card");
                            return false;
                        }                            
                    }
                    //Player tried to play a card when it wasn't their turn
                    else
                    {
                        punishPlayer("Play");
                        return false;
                    }
                        
                }
            }
        }

        return false;
    }

    public bool playCard(Card c, string player)
    {
        Card top = discard.getTopCard();
        if (top.getSuit() == c.getSuit() || top.getNumber() == c.getNumber())
        {
            if (c.getNumber() == "8")
                turnDirection *= -1;
            else if (c.getNumber() == "Ace")
                setTurn();
            return true;
        }
        else
            return false;
    }

    private void deal()
    {
        for(int i = 0; i < 3; i++)
        {
            king.drawCard();
            jess.drawCard();
            keith.drawCard();
            player.drawCard();
        }
    }

    public void giveOutCard(string p)
    {
        if (p == king.getName())
            king.drawCard();
        else if (p == jess.getName())
            jess.drawCard();
        else if (p == keith.getName())
            keith.drawCard();
        else if (p == player.getName())
            player.drawCard();

        hasPlayed = true;
    }

    public Player getPlayer(string p)
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        takingTurn = true;
    }

    public void punishPlayer(string rule)
    {
        if (rule == "Draw")
        {
            displayText.text = "It's not your turn!";
            player.drawCard();
            player.drawCard();
        }
        else if(rule == "Play")
        {
            displayText.text = "It's not your turn!";
            player.drawCard();
        }
        else if (rule == "Wrong Card")
        {
            displayText.text = "That card won't work!";
            player.drawCard();
            hasPlayed = true;
        }
        else if (rule == "Time")
        {
            displayText.text = "You took too long!";
            player.drawCard();
        }
    }

    public void punishNPC(string rule, Player p)
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
            hasPlayed = true;
        }
        else if (rule == "Time")
        {
            displayText.text = p.getName() + " took too long!";
            p.drawCard();
        }
    }

    public int getCurrentTurn()
    {
        return currentTurn;
    }

    public void setText(string s)
    {
        displayText.text = s;
    }
}
