using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int currentBalls = 1;
    public GameObject ballCloneprefab;

    public float cloneSpeed = 10;

    public bool gameOver;

    private MobileInput mInput;
    private Vector3 sd;


    private int _damageMultiplier = 1;
    public int damageMultiplier {
        get { return _damageMultiplier; }
        set { _damageMultiplier = value; }
    }

    private float _speedMult = 1;
    public float speedMultiplier
    {
        get { return _speedMult; }
        set { _speedMult = value; }
    }

    // Use this for initialization
    public void changePosition(Vector3 newPosition) {
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
            //Send it flying
            bclone.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            bclone.GetComponent<CloneBall>().SendBallInDirection(direction);
            bclone.GetComponent<CloneBall>().damage = 1 * damageMultiplier;
            if (first)
            {
                //Apply logic
                first = false;
            }
            yield return new WaitForSeconds(0.05f);

        }
    }
    void Start()
    {
        //initialize swipe data to zero
        sd = Vector3.zero;
        mInput = GameObject.Find("GameController").GetComponent<MobileInput>();
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
                //Check if finger lifted
                if (mInput.release)
                {
                    //Start Shooting with delays
                    StartCoroutine(ballShoot(sd.normalized));
                }

            }

        }
    }
}
