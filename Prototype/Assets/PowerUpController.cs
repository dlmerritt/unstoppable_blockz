using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum powerType { none, speed, bomb }

public class PowerUpController : MonoBehaviour
{

    public powerType currentPower = powerType.none;
    public Button speedButton;
    public Button bombButton;
    public Button ReloadButton;
    public float speedPowerupTimeOut = 5;
    public float buttonReloadTime = 5;
    public Text BallAmount;
    public Text Score;

    private bool refill;
    private Image buttonReloadImage;
    private float currentButtonReloadTime;
    private powerType lastPower;
    private BallController ballControl;
    private bool reserveSpeed;
    private bool reserveBomb;
    private float currentSpeedTime;
    public void UpdateBalls() {
        if(ballControl)
            BallAmount.text = ballControl.currentBalls.ToString() + " x ";
        
    }
    public void ReloadSpeed()
    {
        if (lastPower == powerType.none || lastPower == powerType.bomb)
        {
            if (speedButton)
            {
                speedButton.interactable = true;
                speedButton.gameObject.GetComponent<Image>().fillAmount = 1;
            }
        }
        if (lastPower == powerType.speed)
        {
            reserveSpeed = true;
        }
    }
    public void ReloadBomb()
    {
        if (lastPower == powerType.none || lastPower == powerType.speed)
        {
            if (bombButton)
            {
                bombButton.interactable = true;
                bombButton.gameObject.GetComponent<Image>().fillAmount = 1;
            }
            if (lastPower == powerType.bomb)
            {
                reserveBomb = true;
            }
        }
    }
    public void speedPowerUp()
    {
        if (currentPower == powerType.none)
        {
            currentPower = powerType.speed;
            speedButton.interactable = false;

        }
    }
    public void bombPowerUp()
    {
        if (currentPower == powerType.none)
        {
            currentPower = powerType.bomb;
            bombButton.interactable = false;

        }
    }

    public void startSpeed()
    {

        ballControl.speedMultiplier = 2;
        currentSpeedTime = 0;
        StartCoroutine(speedDisable());
    }
    public void Kaboom()
    {
        bombButton.gameObject.GetComponent<Image>().fillAmount = 0;
        currentPower = powerType.none;

    }
    IEnumerator speedDisable()
    {
        float countDown = speedPowerupTimeOut;
        lastPower = currentPower;
        currentPower = powerType.none;
        while (countDown > 0f)
        {

            currentSpeedTime += Time.deltaTime;
            speedButton.gameObject.GetComponent<Image>().fillAmount = 1 - currentSpeedTime / speedPowerupTimeOut;

            yield return new WaitForEndOfFrame();
            countDown -= Time.deltaTime;
        }

        //yield return new WaitForSeconds(speedPowerupTimeOut);
        ballControl.speedMultiplier = 1;
        lastPower = powerType.none;
        currentSpeedTime = 0;

        if (reserveBomb)
        {
            ReloadBomb();
            reserveBomb = false;
        }
        if (reserveSpeed)
        {
            ReloadSpeed();
            reserveSpeed = false;
        }
        //speedButton.interactable = true;
    }

    public void Reload()
    {
        ReloadButton.interactable = false;
        buttonReloadImage.fillAmount = 0;
        refill = true;
        currentButtonReloadTime = 0;
        StartCoroutine(inputWait());
    }
    IEnumerator inputWait()
    {
        yield return new WaitForSeconds(.1f);
        ballControl.reloaded = true;
        ballControl.manualReload = true;
    }



    // Use this for initialization
    void Start()
    {
        ballControl = GetComponent<BallController>();
        currentButtonReloadTime = buttonReloadTime;
        buttonReloadImage = ReloadButton.transform.GetChild(0).GetComponent<Image>();
        //initialize swipe data to zero
        ReloadButton.interactable = true;
        UpdateBalls();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentButtonReloadTime < buttonReloadTime && refill)
        {
            buttonReloadImage.fillAmount = currentButtonReloadTime / buttonReloadTime;
            currentButtonReloadTime += Time.deltaTime;
            if (currentButtonReloadTime >= buttonReloadTime)
            {
                ReloadButton.interactable = true;
                currentButtonReloadTime = 0;
                refill = false;
            }
        }
    }
}
