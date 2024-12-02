using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int money;
    public int wager;

    public Player()
    {
        money = 1;
    }

    public void IncrementWager()
    {
        if (wager < money)
        {
            wager++;
        }
    }

    public void DecrementWager()
    {
        if (wager > 0)
        {
            wager--;
        }
    }

    public void IncreaseMoney(int amount)
    {
        money += amount;
    }

    public void DecreaseMoney(int amount)
    {
        money -= amount;
        
        if (money < 1)
        {
            money = 1;
        }

        CheckWagerViolation();
    }

    public void CheckWagerViolation()
    {
        if (wager > money)
        {
            wager = money;
        }
    }
}
