using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateCardsLeftDisplay : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = "Cards Left\n" + Game.deck.deckCards.Count.ToString();
    }
}
