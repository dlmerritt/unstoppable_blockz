using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    public Text scoreText;
    public Text HighScoreText;
    public Text NewHighScoreText;
	// Use this for initialization
	void Start () {
        int score = PlayerPrefs.GetInt("Score");
        int isNewHigh = PlayerPrefs.GetInt("NewHigh", 1);
        int HighScore = PlayerPrefs.GetInt("BestScore", 0);
        if (isNewHigh == 1) {
            NewHighScoreText.text = "New High Score!";
        }
        HighScoreText.text = "BEST " + HighScore.ToString();
        scoreText.text = score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
