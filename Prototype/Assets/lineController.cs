using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineController : MonoBehaviour {
    
    
    public int physicsSteps = 4;
    private LineRenderer lineView;
    private float gravityScale = .1f;
    private Vector3 velocity;
    private Vector3 lastPos;
    private BallController BallInfo;
    private void Start()
    {
        lineView = transform.GetChild(1).GetComponent<LineRenderer>();
        BallInfo = GetComponent<BallController>();
    }
    
    public void updateBallView(Vector3 sd, bool isEnabled)
    {

        lineView.enabled = isEnabled;
        if (!isEnabled) { return; }
        lastPos = transform.position;
        velocity = sd.normalized * BallInfo.cloneSpeed * BallInfo.speedMultiplier;
        lineView.positionCount = 1;
        lineView.SetPosition(0, lastPos);
        int i = 1;
        while (i < physicsSteps)
        {
            velocity.y += (Physics2D.gravity.y + gravityScale) * Time.fixedDeltaTime;
            RaycastHit2D hit = Physics2D.Raycast(lastPos, velocity, 5, 1 << LayerMask.NameToLayer("Walls"));
            if (hit.collider != null)
            {

                velocity = Vector3.Reflect(velocity, hit.normal);
                lastPos = hit.point;
                if (hit.point.x < 0)
                {
                    lastPos.x += GetComponent<CircleCollider2D>().radius;
                }
                else if (hit.point.x > 0)
                {
                    lastPos.x -= GetComponent<CircleCollider2D>().radius;
                }
                lastPos.y -= GetComponent<CircleCollider2D>().radius;


            }

            lineView.positionCount = i + 1;
            lineView.SetPosition(i, lastPos);
            lastPos += velocity * Time.fixedDeltaTime;
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
}
