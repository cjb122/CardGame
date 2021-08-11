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

    // Required for button rules
    public bool haveToPickButton;
    public bool pickPlayer;
    public bool pickSuit;

    // Card rule booleans
    private bool needToHandOutCard;
    private bool handedOutCard;
    private bool needToChangeSuit;
    private bool changedSuit;

    public Text displayText;
    public Dropdown suit;
    public Dropdown players;

    // Card prefabs
    public bool instantiated;
    public Card club;
    public Card diamond;
    public Card heart;
    public Card spade;

    public Dropdown suitDropdown;

    // Start is called before the first frame update
    void Start()
    {
        deck = new Deck();
        king = new Player(deck, true, false, 4, "King");
        jess = new Player(deck, false, false, 1, "Jess");
        player = new Player(deck, false, true, 2, "Player");
        keith = new Player(deck, false, false, 3, "Keith");
        GameActions.deal(king, jess, keith, player);
        discard = new Discard(deck.drawCard());
        currentTurn = 1;
        gameTimer = 0f;
        turnDirection = 1;
        hasPlayed = false;
        takingTurn = true;
        instantiated = false;
        haveToPickButton = false;
        pickPlayer = false;
        pickSuit = false;

    /*
    spade = Instantiate(spade, new Vector3(100, 100, 0), Quaternion.identity);
    diamond = Instantiate(diamond, new Vector3(100, 100, 0), Quaternion.identity);
    heart = Instantiate(heart, new Vector3(100, 100, 0), Quaternion.identity);
    club = Instantiate(club, new Vector3(100, 100, 0), Quaternion.identity);
    */
}

    void Update()
    {
       if (Input.GetMouseButtonDown(0))
       {
            if (GameActions.checkPlayerCard(player, currentTurn, discard, displayText, turnDirection))
                hasPlayed = true;
       }
       if (takingTurn)
       {
            gameTimer += Time.deltaTime;

            //NPCs take their turn
            if (currentTurn == king.getTurn() && !hasPlayed)
                hasPlayed = GameActions.npcCheckCard(king, displayText, discard, turnDirection, currentTurn);
            if (currentTurn == jess.getTurn() && !hasPlayed)
                hasPlayed = GameActions.npcCheckCard(jess, displayText, discard, turnDirection, currentTurn);
            if (currentTurn == keith.getTurn() && !hasPlayed)
                hasPlayed = GameActions.npcCheckCard(keith, displayText, discard, turnDirection, currentTurn);

            //Each player has 10 seconds to take their turn.
            //If they don't, they will be forced to draw a card and
            //their turn will be skipped
            if (gameTimer >= 10 && !hasPlayed && haveToPickButton)
            {
                gameTimer = 0;

                haveToPickButton = false;
                pickPlayer = false;
                pickSuit = false;

                GameActions.forceDraw(currentTurn, king, jess, keith, player, displayText);
                currentTurn = GameActions.setTurn(currentTurn, turnDirection);
                hasPlayed = false;
                if(currentTurn != player.getTurn())
                {
                    takingTurn = false;
                    StartCoroutine(Wait());
                }
            }
            //If the current player has played a correct card or drawn a card, 
            //it moved to the next player's turn
            else if (hasPlayed && !haveToPickButton)
            {
                gameTimer = 0;

                if (discard.getTopCard().getNumber() == "Jack" && currentTurn != 2 
                        && discard.getTopCard().getPlayedCorrectly())
                    GameActions.npcChangeSuit(getCurrentNpc(), instantiated, discard);
                currentTurn = GameActions.setTurn(currentTurn, turnDirection);
                hasPlayed = false;
                if (currentTurn != player.getTurn())
                {
                    takingTurn = false;
                    instantiated = false;
                    haveToPickButton = false;
                    pickPlayer = false;
                    pickSuit = false;
                    StartCoroutine(Wait());
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        takingTurn = true;
    }

    /*
     *  Getters and Setters
     */

    public int getCurrentTurn()
    {
        return currentTurn;
    }

    public void setText(string s)
    {
        displayText.text = s;
    }

    public Deck getDeck()
    {
        return deck;
    }
    public Discard getDiscard()
    {
        return discard;
    }

    public Card getPrefab(string prefab)
    {
        if (prefab == "Spade")
            return spade;
        else if (prefab == "Heart")
            return heart;
        else if (prefab == "Club")
            return club;
        else if (prefab == "Diamond")
            return diamond;

        return null;
    }

    public Player getCurrentNpc()
    {
        if (currentTurn == king.getTurn())
            return king;
        if (currentTurn == jess.getTurn())
            return jess;
        if (currentTurn == keith.getTurn())
            return keith;

        return null;
    }

    public void setHasPlayed(bool b)
    {
        hasPlayed = b;
    }

    public Player getKing()
    {
        return king;
    }

    public Player getKeith()
    {
        return keith;
    }

    public Player getJess()
    {
        return jess;
    }

    public Player getPlayer()
    {
        return player;
    }

    public void setCurrentTurn(int t)
    {
        currentTurn = t;
    }

    public void setTurnDirection()
    {
        turnDirection *= -1;
    }

    public int getTurnDirection()
    {
        return turnDirection;
    }
    public void setHaveToPickButton(bool b)
    {
        haveToPickButton = b;
    }

    public bool getHaveToPickButton()
    {
        return haveToPickButton;
    }
    
    public void setPickSuit(bool b)
    {
        pickSuit = b;
    }

    public bool getPickSuit()
    {
        return pickSuit;
    }

    public void setPickPlayer(bool b)
    {
        pickPlayer = b;
    }

    public bool getPickPlayer()
    {
        return pickPlayer;
    }

    public void setGameTimer(int i)
    {
        gameTimer = i;
    }
}
