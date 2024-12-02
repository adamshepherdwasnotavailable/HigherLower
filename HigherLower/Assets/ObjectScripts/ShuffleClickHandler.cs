using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuffleClickHandler : MonoBehaviour
{

    public Button button;

    // Update is called once per frame
    void Start()
    {
        button.onClick.AddListener(Game.deck.Shuffle);
    }
    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
