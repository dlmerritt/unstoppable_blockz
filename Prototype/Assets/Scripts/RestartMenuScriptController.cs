using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenuScriptController : MonoBehaviour {

	public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("menuScene");
    }
}
