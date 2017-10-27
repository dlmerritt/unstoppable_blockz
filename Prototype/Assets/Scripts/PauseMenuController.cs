using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public CanvasGroup pause_menu;

    void Start()
    {
        pause_menu.alpha = 0;
    }

    public void OnPauseButtonClicked()
    {
        if (pause_menu.alpha == 0)
        {
            pause_menu.alpha = 1;
            Time.timeScale = 1.0f;
        }
        else
        {
            pause_menu.alpha = 0;
            Time.timeScale = 0f;
        }
    }

    public void OnContinueButtonClicked()
    {
        pause_menu.alpha = 1;
    }
    
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("menuScene2");
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
