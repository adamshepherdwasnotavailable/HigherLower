using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateWagerDisplay : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    // Update is called once per frame
    void Update()
    {
        textComponent.text = Game.player.wager.ToString();
    }
}
