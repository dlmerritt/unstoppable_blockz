using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartMenuScriptController : MonoBehaviour {

    private int currentGameScore;
    public Text currentGameScoreText;

	public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("menuScene");
    }

    private void Update()
    {

    }

    private void Awake()
    {

        currentGameScore = ScoreKeep.score;

        currentGameScoreText.text = currentGameScore.ToString();

        

    }

}
