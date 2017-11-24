using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{
    /*
    public GameObject BallClones;
    public LevelCount GameController;
    private const float DEADZONE = 30.0f;
    private const float MAXIMUM_PULL = 100.0f;
    public Transform resetPos;
    private bool isBreakingStuff;
    private Vector2 landingPosition;
    public LineRenderer line;
    private bool GameOver;
    public bool gameOver
    {
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
    private MobileInput mInput;
    public float speedMultiplier = 1;
    public bool SpeedPowerUpEnabled = false;
    public float speedMultiplierTime = 3;
    private float currentSpeedTime = 0;

    public bool BombPowerUpEnabled = false;
    public float BombMultiplierTime = 3;
    private float currentBombTime = 0;
    public bool Recalled;
    public GameObject SpeedSlider;
    public GameObject BombSlider;
    Vector3 sd;
    public void ResetSpeed() {
        speedMultiplier = 1;
        SpeedPowerUpEnabled = false;
        SpeedSlider.GetComponent<Slider>().value = 0;
        SpeedSlider.SetActive(false);
    }
    public void ResetBomb() {
        BombSlider.SetActive(false);
        BombPowerUpEnabled = false;
        BombSlider.GetComponent<Slider>().value = 0;
        BombSlider.SetActive(false);
    }
    public void DoubleSpeed() {
        speedMultiplier = 2;
        SpeedPowerUpEnabled = true;
        SpeedSlider.SetActive(true);
        SpeedSlider.GetComponent<Slider>().value = 0;

    }
    public void BombCreate() {
        BombPowerUpEnabled = true;
        BombSlider.SetActive(true);
        BombSlider.GetComponent<Slider>().value = 0;
    }

    private void Start()
    {
        ResetSpeed();
        ResetBomb();
        rigid = GetComponent<Rigidbody2D>();
        ballsPreview.parent.gameObject.SetActive(false);
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        TouchFloor();
        gameOver = false;
        isBreakingStuff = false;
        mInput = GameObject.Find("GameController").GetComponent<MobileInput>();
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);

        sd = Vector3.zero;
        //currentSpawnY = 0.45f;
    }

    private void Update()
    {

        if (!gameOver)
        {
            if (SpeedPowerUpEnabled) {
                currentSpeedTime += Time.deltaTime;
                SpeedSlider.GetComponent<Slider>().value =  1 - currentSpeedTime/speedMultiplierTime;
                if (currentSpeedTime > speedMultiplierTime) {
                    ResetSpeed();
                    currentSpeedTime = 0;
                    SpeedPowerUpEnabled = false;
                }
            }
            if (BombPowerUpEnabled)
            {
                currentBombTime += Time.deltaTime;
                BombSlider.GetComponent<Slider>().value = 1 - currentBombTime / BombMultiplierTime;
                if (currentBombTime > BombMultiplierTime)
                {
                    ResetBomb();
                    currentBombTime = 0;
                    BombPowerUpEnabled = false;
                }
            }
            if (!isBreakingStuff)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                rigid.gravityScale = 0;
                PoolInput();

            }
            if (touchedFloor)
            {
                transform.position = resetPos.position;
                sprite.enabled = true;

            }
            if (Recalled)
            {
                isBreakingStuff = false;
                Recalled = false;
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
    Vector3 last_pos;
    Vector3 velocity;
    int physics_steps = 4;
    private void FixedUpdate()
    {
        if (sd != Vector3.zero)
        {

            last_pos = transform.position;
            velocity = sd.normalized * speed * speedMultiplier;
            line.positionCount = 1;
            line.SetPosition(0, last_pos);
            int i = 1;
            while (i < physics_steps)
            {
                velocity.y += (Physics2D.gravity.y + rigid.gravityScale) * Time.fixedDeltaTime;
                RaycastHit2D hit = Physics2D.Raycast(last_pos, velocity, 5, 1 << LayerMask.NameToLayer("Walls"));
                if (hit.collider != null)
                {

                    velocity = Vector3.Reflect(velocity, hit.normal);
                    last_pos = hit.point;
                    if (hit.point.x < 0)
                    {
                        last_pos.x += GetComponent<CircleCollider2D>().radius;
                    }
                    else if (hit.point.x > 0)
                    {
                        last_pos.x -= GetComponent<CircleCollider2D>().radius;
                    }
                    last_pos.y -= GetComponent<CircleCollider2D>().radius;


                }

                line.positionCount = i + 1;
                line.SetPosition(i, last_pos);
                last_pos += velocity * Time.fixedDeltaTime;
                if (hit)
                {
                    if (hit.collider.name == "TopCollision")
                    {
                        break;
                    }
                }
                i++;
            }

        }
        else {
            line.enabled = false;
        }
    }
    private void PoolInput()
    {
        // Drag the ball around

        sd = mInput.swipeDelta;
        //sd.Set(-sd.x, -sd.y, sd.z);

        if (sd != Vector3.zero)
        {
            //Are we dragging in the wrong direction
            if (sd.y < 1.0f)
            {
                ballsPreview.parent.gameObject.SetActive(false);
                line.enabled = false;
            }
            else
            {
                line.enabled = true;

                //ballsPreview.parent.up = sd.normalized;
                //ballsPreview.parent.gameObject.SetActive(true);
                //ballsPreview.localScale = Vector3.Lerp(new Vector3(1, 3, 1), new Vector3(1, 10, 1), sd.magnitude / MAXIMUM_PULL);
                int totalBricks = GameObject.FindGameObjectsWithTag("Bricks").Length + GameObject.FindGameObjectsWithTag("NewBallBrick").Length;
                if (mInput.release)
                {
                    line.enabled = false;
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
        }
    }

    IEnumerator ballWait(Vector3 n)
    {
        CurrentBalls = GameController.currentBalls;
        bool first = true;
        for (int i = 0; i < GameController.currentBalls; i++)
        {
            int totalBricks = GameObject.FindGameObjectsWithTag("Bricks").Length + GameObject.FindGameObjectsWithTag("NewBallBrick").Length;
            //if (totalBricks <= 0)
            //{
            //    break;
            //}
            GameObject bclone = Instantiate(BallClones, currentPos, resetPos.rotation);
            if (BombPowerUpEnabled && first)
            {
                bclone.GetComponent<CloneBall>().becomeBomb();
                first = false;
            }
            bclone.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            bclone.GetComponent<CloneBall>().speed = speed * speedMultiplier;
            bclone.GetComponent<CloneBall>().SendBallInDirection(n);
            first = false;
            yield return new WaitForSeconds(0.05f);
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


    void Touched()
    {
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
    */
}

