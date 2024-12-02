using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Deck
{
    private static System.Random rng = new System.Random();

    public List<Card> deckCards;
    public Card currentCard;

    private int jokersFound = 0;
    public bool UseJokers
    {
        get
        {
            return _useJokers;
        }
        set
        {
            if (value == false)
            {
                RemoveJokers();
            }
            else if (value == true && _useJokers == false)
            {
                AddJokers();
            }

            _useJokers = value;
        }
    }

    private bool _useJokers = false;

    public Deck()
    {
        RecreateDeck();
        DrawCard();
    }


    public void RecreateDeck()
    {
        // empty the deck
        deckCards = new List<Card>();

        // populate with new cards
        foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
        {
            if (suit == Card.Suit.None) continue;
            for (int i = 1; i <= 13; i++)
            {
                deckCards.Add(new Card(suit, i));
            }
        }

        // if use jokers, insert
        if (UseJokers)
        {
            deckCards.Add(new Card(Card.Suit.None, 14));
            deckCards.Add(new Card(Card.Suit.None, 14));
        }

        Shuffle();

        jokersFound = 0;
    }

    public void Shuffle()
    {
        deckCards = deckCards.OrderBy(_ => rng.Next()).ToList();
    }


    public Card DrawCard()
    {
        if (deckCards.Count == 0) RecreateDeck();

        currentCard = deckCards[0];
        deckCards.RemoveAt(0);

        GameObject go = GameObject.FindGameObjectWithTag("FaceupCard");
        if (go != null)
        {
            MakeCard makeCardScript = go.GetComponent<MakeCard>();
            makeCardScript.UpdateCardUI();
        }

        if (currentCard.suit == Card.Suit.None) jokersFound++;

        return currentCard;
    }

    public void RemoveJokers()
    {
        deckCards.RemoveAll(c => c.suit == Card.Suit.None);
    }

    public void AddJokers()
    {
        for (int i = 0; i < 2 - jokersFound; i++)
        {
            deckCards.Add(new Card(Card.Suit.None, 14));
        }

        Shuffle();
    }
}
