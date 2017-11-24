using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour {
    public int randomBlock;

    private void Awake()
    {
        int randomAmount = Random.Range(1, transform.childCount);
        for (int i = 0; i < randomAmount; i++)
        {
            int randomChild = Random.Range(0, transform.childCount);
            Destroy(transform.GetChild(randomChild).gameObject);
        }
    }
    private void Start()
    {

        StartCoroutine(infoUpdate());
        
    }
    private void Update()
    {
        if (transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator infoUpdate() {
        yield return new WaitForEndOfFrame();
        foreach (Transform blockChild in transform)
        {
            blockChild.GetComponent<boxController>().Create();
        }
        //Guarantee of BlockPlus
        randomBlock = Random.Range(0, transform.childCount);
        transform.GetChild(randomBlock).gameObject.GetComponent<boxController>().makeBallBrick();

    }
}
