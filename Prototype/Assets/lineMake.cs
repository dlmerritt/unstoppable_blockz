using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineMake : MonoBehaviour
{
    LineRenderer lr;
    Vector3[] lines;
    public float radius = 3;
    public GameObject LinePart;
    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {


        if (lr.positionCount > 0)
        {
            if (lr.enabled)
            {

                lines = new Vector3[lr.positionCount];
                lr.GetPositions(lines);
                int removedLines = lr.positionCount - 1;
                for (int i = removedLines; i < transform.childCount ; i++) {
                    for (int j = 0; j < transform.GetChild(i).childCount; j++) {
                        Destroy(transform.GetChild(i).GetChild(j).gameObject);
                    }
                }
                for (int j = 0; j < lr.positionCount - 1; j++)
                {
                    float dist = Vector3.Distance(lines[j], lines[j+1]);

                    int amount = (int)(dist / radius);

                    if (transform.GetChild(j).childCount < amount)
                    {
                        int needed = amount - transform.GetChild(j).childCount;
                        for (int i = 0; i < needed; i++)
                        {
                            GameObject spawned = (GameObject)Instantiate(LinePart, transform.position, transform.rotation);
                            spawned.transform.SetParent(transform.GetChild(j));
                        }
                    }
                    else if (transform.GetChild(j).childCount > amount)
                    {
                        int over = transform.GetChild(j).childCount - amount;
                        for (int i = 0; i < over; i++)
                        {
                            Destroy(transform.GetChild(j).GetChild(transform.GetChild(j).childCount - i - 1).gameObject);
                        }

                    }

                    float f = 0;
                    for (int i = 0; i < transform.GetChild(j).childCount; i++)
                    {
                        Transform spawned = transform.GetChild(j).GetChild(i);
                        spawned.position = Vector3.Lerp(lines[j], lines[j+1], f / dist);
                        f += radius;
                    }

                }


            }
            else {
                for (int i = 0; i < transform.childCount; i++)
                {
                    for (int j = 0; j < transform.GetChild(i).childCount; j++)
                    {
                        Destroy(transform.GetChild(i).GetChild(j).gameObject);
                    }
                }
            }

        }




    }
}
