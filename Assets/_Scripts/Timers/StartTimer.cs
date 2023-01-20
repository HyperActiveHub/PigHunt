using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    public int startTime = 200;
    private int timeLeft;
    public TextMeshProUGUI timerTextField;
    public string text = "";

    private Timer randomTimer;
    private GameTimer gameTimer;

    private void Start()
    {
        randomTimer = FindObjectOfType<Timer>();
        gameTimer = FindObjectOfType<GameTimer>();
    }

    public void InitTimer()
    {
        timeLeft = startTime;
        timerTextField.text = timeLeft.ToString();
        StartCoroutine(Timer());
    }


    IEnumerator Timer()
    {
        while (true)
        {


            if (Random.Range(0f, 1f) < 0.1)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }

            if (timeLeft <= 3 && Random.Range(0f, 1f) < 0.3f)
            {
                timerTextField.text = text + "SHOOT!!";
                yield return new WaitForSeconds(0.8f);
                timerTextField.enabled = false;

                gameTimer.InitTimer();
                randomTimer.InitTimer();

                StopAllCoroutines();
            }


            timeLeft--;
            timerTextField.text = timeLeft.ToString();
        }
    }
}
