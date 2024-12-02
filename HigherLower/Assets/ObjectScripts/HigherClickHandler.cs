using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HigherClickHandler : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(WagerHigher);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void WagerHigher()
    {
        Card currentCard = Game.deck.currentCard;
        Card nextCard = Game.deck.DrawCard();

        if (currentCard.pips != 14 && nextCard.pips != 14 && nextCard.pips != currentCard.pips)
        {
            if (nextCard.pips > currentCard.pips)
            {
                Game.player.IncreaseMoney(Game.player.wager);
            }
            else
            {
                Game.player.DecreaseMoney(Game.player.wager);
            }
        }
    }
}
