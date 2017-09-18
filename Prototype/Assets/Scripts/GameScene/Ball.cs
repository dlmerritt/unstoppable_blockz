using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GameObject BallClones;
    public LevelCount GameController;
    private const float DEADZONE = 30.0f;
    private const float MAXIMUM_PULL = 100.0f;
    public Transform resetPos;
    private bool isBreakingStuff;
    private Vector2 landingPosition;

    private bool GameOver;
    public bool gameOver {
        get { return GameOver; }
        set { GameOver = value; }
    }
    private Rigidbody2D rigid;
    public float speed;

    public Transform ballsPreview;
    public GameObject tutorialContainer;

    public int CurrentBalls;
    public bool touchedFloor;
    private SpriteRenderer sprite;
    private Vector3 currentPos;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ballsPreview.parent.gameObject.SetActive(false);
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        TouchFloor();
        //currentSpawnY = 0.45f;
    }

    private void Update()
    {


        if (!gameOver)
        {

            if (!isBreakingStuff)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                rigid.gravityScale = 0;
                PoolInput();

            }
            if (touchedFloor) {
                transform.position = resetPos.position;
                sprite.enabled = true;
                
            }
            if (CurrentBalls <= 0 && GameObject.FindGameObjectsWithTag("Clone").Length == 0)
            {
                if (touchedFloor)
                {

                    //resetPos.position = transform.position;
                    
                    isBreakingStuff = false;
                }
                else
                {
                    transform.position = resetPos.position;
                }


            }
        }

    }

    private void PoolInput()
    {
        // Drag the ball around

        Vector3 sd = MobileInput.Instance.swipeDelta;
        //sd.Set(-sd.x, -sd.y, sd.z);

        if (sd != Vector3.zero)
        {

            //Are we dragging in the wrong direction
            if (sd.y < 1.0f)
            {
                ballsPreview.parent.gameObject.SetActive(false);
            }
            else
            {
                ballsPreview.parent.up = sd.normalized;
                ballsPreview.parent.gameObject.SetActive(true);
                ballsPreview.localScale = Vector3.Lerp(new Vector3(1, 3, 1), new Vector3(1, 10, 1), sd.magnitude / MAXIMUM_PULL);

                if (MobileInput.Instance.release)
                {
                    tutorialContainer.SetActive(false);
                    isBreakingStuff = true;
                    //SendBallInDirection(sd.normalized);
                    currentPos = resetPos.position;
                    touchedFloor = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    StartCoroutine(ballWait(sd.normalized));
                    ballsPreview.parent.gameObject.SetActive(false);
                }

            }


            //Currently dragging

            //ballsPreview.localScale = Vector3.Lerp(new Vector3(1, 3, 1), new Vector3(1, 10, 1), sd.magnitude / MAXIMUM_PULL);
        }

        //Debug.Log(MobileInput.Instance.swipeDelta);

    }

    IEnumerator  ballWait(Vector3 n) {
        CurrentBalls = GameController.currentBalls - 1;
        for (int i = 0; i < GameController.currentBalls - 1; i++)
        {
            yield return new WaitForSeconds(0.05f);
            GameObject bclone = Instantiate(BallClones, currentPos, resetPos.rotation);
            bclone.GetComponent<Rigidbody2D>().gravityScale = .1f;
            bclone.GetComponent<CloneBall>().speed = speed;
            bclone.GetComponent<CloneBall>().SendBallInDirection(n);
        }

        
    }

    private void SendBallInDirection(Vector3 dir)
    {
        sprite.enabled = false;
        touchedFloor = false;
        rigid.gravityScale = .8f;
        rigid.velocity = dir * speed;

    }

    private void TouchFloor()
    {
        sprite.enabled = true;
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
        touchedFloor = true;
        Vector3 temp = transform.position;
        temp.y = -1.606f;
        transform.position = temp;

    }


    void Touched() {
        touchedFloor = true;
    }


   // void OnCollisionEnter2D(Collision2D collision)
    //{
        //if (collision.gameObject.tag == "Floor")
       // {
            //TouchFloor();

            //Debug.Log("This is working");
       // }
 //   }

}
