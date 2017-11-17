using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private const string url = "Put link to something here";
    private const string twitter_url = "https://twitter.com/u_blockz_game";

    public void OnPlayClick()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void OnRateClick()
    {
        Application.OpenURL(url);
    }

    public void SoundClick()
    {

    }

    public void OnTwitterLogoCLick()
    {
        Application.OpenURL(twitter_url);
    }

    public void OnShopButtonClick()
    {
        SceneManager.LoadScene("shopScene");
    }
}
