using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloneBall : MonoBehaviour {
    private Rigidbody2D rigid;
    public float speed;
    private LevelCount GameController;
    private Text ballAmount;
    public bool passed;
    private float lastPos;
    Ball MainBall;
    public float diff;
    public bool ded;
    private Transform ResetPoint;
    private void Start()
    {
        ResetPoint = GameObject.Find("Reset").GetComponent<Transform>();
        GameController = GameObject.FindGameObjectWithTag("Controller").GetComponent<LevelCount>();
        ballAmount = GameObject.FindGameObjectWithTag("BallAmount").GetComponent<Text>();
        MainBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>() ;
        //currentSpawnY = 0.45f;
        lastPos = transform.position.y;
    }
    public void SendBallInDirection(Vector3 dir)
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = dir * speed;

    }
    private void Update()
    {
        if (ded)
        {
            rigid.velocity = Vector3.zero;
            rigid.gravityScale = 0;
            rigid.isKinematic = true;
            transform.position = Vector3.MoveTowards(transform.position, ResetPoint.position, 5 * Time.deltaTime);
            if (Vector3.Distance(transform.position, ResetPoint.position) < .1f) {
                Destroy(gameObject);
            }
        }
        else
        {
            if (GameController.LowestPosition)
            {
                //float curlowest = GameController.LowestPosition.transform.position.y - GameController.DISTANCE_BETWEEN_BLOCKS;
                if (passed)
                {
                    if (transform.position.y < GameController.LowestPosition.transform.position.y)
                    {
                        MainBall.CurrentBalls -= 1;
                        ded = true;
                    }
                }
                else
                {
                    if (transform.position.y > GameController.LowestPosition.transform.position.y)
                    {
                        passed = true;
                    }
                    diff = transform.position.y - lastPos;
                    if (diff < 0)
                    {
                        passed = true;
                    }


                }
            }
            lastPos = transform.position.y;
        }

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            MainBall.CurrentBalls -= 1;
            Destroy(gameObject);


            //Debug.Log("This is working");
        }
        if (collision.gameObject.CompareTag("Bricks"))
        {
            collision.gameObject.transform.GetChild(0).GetChild(0).GetComponent<CountKeep>().ReduceCount();

        }
        if (collision.gameObject.CompareTag("NewBallBrick"))
        {
            CountKeep controller = collision.gameObject.transform.GetChild(0).GetChild(0).GetComponent<CountKeep>();
            controller.ReduceCount();
            if (controller.Hits <= 0)
            {
                GameController.currentBalls += 1;
                ballAmount.text = GameController.currentBalls.ToString() + " x";
                Destroy(collision.gameObject);
            }
        }
    }
}
