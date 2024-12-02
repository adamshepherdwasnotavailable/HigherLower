using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static bool gameStarted = false;
    public static Deck deck;
    public static Player player;

    public static void StartGame()
    {
        if (gameStarted == false)
        {
            gameStarted = true;
            deck = new Deck();
            player = new Player();
        }
    }
}
