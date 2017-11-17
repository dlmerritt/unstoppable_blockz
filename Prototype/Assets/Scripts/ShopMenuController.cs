using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenuController : MonoBehaviour {

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("menuScene");
    }


}
