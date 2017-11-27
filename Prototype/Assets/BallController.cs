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

    public bool gameOver;
    private PowerUpController powerUpControl;

    private Transform cloneParent;
    private MobileInput mInput;
    private Vector3 sd;

    private lineController lineControl;
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
    // Use this for initialization
    public void changePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    IEnumerator ballShoot(Vector3 direction)
    {
        //hook for first ball
        bool first = true;
        //Make balls shoot
        for (int i = 0; i < currentBalls; i++)
        {
            //Create object
            GameObject bclone = Instantiate(ballCloneprefab, transform.position, transform.rotation);
            bclone.transform.SetParent(cloneParent);
            //Send it flying
            bclone.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            bclone.GetComponent<CloneBall>().SendBallInDirection(direction);
            bclone.GetComponent<CloneBall>().damage = 1 * damageMultiplier;
            if (first)
            {
                //Apply logic
                first = false;
                if (powerUpControl.currentPower == powerType.bomb) {
                    bclone.GetComponent<CloneBall>().CreateBomb();
                }
            }
            yield return new WaitForSeconds(0.05f);

        }
    }

    void Start()
    {
        powerUpControl = GetComponent<PowerUpController>();
        sd = Vector3.zero;
        cloneParent = GameObject.Find("Clone Balls").transform;
        lineControl = GetComponent<lineController>();
        mInput = GameObject.Find("GameController").GetComponent<MobileInput>();

    }
    private void Update()
    {

        if (cloneParent.childCount <= 0)
        {
            reloaded = true;

        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (!gameOver)
        {
            sd = mInput.swipeDelta;
            //Check if swipe data is touched and pointing above
            if (sd != Vector3.zero && sd.y > 1.0f)
            {
                if (reloaded || manualReload)
                {
                    lineControl.updateBallView(sd, true);

                    //Check if finger lifted
                    if (mInput.release)
                    {
                        //Start Shooting with delays
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
            }

        }
    }


}
