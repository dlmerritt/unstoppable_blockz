using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenuController : MonoBehaviour {

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("menuScene");
    }

	public void OnRedBallsClicked() 
	{

		Debug.Log ("Red Balls purchased!");

	}

	public void OnBlueBallsClicked() 
	{

		Debug.Log ("Blue Balls purchased!");

	}

	public void OnGreenBallsClicked() 
	{

		Debug.Log ("Green Balls purchased!");

	}



}
