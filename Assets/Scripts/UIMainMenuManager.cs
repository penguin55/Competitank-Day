using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuManager : MonoBehaviour
{
    public void Click()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("GAME");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
