using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClicked : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(PlayGame);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Game")
    }
}
