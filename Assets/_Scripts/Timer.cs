using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public int timeLeft = 10;
    public TextMeshProUGUI timerText;

    // Use this for initialization    
    void Start()
    {
        timeLeft = Random.Range(10, 20);
        timerText.text = timeLeft.ToString();
        StartCoroutine(LoseTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            StopCoroutine(LoseTime());
            timerText.text = "Time's up!";
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            timerText.text = timeLeft.ToString();
        }
    }
}