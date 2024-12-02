using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecreaseWagerClickHandler : MonoBehaviour
{

    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(DecreaseWager);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void DecreaseWager()
    {
        Game.player.DecrementWager();
    }
}
