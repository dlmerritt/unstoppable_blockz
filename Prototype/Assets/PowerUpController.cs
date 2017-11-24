using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum powerType { none, speed, bomb }

public class PowerUpController : MonoBehaviour {

    public powerType currentPower = powerType.none;
    private float speedMultiplier = 1;

    public float speedPowerupTimeOut = 5;
    public void speedPowerUp()
    {
        speedMultiplier = 2;
    }


    IEnumerator speedDisable()
    {

        yield return new WaitForSeconds(speedPowerupTimeOut);
        speedMultiplier = 1;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
