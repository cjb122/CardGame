using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    string number;
    string suit;
    string rule;

    // Start is called before the first frame update
    public Card(int n, int s)
    {
        this.number = setNumber(n);
        this.suit = setSuit(s);

        switch(n)
        {
            case 1:
                rule = "skip";
                break;
            case 7:
                rule = "draw";
                break;
            case 8:
                rule = "switch";
                break;
            case 11:
                rule = "suit";
                break;
            default:
                rule = "";
                break;
        }
    }

    public string setSuit(int s)
    {
        switch (s)
        {
            case 1:
                return "Spades";
            case 2:
                return "Heart";
            case 3:
                return "Diamond";
            case 4:
                return "Clubs";
        }

        return "Error";
    }

    public string setNumber(int n)
    {
        switch (n)
        {
            case 1:
                return "Ace";
            case 2:
                return "2";
            case 3:
                return "3";
            case 4:
                return "4";
            case 5:
                return "5";
            case 6:
                return "6";
            case 7:
                return "7";
            case 8:
                return "8";
            case 9:
                return "9";
            case 10:
                return "10";
            case 11:
                return "Jack";
            case 12:
                return "Queen";
            case 13:
                return "King";
        }


        return "Error";
    }

    public string getSuit()
    {
        return suit;
    }

    public string getNumber()
    {
        return number;
    }

    public string getRule()
    {
        return rule;
    }
}
