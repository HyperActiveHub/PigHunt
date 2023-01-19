using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public int timeLeft = 10;
    public TextMeshProUGUI timerText;
    private Player[] players;

    // Use this for initialization    
    void Start()
    {
        timeLeft = Random.Range(10, 20);
        timerText.text = timeLeft.ToString();
        StartCoroutine(LoseTime());
        players = FindObjectsOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            StopCoroutine(LoseTime());
            timerText.text = "Time's up!";
            foreach (Player p in players)
            {
                Player randomPlayer = players[Random.Range(0, players.Length - 1)];

                p.SwapPosition(randomPlayer);
            }

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