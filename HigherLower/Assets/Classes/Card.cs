using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Suit suit;
    public int pips;

    public Card(Suit suit, int pips)
    {
        this.suit = suit;
        this.pips = pips;
    }

    public enum Suit
    {
        None,
        Clubs,
        Hearts,
        Diamonds,
        Spades
    }
}
