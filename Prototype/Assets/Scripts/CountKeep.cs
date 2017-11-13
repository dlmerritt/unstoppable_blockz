using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountKeep : MonoBehaviour
{
    public int Hits = 1;
    private LevelCount Controller;
    public bool isBallBlock;
    public bool isDoublePowerUpBlock;
    public bool isBombBlock;
    public void ConvertToBall()
    {
        isBallBlock = true;
        Controller = GameObject.Find("LevelContainer").GetComponent<LevelCount>();
        gameObject.transform.parent.parent.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.blue;
        gameObject.transform.parent.parent.tag = "NewBallBrick";
        GetComponent<Text>().text = (Controller.CurrentRow + 1).ToString();
    }
    public void ConvertToDoublePower(Sprite toChange) {
        isDoublePowerUpBlock = true;
        Controller = GameObject.Find("LevelContainer").GetComponent<LevelCount>();
        GameObject objectToChange = gameObject.transform.parent.parent.gameObject;
        objectToChange.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = toChange;
        objectToChange.tag = "DoublePowerUp";
        //objectToChange.transform.localScale = Vector3.one * .37f;
        objectToChange.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        objectToChange.GetComponent<BoxCollider2D>().size = new Vector2(.16f, .16f);


        Hits = 1;
        GetComponent<Text>().enabled = false;
    }
    public void ConvertToBomb(Sprite toChange) {
        isBombBlock = true;
        Controller = GameObject.Find("LevelContainer").GetComponent<LevelCount>();
        GameObject objectToChange = gameObject.transform.parent.parent.gameObject;
        objectToChange.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = toChange;
        objectToChange.tag = "BombPowerUp";

        //objectToChange.transform.localScale = Vector3.one * .37f;
        objectToChange.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        objectToChange.GetComponent<BoxCollider2D>().size = new Vector2(.16f, .16f);
        Hits = 1;
        GetComponent<Text>().enabled = false;
    }
    // Use this for initialization
    void Start()
    {
        Controller = GameObject.Find("LevelContainer").GetComponent<LevelCount>();
        GetComponent<Text>().color = Color.white;

        int min = Controller.CurrentRow;
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
    public void ReduceCount(int amount)
    {
        Hits -= amount;
        if (Hits < 1)
        {
            
            Destroy(gameObject.transform.parent.parent.gameObject);
        }
        else
            GetComponent<Text>().text = Hits.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
