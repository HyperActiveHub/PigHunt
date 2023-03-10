using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public int startTime = 200;
    private int timeLeft = 200;
    public TextMeshProUGUI timerTextField;
    public string text = "Time left: ";

    private bool complete = false;

    public void InitTimer()
    {
        timeLeft = startTime + Random.Range(1, 15);
        timerTextField.text = text + timeLeft.ToString();
        StartCoroutine(Timer());
		SwitchWorld.StartedInFirstWorld();

	}

    private void EndTimer()
    {
        complete = true;
        timeLeft = 0;
        timerTextField.text = text + "0";
        StopAllCoroutines();
        FindObjectOfType<Timer>().EndTime();
        ScoreManager.Instance.EndGame();
        // H?mta namn p? 
        GameObject.Find("OpacityBackground").GetComponent<Image>().enabled = true;
    }

    private void Update()
    {
        if (timeLeft == 0 && !complete)
        {
            EndTimer();

            complete = true;
        }
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
                EndTimer();
            }


            if (timeLeft > 0)
                timeLeft--;
            timerTextField.text = text + timeLeft;
        }
    }
}
