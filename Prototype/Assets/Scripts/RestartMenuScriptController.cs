using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenuScriptController : MonoBehaviour {

	public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("menuScene2");
    }
}
