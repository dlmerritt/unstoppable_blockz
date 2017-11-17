using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {

    public float BlinkSpeed = .5f;
    private float currentTime;
    private int currentBall;
	// Use this for initialization
	void Start () {
        currentTime = 0;
        currentBall = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount > 0)
        {
            if (currentTime <= BlinkSpeed)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                if (currentBall > transform.childCount - 1)
                {
                    currentBall = 0;
                }
                transform.GetChild(currentBall).GetComponent<SpriteRenderer>().color = Color.blue;
                currentBall++;
                if (currentBall > transform.childCount - 1)
                {
                    currentBall = 0;
                }
                //transform.GetChild(currentBall).GetComponent<SpriteRenderer>().color = Color.red;
                currentTime = 0;
            }
        }
		
	}
}
