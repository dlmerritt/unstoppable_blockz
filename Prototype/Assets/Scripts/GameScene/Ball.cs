using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

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
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ballsPreview.parent.gameObject.SetActive(false);
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        TouchFloor();
        gameOver = false;
        isBreakingStuff = false;
        mInput = GameObject.Find("GameController").GetComponent<MobileInput>();
        //line.SetPosition(0, Vector3.zero);
        //line.SetPosition(1, Vector3.zero);
        //line.SetPosition(2, Vector3.zero);
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
            if (touchedFloor)
            {
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

        Vector3 sd = mInput.swipeDelta;
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

                Vector3 forward = sd.normalized * speed;
                forward.z = 0;
                RaycastHit2D h = Physics2D.Raycast(transform.position, forward, LayerMask.GetMask("Walls"));
                //Debug.DrawRay(transform.position, forward * 15, Color.green);
                if (h.collider != null)
                {
                    //Vector3 forward = sd.normalized * 5;
                    Vector3 startPoint = transform.position;
                    line.positionCount = 4;

                    Vector3 reflected = Vector3.Reflect(sd.normalized, h.normal);
                    //Debug.DrawRay(transform.position, forward, Color.red);
                    if (h.collider != null) {
                        //Debug.Log(h.collider.name + " " +h.collider.gameObject.layer);
                        line.SetPosition(0, transform.position);
                        line.SetPosition(1, h.point);
                        startPoint = h.point;
                        h = Physics2D.Raycast(startPoint, reflected, Mathf.Infinity, 1 << LayerMask.NameToLayer("Walls"));

                        if (h.collider != null)
                        {
                            line.SetPosition(2, h.point);
                            startPoint = h.point;
                            reflected = Vector3.Reflect(reflected, h.normal);
                            h = Physics2D.Raycast(startPoint, reflected, Mathf.Infinity, 1 << LayerMask.NameToLayer("Walls"));

                            if (h.collider != null)
                            {
                                line.SetPosition(3, h.point);
                            }
                        }

                        Debug.DrawRay(transform.position, h.point * 15, Color.green);
                    }

                    
                    /*int verts = 100;
                    line.positionCount = verts;

                    Vector2 startpos = transform.position;
                    Vector2 vel = sd.normalized * speed;
                    Vector2 grav = new Vector2(Physics.gravity.x, Physics.gravity.y);

                    for (int i = 0; i < verts; i++)
                    {
                        line.SetPosition(i, startpos);
                        RaycastHit2D ha = Physics2D.Raycast(startpos, sd.normalized, 10, 1 << LayerMask.NameToLayer("Walls"));
                        if (ha.collider != null)
                        {

                            startpos = ha.point;
                            vel = Vector3.Reflect(vel, ha.normal);

                        }
                        //vel += Physics2D.gravity * Time.fixedDeltaTime;
                        startpos += vel * Time.fixedDeltaTime;

                        Vector3 forwarda = sd.normalized * 5;
                        RaycastHit2D he = Physics2D.Raycast(transform.position, -sd.normalized);
                        Debug.DrawRay(transform.position, forwarda, Color.red);
                        if (he.collider != null)
                        {

                            //Debug.DrawRay(transform.position, h.point, Color.green);

                        }

                    }*/
                        ballsPreview.parent.up = sd.normalized;
                        ballsPreview.parent.gameObject.SetActive(true);
                        ballsPreview.localScale = Vector3.Lerp(new Vector3(1, 3, 1), new Vector3(1, 10, 1), sd.magnitude / MAXIMUM_PULL);
                        int totalBricks = GameObject.FindGameObjectsWithTag("Bricks").Length + GameObject.FindGameObjectsWithTag("NewBallBrick").Length;
                        if (mInput.release && totalBricks > 0)
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

                    


                    //Currently dragging

                    //ballsPreview.localScale = Vector3.Lerp(new Vector3(1, 3, 1), new Vector3(1, 10, 1), sd.magnitude / MAXIMUM_PULL);
                }

                //Debug.Log(MobileInput.Instance.swipeDelta);

            }
        }
    }

            IEnumerator ballWait(Vector3 n)
            {
                CurrentBalls = GameController.currentBalls;
                for (int i = 0; i < GameController.currentBalls; i++)
                {
                    int totalBricks = GameObject.FindGameObjectsWithTag("Bricks").Length + GameObject.FindGameObjectsWithTag("NewBallBrick").Length;
                    if (totalBricks <= 0)
                    {
                        break;
                    }
                    GameObject bclone = Instantiate(BallClones, currentPos, resetPos.rotation);
                    bclone.GetComponent<Rigidbody2D>().gravityScale = 0f;
                    bclone.GetComponent<CloneBall>().speed = speed;
                    bclone.GetComponent<CloneBall>().SendBallInDirection(n);
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

        }
    
