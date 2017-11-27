using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LowestRowTrigger : MonoBehaviour
{
    private Transform LowestPosition;
    private RowGeneration rowInfo;
    private BallController ballControl;
    // Use this for initialization
    void Start()
    {
        ballControl = GameObject.Find("Ball Cloner").GetComponent<BallController>();
        rowInfo = GetComponent<RowGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            LowestPosition = transform.GetChild(0);
            if (LowestPosition.childCount > 0)
            {
                LowestPosition = LowestPosition.GetChild(0);
                Vector3 temp = LowestPosition.position;
                temp.y -= rowInfo.DISTANCE_BETWEEN_BLOCKS;
                temp.x = -1;
                Debug.DrawLine(temp, new Vector3(1, temp.y, temp.z), Color.red);
            }
        }
        else
        {
            LowestPosition = null;
        }
        if (LowestPosition)
        {
            if (LowestPosition.position.y < (ballControl.transform.position.y + rowInfo.DISTANCE_BETWEEN_BLOCKS))
            {
                //Debug.Log("Score: " + CurrentScore.ToString());
                /*
                int bestScore = PlayerPrefs.GetInt("BestScore", 0);
                if (CurrentScore > bestScore)
                {
                    PlayerPrefs.SetInt("NewHigh", 1);
                    PlayerPrefs.SetInt("BestScore", CurrentScore);
                }
                else
                {
                    PlayerPrefs.SetInt("NewHigh", 0);
                }
                PlayerPrefs.SetInt("Score", CurrentScore);
                PlayerPrefs.Save();
                */
                ballControl.gameOver = true;

                Transform cloneBalls = GameObject.Find("Clone Balls").transform;
                foreach (Transform clone in cloneBalls)
                {
                    clone.GetComponent<CloneBall>().GameOver();
                }
                SceneManager.LoadScene("restartScene");
                //Use if you want delay, I wanted some Dissolve Effect or Transparent Effect
                //StartCoroutine(EndLevel());
            }
        }
    }

}
