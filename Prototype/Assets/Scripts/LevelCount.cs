using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCount : MonoBehaviour {
    /*
    public Sprite doubleSprite;
    public Sprite bombSprite;
    public float spawnTime = 5.0f;
    private float currentTime;
    public int currentBalls;
    public Text currentScoreText;
    private int curScore;
    public int CurrentScore {
        get { return curScore; }
        set { curScore = value; }
    }
    private Transform spawnLocation;
    private Transform rowContainer;
    public GameObject rowPrefab;
    private Ball ball;
    private Vector2 desiredPosition;
    private Vector2 rowContainerStartingPosition;
    private int curRow;
    public int CurrentRow {
        get { return curRow; }
        set { curRow = value; }
    }
    private float currentSpawnY;
    private int score;
    public  float DISTANCE_BETWEEN_BLOCKS = .37f;
    private Transform LowPos;
    public Transform LowestPosition {
        get { return LowPos; }
        set { LowPos = value; }
    }
    public bool gameOver = false;
    public Text BestScore;
    int SpecialPowerup = 0;
    public int EveryLevelforPowerup;

    public Button RecallButton;
    public float RecallButtonTime;
    private float currentRecallTime;
    // Use this for initialization

    void Start () {
        currentRecallTime = 0;
        BestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        CurrentScore = 0;
        currentScoreText.text = CurrentScore.ToString();
        CurrentRow = 0;
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
                    Vector3 temp = LowestPosition.position;
                    temp.y -= DISTANCE_BETWEEN_BLOCKS;
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
                if (LowestPosition.position.y < (spawnLocation.position.y + DISTANCE_BETWEEN_BLOCKS))
                {
                    //Debug.Log("Score: " + CurrentScore.ToString());
                    int bestScore = PlayerPrefs.GetInt("BestScore", 0);
                    if (CurrentScore > bestScore)
                    {
                        PlayerPrefs.SetInt("NewHigh", 1);
                        PlayerPrefs.SetInt("BestScore", CurrentScore);
                    }
                    else {
                        PlayerPrefs.SetInt("NewHigh", 0);
                    }
                    PlayerPrefs.SetInt("Score", CurrentScore);
                    PlayerPrefs.Save();

                    gameOver = true;
                    ball.gameOver = true;
                    GameObject[] cloneBalls = GameObject.FindGameObjectsWithTag("Clone");
                    foreach (GameObject clone in cloneBalls)
                    {
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
                //spawnTime *= .98f;
                GenerateNewRow();
                CurrentRow++;
                SpecialPowerup++;
                if (SpecialPowerup > EveryLevelforPowerup) {
                    int randomChild = Random.Range(0, 2);
                    if (randomChild == 0)
                    {
                        transform.GetChild(transform.childCount - 1).GetComponent<DestroyRow>().MakeDoublePower(doubleSprite);
                    }
                    else {
                        transform.GetChild(transform.childCount - 1).GetComponent<DestroyRow>().MakeBomb(bombSprite);
                    }
                    SpecialPowerup = 0;
                }
                currentTime = 0;
            }


            if ((Vector2)rowContainer.position != desiredPosition)
            {
                rowContainer.transform.position = Vector3.MoveTowards(rowContainer.transform.position, desiredPosition, Time.deltaTime);
            }

            if (currentRecallTime < RecallButtonTime)
            {
                RecallButton.interactable = false;
                RecallButton.targetGraphic.GetComponent<Image>().fillAmount = currentRecallTime / RecallButtonTime;
                currentRecallTime += Time.deltaTime;
                if (currentRecallTime >= RecallButtonTime) {
                    RecallButton.interactable = true;
                }
            }

        }

    }
    IEnumerator EndLevel() {
        
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(2);
    }
    public void AddScore() {
        CurrentScore++;
        currentScoreText.text = CurrentScore.ToString();
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

    public void RecallBalls() {
        RecallButton.interactable = false;
        RecallButton.targetGraphic.GetComponent<Image>().fillAmount = 0;
        currentRecallTime = 0;
        GameObject[] cloneBalls = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject cBall in cloneBalls) {
            cBall.GetComponent<CloneBall>().recalled = true;
        }
    }
    */
}
