using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClicked : MonoBehaviour
{
    public Button button;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ChangeScene);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeScene()
    {
        if (sceneName == "Game")
        {
            Game.StartGame();
        }
        SceneManager.LoadScene(sceneName);
    }
}
