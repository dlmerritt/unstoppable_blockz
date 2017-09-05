using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private const string url = "Put link to something here";

    public void OnPlayClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnRateClick()
    {
        Application.OpenURL(url);
    }

    public void SoundClick()
    {

    }
}
