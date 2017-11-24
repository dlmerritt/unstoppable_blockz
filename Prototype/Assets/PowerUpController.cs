using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum powerType { none, speed, bomb }

public class PowerUpController : MonoBehaviour {

    public powerType currentPower = powerType.none;
    public Button speedButton;
    public Button bombButton;
    private float speedMultiplier = 1;
    private BallController ballControl;
    public float speedPowerupTimeOut = 5;
    private float currentSpeedTime;
    public void speedPowerUp()
    {
        if (currentPower == powerType.none)
        {
            speedButton.interactable = false;
            currentPower = powerType.speed;
            ballControl.speedMultiplier = 2;
            currentSpeedTime = 0;
            StartCoroutine(speedDisable());
        }
    }


    IEnumerator speedDisable()
    {

        yield return new WaitForSeconds(speedPowerupTimeOut);
        ballControl.speedMultiplier = 1;
        currentPower = powerType.none;
        //speedButton.interactable = true;
    }
    // Use this for initialization
    void Start () {
        ballControl = GetComponent<BallController>();

    }
	
	// Update is called once per frame
	void Update () {
        switch (currentPower)
        {
            case powerType.speed:
                currentSpeedTime += Time.deltaTime;
                speedButton.gameObject.GetComponent<Image>().fillAmount = 1 -  currentSpeedTime / speedPowerupTimeOut;
                break;
        }
	}
}
