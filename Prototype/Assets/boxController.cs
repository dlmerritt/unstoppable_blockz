using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boxController : MonoBehaviour
{
    public int lives = 1;
    
    private RowGeneration rowControl;
    private Text lifeText;
    private SpriteRenderer Artwork;
    private BallController ballControl;
    private PowerUpController powerUpControl;
    // Use this for initialization
    public void Create()
    {
        ballControl = GameObject.Find("Ball Cloner").GetComponent<BallController>();
        powerUpControl = ballControl.GetComponent<PowerUpController>();
        rowControl = GameObject.Find("LevelContainer").GetComponent<RowGeneration>();
        lifeText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Artwork = transform.GetChild(1).GetComponent<SpriteRenderer>();
        lifeText.color = Color.white;
        int min = rowControl.currentRow;
        int max = min + 2;
        lives = Random.Range(min, max);
        lifeText.text = lives.ToString();


    }

    // Update is called once per frame
    void Update()
    {

    }
    private void updateLives(GameObject damager)
    {
        lives -= damager.GetComponent<CloneBall>().damage;
        lifeText.text = lives.ToString();

        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Clone"))
        {
            updateLives(collision.gameObject);

        }
    }

    public void makeBallBrick()
    {
        Artwork.color = Color.blue;
        tag = "NewBallBrick";
    }

    public void makeSpeedBrick(Sprite SpeedSign) {
        Artwork.sprite = SpeedSign;
        Artwork.color = Color.black;
        tag = "DoublePowerUp";
        lives = 1;
        lifeText.text = "";
    }
    public void makeBombBrick(Sprite BombSign) {
        Artwork.transform.localScale = Vector3.one * 0.1302038f;
        Artwork.sprite = BombSign;
        Artwork.color = Color.white;
        tag = "BombPowerUp";
        lifeText.text = "";
        lives = 1;
    }
    public void OnDestroy()
    {
        switch (tag)
        {
            case "NewBallBrick":
                ballControl.currentBalls++;
                break;
            case "DoublePowerUp":
                powerUpControl.ReloadSpeed();
                break;
            case "BombPowerUp":
                powerUpControl.ReloadBomb();
                break;
        }
    }

    /*
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
        objectToChange.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.black;

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
        objectToChange.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        objectToChange.transform.GetChild(1).localScale = Vector3.one * 0.1755045f;
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

    
    */
}
