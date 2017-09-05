using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountKeep : MonoBehaviour {
    public int Hits = 1;
    
	// Use this for initialization
	void Start () {
        int min = GameObject.Find("LevelContainer").GetComponent<LevelCount>().CurrentLevel -1;
        int max = min + 2;
        Hits = Random.Range(min, max);
        if (Hits == min) {
            int BecomeBall = Random.Range(0, 2);
            if (BecomeBall == 1)
            {
                GetComponent<Text>().text = "";
                gameObject.transform.parent.parent.GetComponent<SpriteRenderer>().color = Color.green;
                gameObject.transform.parent.parent.tag = "NewBallBrick";
            }
            else
            {
                Destroy(gameObject.transform.parent.parent.gameObject);
            }
        }
        else
            GetComponent<Text>().text = Hits.ToString();

	}
    public void ReduceCount() {
        Hits -= 1;
        if (Hits < 1) {
            Destroy(gameObject.transform.parent.parent.gameObject);
        }
        else
            GetComponent<Text>().text = Hits.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
