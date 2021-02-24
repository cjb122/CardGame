using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Player king;
    private Player jess;
    private Player keith;
    private Player player;
    private Deck deck;
    //Create discard object ???
    private Stack<Card> discard;
    
    // Start is called before the first frame update
    void Start()
    {
        deck = new Deck();
        discard = new Stack<Card>();
        king = new Player(deck);
        jess = new Player(deck);
        keith = new Player(deck);
        player = new Player(deck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
