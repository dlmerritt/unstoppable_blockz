using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallController : MonoBehaviour
{
    public int currentBalls = 1;
    public GameObject ballCloneprefab;
    public float cloneSpeed = 10;

    public float bombRadius = 5;
    public float bombPower = 10;
    public GameObject BombPrefab;
    public GameObject BallGraphics;
    public GameObject reloadBallGraphics;
    public bool gameOver;
    private PowerUpController powerUpControl;

    private Transform cloneParent;
    private Transform ignoreClonesParent;
    private MobileInput mInput;
    private Vector3 sd;
    public lineController lineControl;
    public lineController reloadLineControl;
    private Vector3 _returnPosition;
    public Vector3 returnPosition
    {
        get { return _returnPosition; }
        set { _returnPosition = value; }
    }
    private bool _reloaded;
    public bool reloaded
    {
        get { return _reloaded; }
        set { _reloaded = value; }
    }
    private int _damageMultiplier = 1;
    public int damageMultiplier
    {
        get { return _damageMultiplier; }
        set { _damageMultiplier = value; }
    }

    private float _speedMult = 1;
    public float speedMultiplier
    {
        get { return _speedMult; }
        set { _speedMult = value; }
    }
    private bool _manualReload;
    public bool manualReload
    {
        get { return _manualReload; }
        set { _manualReload = value; }
    }
    public bool firstBallReturned = false;
    private bool allOut = true;
    private float originalY;
    // Use this for initialization
    public void changePosition(Vector3 pos)
    {

        if (!firstBallReturned)
        {
            returnPosition = new Vector3(pos.x, originalY, pos.z);
            BallGraphics.GetComponent<SpriteRenderer>().enabled = true;
            BallGraphics.transform.position = returnPosition;
            firstBallReturned = true;

        }

    }

    IEnumerator ballShoot(Vector3 direction)
    {
        //hook for first ball
        bool first = true;
        //Make balls shoot
        allOut = false;
        if (!manualReload)
        {
            BallGraphics.GetComponent<SpriteRenderer>().enabled = false;
        }
        reloadBallGraphics.GetComponent<SpriteRenderer>().enabled = false;
        Vector3 spawnPos = transform.position;
        bool mReload = false;
        if (manualReload)
        {
            spawnPos.x = 0;
            mReload = true;

        }

        for (int i = 0; i < currentBalls; i++)
        {
            //Create object
            GameObject bclone = Instantiate(ballCloneprefab, spawnPos, transform.rotation);
            bclone.transform.SetParent(cloneParent);
            //Send it flying
            bclone.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            if (mReload)
            {
                bclone.transform.SetParent(ignoreClonesParent);
                bclone.GetComponent<CloneBall>().dummyBall();

            }
            bclone.GetComponent<CloneBall>().SendBallInDirection(direction);
            bclone.GetComponent<CloneBall>().damage = 1 * damageMultiplier;
            if (first)
            {


                //Apply logic
                first = false;
                if (powerUpControl.currentPower == powerType.bomb)
                {
                    bclone.GetComponent<CloneBall>().CreateBomb();
                }
            }
            yield return new WaitForSeconds(0.05f);

        }
        reloadBallGraphics.GetComponent<SpriteRenderer>().enabled = false;
        allOut = true;
        //changePosition(transform.position);
    }

    void Start()
    {
        returnPosition = transform.position;
        originalY = transform.position.y;
        powerUpControl = GetComponent<PowerUpController>();
        sd = Vector3.zero;
        cloneParent = GameObject.Find("Clone Balls").transform;
        ignoreClonesParent = GameObject.Find("IgnoreCloneBalls").transform;
        mInput = GameObject.Find("GameController").GetComponent<MobileInput>();

    }
    private void Update()
    {

        if (cloneParent.childCount <= 0)
        {
            reloaded = true;
            transform.position = returnPosition;
            BallGraphics.transform.position = transform.position;

        }


    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (!gameOver)
        {
            sd = mInput.swipeDelta;
            if (manualReload)
            {
                reloadBallGraphics.GetComponent<SpriteRenderer>().enabled = true;
            }
            //Check if swipe data is touched and pointing above
            if (sd != Vector3.zero && sd.y > 1.0f)
            {
                if (reloaded || manualReload)
                {
                    if (manualReload)
                    {
                        reloadLineControl.updateBallView(sd, true);
                        lineControl.updateBallView(sd, false);
                    }
                    else
                    {
                        reloadLineControl.updateBallView(sd, false);
                        lineControl.updateBallView(sd, true);
                    }

                    //Check if finger lifted
                    if (mInput.release)
                    {
                        //Start Shooting with delays
                        if (!manualReload)
                        {
                            firstBallReturned = false;
                        }

                        StartCoroutine(ballShoot(sd.normalized));

                        reloaded = false;
                        if (manualReload)
                        {

                            manualReload = false;

                        }

                        switch (powerUpControl.currentPower)
                        {
                            case (powerType.speed):
                                powerUpControl.startSpeed();
                                break;
                            case (powerType.bomb):
                                //bombstuff
                                powerUpControl.Kaboom();
                                break;

                        }


                    }

                }

            }
            else
            {
                lineControl.updateBallView(sd, false);
                reloadLineControl.updateBallView(sd, false);
            }

        }
    }


}
