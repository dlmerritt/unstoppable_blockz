using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCount : MonoBehaviour {
    public float spawnTime = 5.0f;
    private float currentTime;
    public int currentBalls;
    public Text currentLevelText;
    public int CurrentLevel = 1;
    private Transform spawnLocation;
    private Transform rowContainer;
    public GameObject rowPrefab;
    private Ball ball;
    private Vector2 desiredPosition;
    private Vector2 rowContainerStartingPosition;

    private float currentSpawnY;
    private int score;
    public  float DISTANCE_BETWEEN_BLOCKS = .37f;
    public Transform LowestPosition;
    public bool gameOver = false;
    // Use this for initialization

    void Start () {
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        spawnLocation = GameObject.Find("Reset").GetComponent<Transform>();
        rowContainer = transform;
        rowContainerStartingPosition = rowContainer.transform.position;
        currentTime = spawnTime;
        gameOver = false;
        currentTime = spawnTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameOver)
        {
            if (transform.childCount > 0)
            {
                LowestPosition = transform.GetChild(0);
                if (LowestPosition.childCount > 0)
                {
                    LowestPosition = LowestPosition.GetChild(0);
                }
            }
            else
            {
                LowestPosition = null;
            }
            if (LowestPosition)
            {
                if (LowestPosition.position.y < (spawnLocation.position.y + DISTANCE_BETWEEN_BLOCKS))
                {
                    gameOver = true;
                    ball.gameOver = true;
                    GameObject[] cloneBalls = GameObject.FindGameObjectsWithTag("Clone");
                    foreach (GameObject clone in cloneBalls) {
                        clone.GetComponent<CloneBall>().GameOver();
                    }
                    SceneManager.LoadScene(2);
                    //Use if you want delay, I wanted some Dissolve Effect or Transparent Effect
                    //StartCoroutine(EndLevel());
                }
            }
            currentTime += Time.deltaTime;
            if (currentTime > spawnTime)
            {
                spawnTime *= .98f;
                GenerateNewRow();
                CurrentLevel++;
                currentLevelText.text = CurrentLevel.ToString();
                currentTime = 0;
            }


            if ((Vector2)rowContainer.position != desiredPosition)
            {
                rowContainer.transform.position = Vector3.MoveTowards(rowContainer.transform.position, desiredPosition, Time.deltaTime);
            }
        }
    }
    IEnumerator EndLevel() {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(2);
    }

    private void GenerateNewRow()
    {
        //GameObject.Find("LevelContainer").GetComponent<LevelCount>().CurrentLevel += 1;
        GameObject go = Instantiate(rowPrefab) as GameObject;
        go.transform.SetParent(rowContainer);
        go.transform.localPosition = Vector2.down * currentSpawnY;
        currentSpawnY -= DISTANCE_BETWEEN_BLOCKS;

        desiredPosition = rowContainerStartingPosition + (Vector2.up * currentSpawnY);
    }
}
