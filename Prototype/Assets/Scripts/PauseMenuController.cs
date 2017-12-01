using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public RectTransform pause_menu;

    void Start()
    {
        pause_menu.gameObject.SetActive(false);
    }

    public void OnPauseButtonClicked()
    {
        if (pause_menu.gameObject.activeSelf == false)
        {
            pause_menu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pause_menu.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void OnContinueButtonClicked()
    {
        if (pause_menu.gameObject.activeSelf == true)
        {
            pause_menu.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }

    }
    
    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("menuScene");
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("gameScene");
    }
}
