using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCount : MonoBehaviour {
    public float spawnTime = 5.0f;
    private float currentTime;
    public int currentBalls;
    public Text currentLevelText;
    public int CurrentLevel = 1;

    private Transform rowContainer;
    public GameObject rowPrefab;

    private Vector2 desiredPosition;
    private Vector2 rowContainerStartingPosition;

    private float currentSpawnY;
    private int score;
    public  float DISTANCE_BETWEEN_BLOCKS = .37f;

    // Use this for initialization

    void Start () {
        rowContainer = transform;
        rowContainerStartingPosition = rowContainer.transform.position;
        currentTime = spawnTime;
    }
	
	// Update is called once per frame
	void Update () {
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
