using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeep : MonoBehaviour {
    public Text TotalScore;
    public static int score = 1;
    public void AddScore() {
        score++;
        TotalScore.text = score.ToString();
    }
	// Use this for initialization
	void Start () {
        TotalScore.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
