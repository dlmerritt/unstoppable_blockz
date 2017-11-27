using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    public int randomBlock;
    public powerType makePower;
    private RowGeneration rowGenerator;
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
        rowGenerator = transform.parent.GetComponent<RowGeneration>();
        StartCoroutine(infoUpdate());

    }
    private void Update()
    {
        if (transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator infoUpdate()
    {
        yield return new WaitForEndOfFrame();
        foreach (Transform blockChild in transform)
        {
            blockChild.GetComponent<boxController>().Create();
        }
        //Guarantee of BlockPlus
        randomBlock = Random.Range(0, transform.childCount);
        transform.GetChild(randomBlock).gameObject.GetComponent<boxController>().makeBallBrick();


        for (int i = 0; i < transform.childCount; i++)
        {
            randomBlock = Random.Range(0, transform.childCount);
            boxController block = transform.GetChild(randomBlock).gameObject.GetComponent<boxController>();
            if (block.CompareTag("Bricks"))
            {
                switch (makePower)
                {
                    case powerType.speed:
                        block.makeSpeedBrick(rowGenerator.speedSprite);
                        break;
                    case powerType.bomb:
                        block.makeBombBrick(rowGenerator.bombSprite);
                        break;
                }

                break;
            }
        }


    }
}
