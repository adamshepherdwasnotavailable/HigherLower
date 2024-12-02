using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ToggleJokerHandler : MonoBehaviour
{
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = Game.deck.UseJokers;
        toggle.onValueChanged.AddListener((value) => UpdateJokerValue(value));
    }
    void OnDestroy()
    {
        toggle.onValueChanged.RemoveAllListeners();
    }

    public void UpdateJokerValue(bool value)
    {
        Game.deck.UseJokers = value;
    }
}
