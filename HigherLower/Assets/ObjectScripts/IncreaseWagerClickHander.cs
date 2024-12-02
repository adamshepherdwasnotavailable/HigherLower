using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IncreaseWagerClickHander : MonoBehaviour
{

    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(IncreaseWager);
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void IncreaseWager()
    {
        Game.player.IncrementWager();
    }
}
