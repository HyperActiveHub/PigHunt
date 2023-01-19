using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public int timeLeft = 10;
    public TextMeshProUGUI timerText;
    private List<Player> players = new List<Player>();
    private bool armed = true;

    // Use this for initialization    
    void Start()
    {
        InitTimer();
    }

    void InitTimer()
    {
        timeLeft = Random.Range(25, 35);
        timerText.text = timeLeft.ToString();
        StartCoroutine(LoseTime());
        players.AddRange(FindObjectsOfType<Player>());
        armed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0 && armed)
        {
            armed = false;
            StopAllCoroutines();
            //timerText.text = "Time's up!";

            players.Clear();
            players.AddRange(FindObjectsOfType<Player>());
            Vector3 firstPlayer = players[0].transform.position;

            for (int i = 0; i < players.Count; i++) 
            {
                if (i == players.Count - 1)
                {
                    players[i].SwapPosition(firstPlayer);
                } else
                {
                    players[i].SwapPosition(players[i + 1].transform.position);
                }

            }
            InitTimer();
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