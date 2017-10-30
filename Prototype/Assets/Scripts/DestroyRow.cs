using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int randomChild = Random.Range(0, transform.childCount);
        transform.GetChild(randomChild).GetChild(0).GetChild(0).GetComponent<CountKeep>().ConvertToBall();

	}
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount == 0) {
            
            Destroy(gameObject);
        }
	}
}
