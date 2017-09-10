using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountKeep : MonoBehaviour {
    public int Hits = 1;
    private LevelCount Controller;
    public bool isBallBlock;
    public void ConvertToBall() {
        isBallBlock = true;
        Controller = GameObject.Find("LevelContainer").GetComponent<LevelCount>();
        gameObject.transform.parent.parent.GetComponent<SpriteRenderer>().color = Color.green;
        gameObject.transform.parent.parent.tag = "NewBallBrick";
        GetComponent<Text>().text = (Controller.CurrentLevel + 1).ToString();
    }
    
	// Use this for initialization
	void Start () {
        Controller = GameObject.Find("LevelContainer").GetComponent<LevelCount>();
        GetComponent<Text>().color = Color.white;

        int min = Controller.CurrentLevel ;
        int max = min + 2;
        Hits = Random.Range(min, max);
        // hasGreen = transform.parent.parent.GetComponent<DestroyRow>().hasGreen;

            int BecomeBall = Random.Range(0, 2);
            if (BecomeBall == 0 && !isBallBlock)
            { 
                Destroy(gameObject.transform.parent.parent.gameObject);
            }


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
