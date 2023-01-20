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

    public void InitTimer()
    {
        timeLeft = Random.Range(25, 35);
        timerText.text = "Randomize in: " + timeLeft.ToString();
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

            Vector3 firstPlayer = new Vector3(0,0,0);

            if (players.Count > 0)
            {
                firstPlayer = players[0].transform.position;
            }
            

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

            ScoreManager.Instance.SetRandomSprites();

            InitTimer();
        }
    }

    public void EndTime()
    {
        StopAllCoroutines();
        timeLeft = 0;
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            

            if (Random.Range(0f, 1f) < 0.1)
            {
                yield return new WaitForSeconds(0.5f);
            } else
            {
                yield return new WaitForSeconds(1);
            }

            if (timeLeft <= 3 && Random.Range(0f, 1f) < 0.3f)
            {
                timeLeft = 0;
                timerText.text = "Randomize in: 0";
                StopAllCoroutines();
            }


            if (timeLeft > 0)
                timeLeft--;
            timerText.text = "Randomize in: " + timeLeft;
        }
    }
}