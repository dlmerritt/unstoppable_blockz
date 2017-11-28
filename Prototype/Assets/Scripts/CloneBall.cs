using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloneBall : MonoBehaviour
{
    private BallController controller;
    private Rigidbody2D rigid;
    private int _damage;
    private bool gameOver;
    private bool isBomb;
    public int damage {
        get { return _damage; }
        set { _damage = value; }
    }

    public void CreateBomb() {
        isBomb = true;
}
    IEnumerator Explode() {
        if (isBomb)
        {
            Vector3 explosionPos = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, controller.bombRadius);
            List<GameObject> toChange = new List<GameObject>();
            foreach (Collider2D hit in colliders)
            {
                GameObject objectHit = hit.gameObject;
                if (!objectHit.CompareTag("CollisionWalls") && !objectHit.CompareTag("Floor") && !objectHit.CompareTag("Ball") && !objectHit.CompareTag("Clone") && !objectHit.CompareTag("Cloner"))
                {
                    toChange.Add(objectHit);

                    Destroy(objectHit.GetComponent<Rigidbody2D>());
                    Destroy(objectHit.GetComponent<BoxCollider2D>());


                    objectHit.transform.SetParent(null);
                    objectHit.layer = 0;
                    //objectHit.tag = "Ded";
                }

            }
            yield return new WaitForFixedUpdate();
            GameObject Explosion = (GameObject)Instantiate(controller.BombPrefab, transform.position, transform.rotation);
            Destroy(Explosion, 3);
            foreach (GameObject obj in toChange)
            {
                obj.AddComponent<BoxCollider>();
                obj.GetComponent<BoxCollider>().size = Vector3.one * .1f;
                obj.AddComponent<Rigidbody>();
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddExplosionForce(controller.bombPower, transform.position, controller.bombRadius);

                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                    Destroy(obj, .5f);
                }
                
            }
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        controller = GameObject.FindWithTag("Cloner").GetComponent<BallController>();
    }
    public void SendBallInDirection(Vector3 dir)
    {
        Start();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = dir * controller.cloneSpeed;
        rigid.velocity = controller.cloneSpeed * (rigid.velocity.normalized) * controller.speedMultiplier;

    }
    private void LateUpdate()
    {
        if (!gameOver)
        {
            rigid.velocity = controller.cloneSpeed * (rigid.velocity.normalized) * controller.speedMultiplier;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor")) {
            Destroy(gameObject);
        }
        if (isBomb) {
            StartCoroutine(Explode());
        }
    }

    public void GameOver()
    {
        rigid.velocity = Vector3.zero;
        rigid.gravityScale = 0;
        rigid.isKinematic = true;
        gameOver = true;
    }
    /*
    void CheckBoundary() {
        if (rowcon.LowestPosition)
        {
            //float curlowest = GameController.LowestPosition.transform.position.y - GameController.DISTANCE_BETWEEN_BLOCKS;
            if (passed)
            {
                if (transform.position.y < GameController.LowestPosition.transform.position.y - GameController.DISTANCE_BETWEEN_BLOCKS)
                {
                    MainBall.CurrentBalls -= 1;
                    ded = true;
                    if (!touchedandPassed)
                    {
                        MainBall.touchedFloor = true;

                    }
                }
                else
                {
                    ded = false;
                }
            }
            else
            {
                if (transform.position.y > GameController.LowestPosition.transform.position.y - GameController.DISTANCE_BETWEEN_BLOCKS)
                {
                    passed = true;
                    touchedandPassed = true;
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
    */


    /*
    private Rigidbody2D rigid;
    public float speed;
    private LevelCount GameController;
    private Text ballAmount;
    public bool passed;
    private float lastPos;
    BallController MainBall;
    public float diff;
    public bool ded;
    private Transform ResetPoint;
    bool gameOver = false;
    bool touchedandPassed = false;
    private float recallSpeed = 10;
    private float speedMultiplier = 1;
    public bool isBomb;
    public GameObject explosionArt;
    public float Bombpower = 50;
    public float Bombradius = 5;
    public bool recalled;
    private void Start()
    {
        ResetPoint = GameObject.Find("Reset").GetComponent<Transform>();
        GameController = GameObject.FindGameObjectWithTag("Controller").GetComponent<LevelCount>();
        ballAmount = GameObject.FindGameObjectWithTag("BallAmount").GetComponent<Text>();
        MainBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallController>();
        //currentSpawnY = 0.45f;
        lastPos = transform.position.y;
    }
    public void SendBallInDirection(Vector3 dir)
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = dir * speed;
        rigid.velocity = speed * (rigid.velocity.normalized) * speedMultiplier;

    }
    public void GameOver()
    {
        rigid.velocity = Vector3.zero;
        rigid.gravityScale = 0;
        rigid.isKinematic = true;
        gameOver = true;
    }
    private void Update()
    {
        if (!gameOver)
        {
            speedMultiplier = MainBall.speedMultiplier;
            int totalBricks = GameObject.FindGameObjectsWithTag("Bricks").Length + GameObject.FindGameObjectsWithTag("NewBallBrick").Length;
            
            rigid.velocity = speed * (rigid.velocity.normalized) * speedMultiplier;

            if (ded && MainBall.touchedFloor)
            {
                rigid.velocity = Vector3.zero;
                rigid.gravityScale = 0;
                rigid.isKinematic = true;
                transform.position = Vector3.MoveTowards(transform.position, ResetPoint.position, recallSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, ResetPoint.position) < .1f)
                {
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
                        if (transform.position.y < GameController.LowestPosition.transform.position.y - GameController.DISTANCE_BETWEEN_BLOCKS)
                        {
                            MainBall.CurrentBalls -= 1;
                            ded = true;
                            if (!touchedandPassed)
                            {
                                MainBall.touchedFloor = true;

                            }
                        }
                        else
                        {
                            ded = false;
                        }
                    }
                    else
                    {
                        if (transform.position.y > GameController.LowestPosition.transform.position.y - GameController.DISTANCE_BETWEEN_BLOCKS)
                        {
                            passed = true;
                            touchedandPassed = true;
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
            if (totalBricks <= 0 || recalled)
            {
                MainBall.CurrentBalls -= 1;
                MainBall.touchedFloor = true;
                MainBall.Recalled = true;
                //ded = true;
                //passed = true;

                //MainBall.touchedFloor = true;


            }
        }

    }

    public void becomeBomb() {
        isBomb = true;
    }
    IEnumerator bombTransform() {
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, Bombradius);
        List<GameObject> toChange = new List<GameObject>();
        foreach (Collider2D hit in colliders)
        {
            GameObject objectHit = hit.gameObject;
            if (!objectHit.CompareTag("CollisionWalls") && !objectHit.CompareTag("Floor") && !objectHit.CompareTag("Ball") && !objectHit.CompareTag("Clone"))
            {
                toChange.Add(objectHit);

                Destroy(objectHit.GetComponent<Rigidbody2D>());
                Destroy(objectHit.GetComponent<BoxCollider2D>());


                objectHit.transform.SetParent(null);
                objectHit.layer = 0;
                objectHit.tag = "Ded";
            }

        }
        yield return new WaitForFixedUpdate();
        GameObject Explosion = (GameObject)Instantiate(explosionArt, transform.position, transform.rotation);
        Destroy(Explosion, 3);
        foreach (GameObject obj in toChange)
        {
            obj.AddComponent<BoxCollider>();
            obj.AddComponent<Rigidbody>();
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddExplosionForce(Bombpower, transform.position, Bombradius);
  
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            Destroy(obj, 4);
        }
        //Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (isBomb)  {

            StartCoroutine(bombTransform());
            MainBall.CurrentBalls -= 1;
            Vector3 temp = transform.position;
            if (!MainBall.touchedFloor)
            {
                temp.y = ResetPoint.position.y;
                ResetPoint.position = temp;
                Destroy(gameObject);
                MainBall.touchedFloor = true;
            }
        }
        if (collision.gameObject.tag == "Floor")
        {
            MainBall.CurrentBalls -= 1;
            Vector3 temp = transform.position;
            if (!MainBall.touchedFloor)
            {
                temp.y = ResetPoint.position.y;
                ResetPoint.position = temp;
                Destroy(gameObject);
                MainBall.touchedFloor = true;
            }


            //Debug.Log("This is working");
        }
        if (collision.gameObject.CompareTag("Bricks"))
        {
            int damage = 1;
            if (speedMultiplier > 1) { damage = 2; }
            collision.gameObject.transform.GetChild(0).GetChild(0).GetComponent<CountKeep>().ReduceCount(damage );

        }
        if (collision.gameObject.CompareTag("NewBallBrick"))
        {
            CountKeep controller = collision.gameObject.transform.GetChild(0).GetChild(0).GetComponent<CountKeep>();
            int damage = 1;
            if (speedMultiplier > 1) { damage = 2; }
            controller.ReduceCount(damage);
            if (controller.Hits <= 0)
            {
                GameController.currentBalls += 1;
                ballAmount.text = GameController.currentBalls.ToString() + " x";
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("DoublePowerUp") || collision.gameObject.CompareTag("BombPowerUp"))
        {
            string nameOfPowerUp = collision.gameObject.tag;
            if (nameOfPowerUp == "DoublePowerUp") {
                MainBall.DoubleSpeed();
            }
            if (nameOfPowerUp == "BombPowerUp") {
                MainBall.BombCreate();
            }
            Destroy(collision.gameObject);

        }
    }
    */
}
