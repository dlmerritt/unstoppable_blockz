using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloneBall : MonoBehaviour {
    private Rigidbody2D rigid;
    public float speed;
    private LevelCount GameController;
    private Text ballAmount;

    public float timetokill = 1;
    private float currentTime;
    Ball MainBall;
    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("Controller").GetComponent<LevelCount>();
        ballAmount = GameObject.FindGameObjectWithTag("BallAmount").GetComponent<Text>();
        MainBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>() ;
        //currentSpawnY = 0.45f;
    }
    public void SendBallInDirection(Vector3 dir)
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = dir * speed;
        currentTime = 0;
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timetokill)
        {
            MainBall.CurrentBalls -= 1;
            Destroy(gameObject);
        }

        if (rigid.velocity.magnitude < 1) {
            MainBall.CurrentBalls -= 1;
            Destroy(gameObject);
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
            currentTime = 0;


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
