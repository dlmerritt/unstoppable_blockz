using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRow : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        int randomChild = Random.Range(0, transform.childCount);
        transform.GetChild(randomChild).GetChild(0).GetChild(0).GetComponent<CountKeep>().ConvertToBall();

	}
    public void MakeBomb(Sprite toSend) {
        for (int i = 0; i < 5; i++)
        {
            int randomChild = Random.Range(0, transform.childCount);
            CountKeep ck = transform.GetChild(randomChild).GetChild(0).GetChild(0).GetComponent<CountKeep>();
            if (!ck.isBallBlock && !ck.isDoublePowerUpBlock)
            {
                ck.ConvertToBomb(toSend);
                break;
            }
        }
    }
    public void MakeDoublePower(Sprite toSend) {
        for (int i = 0; i < 5; i++)
        {
            int randomChild = Random.Range(0, transform.childCount);
            CountKeep ck = transform.GetChild(randomChild).GetChild(0).GetChild(0).GetComponent<CountKeep>();
            if (!ck.isBallBlock && !ck.isBombBlock)
            {
                ck.ConvertToDoublePower(toSend);
                break;
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (transform.childCount == 0) {
            
            Destroy(gameObject);
        }
	}
}
