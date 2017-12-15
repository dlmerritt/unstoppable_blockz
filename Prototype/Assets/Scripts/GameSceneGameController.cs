using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneGameController : MonoBehaviour {

    public Text bestScoreText;

	// Use this for initialization
	void Start () {
        int best_score = PlayerPrefs.GetInt("BestScore", 0);

        bestScoreText.text = best_score.ToString();


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
