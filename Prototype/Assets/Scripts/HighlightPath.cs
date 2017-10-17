using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPath : MonoBehaviour {
    List<Transform> ballsToBeHighlighted;
    public int currentBall;
    public float curTime;
    public float blinkSpeed = .5f;
	// Use this for initialization
	void Awake () {
        ballsToBeHighlighted = new List<Transform>();
        currentBall = 0;
        for (int i = 0; i < transform.childCount; i++) {
            ballsToBeHighlighted.Add(transform.GetChild(i));
            SpriteRenderer sr = ballsToBeHighlighted[currentBall].GetComponent<SpriteRenderer>();
            sr.color = new Color(0, 255, 55, 110);
        }
        curTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        curTime += Time.deltaTime;
        if (curTime > blinkSpeed)
        {
            SpriteRenderer sr = ballsToBeHighlighted[currentBall].GetComponent<SpriteRenderer>();
            sr.color = new Color(255, 0, 55, 110);
            currentBall++;
            if (currentBall > transform.childCount - 1)
            {
                currentBall = 0;
            }
            sr = ballsToBeHighlighted[currentBall].GetComponent<SpriteRenderer>();
            sr.color = new Color(0, 255, 55, 255);
            curTime = 0;
        }


    }
}
