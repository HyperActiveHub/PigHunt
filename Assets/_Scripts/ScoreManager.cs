using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance
	{
		get
		{
			if(instance != null)
				return instance;
			else
				return null;
		}
		set
		{
			if(instance == null)
				instance = value;
		}
	}
	static ScoreManager instance;


    public int connectedPlayers = 1;
    public int maxPlayers = 4;

    private int[] playerScores;
    private float[] lastTimePlayerHitTarget;

    public GameObject textFieldPlayerOne;
    public GameObject textFieldPlayerTwo;
    public GameObject textFieldPlayerThree;
    public GameObject textFieldPlayerFour;

    private GameObject[] playerInfo;

    public Sprite[] targetSprites;

    public string[] activeTargets;

    PlayerInputManager playerInpMgr;
    public TextMeshProUGUI waitingText;
    private bool waitingForPlayers = true;

    private int winnerID = -1;
    public GameObject winningCanvas;

    private void Awake()
	{
		if(instance != null)
			Destroy(gameObject);
		else
			instance = this;
	}

	void Start()
    {
        playerInpMgr = FindObjectOfType<PlayerInputManager>();


        playerInfo = new GameObject[4] { textFieldPlayerOne, textFieldPlayerTwo, textFieldPlayerThree, textFieldPlayerFour };

        // Deactivate not used players
        for (int i = GetConnectedPlayers(); i < maxPlayers; i++)
        {
            playerInfo[i].SetActive(false);
        }
    }

    public void StartGame()
    {
        //TODO Winning Screen
        //End game after X timer endings/rounds
        //The player with the most score wins
        //if 2 or more players have same score, let the player who first shot their target win, i.e
        //save Time.time of last time player shot their target.  
        //In end screen, hide all other players' crosshairs, only show winner so they can move around and see it's them. 
        //Also display winning text: "Player X wins!"
        playerScores = new int[maxPlayers];
        lastTimePlayerHitTarget = new float[4] { Time.time, Time.time, Time.time, Time.time };

        activeTargets = new string[maxPlayers];

        

        winningCanvas.SetActive(false);

        

        SetRandomSprites();

        UpdateTextFields();
    }

    void Update()
    {
        if (waitingForPlayers)
        {
            connectedPlayers = GetConnectedPlayers();

            for (int i = 0; i < 4; i++)
            {
                if (i < connectedPlayers)
                {
                    playerInfo[i].SetActive(true);
                } else
                {
                    playerInfo[i].SetActive(false);
                }

            
            }

            if (connectedPlayers < maxPlayers)
            {
                waitingText.text = "Waiting for " + (maxPlayers - connectedPlayers) + " players";
            } else
            {
                //waitingText.enabled = false;
                waitingForPlayers = false;
                GetComponent<StartTimer>().InitTimer();
            }
        }
        
    }

    void UpdateTargetImages()
    {
        for (int i = 0; i < maxPlayers; i++)
        {
            playerInfo[i].GetComponentInChildren<Image>().sprite = FindSpriteByName(activeTargets[i]);
        }
    }

    void UpdateTextFields()
    {
        for (int i = 0; i < maxPlayers; i++)
        {
            playerInfo[i].GetComponentInChildren<TextMeshProUGUI>().text = "Player " + (i + 1) + ": " + playerScores[i];
        }
    }

    public void OnScore(int playerID, string targetName)
    {
		if(CheckIfRightTarget(targetName, playerID))
		{
			playerScores[playerID] += 10;
            lastTimePlayerHitTarget[playerID] = Time.time;

        }
		else
			playerScores[playerID] -= 3;

        UpdateTextFields();
    }

    private bool CheckIfRightTarget(string targetName, int id)
    {
		print("Target Name: " + targetName);
        if (targetName == null) 
			return false;


        //foreach (string targetType in GetAllSpriteNames())
        {
			string assignedTarget = activeTargets[id];
			//actual target does not switch when Randomize timer ends..
            Debug.Log("Self: " + targetName + "target type: " + assignedTarget);
            if (targetName.Contains(assignedTarget))
			{
				return true;
			}
        }

        return false;
    }

    public void EndGame()
    {
        SetWinner();
        winningCanvas.SetActive(true);
        winningCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Player " + (winnerID + 1) + " wins!!";
        Debug.Log("Winner ID" + winnerID);
        HideAllCrosshairsExceptWinner();
    }

    private void SetWinner()
    {
        int highestScoreIndex = 0;
        int highestScore = -10000;

        int secondHighestScoreIndex = 0;
        int secondHighestScore = -10000;
        for (int i = 0; i < playerScores.Length; i++)
        {
            int score = playerScores[i];
            if (score > highestScore)
            {
                secondHighestScoreIndex = highestScoreIndex;
                secondHighestScore = highestScore;

                highestScoreIndex = i;
                highestScore = score;
            }
        }

        if (highestScore == secondHighestScore)
        {
            float timeSienceLastHitP1 = lastTimePlayerHitTarget[highestScoreIndex];
            float timeSienceLastHitP2 = lastTimePlayerHitTarget[secondHighestScoreIndex];

            if (timeSienceLastHitP1 < timeSienceLastHitP2)
            {
                winnerID = highestScoreIndex;
            } else
            {
                winnerID = secondHighestScoreIndex;
            }
        } else
        {
            winnerID = highestScoreIndex;
        }
    }

    private void HideAllCrosshairsExceptWinner()
    {
        Player[] allCrosshairs = FindObjectsOfType<Player>();
        Debug.Log(allCrosshairs.Length);
        for (int i = 0; i < allCrosshairs.Length; i++)
        {
            if (allCrosshairs[i].Id != winnerID)
            {
                allCrosshairs[i].enabled = false;
				allCrosshairs[i].GetComponent<Renderer>().enabled = false;

			} else
            {
                allCrosshairs[i].enabled = true;
            }
        }
    }

    private string[] GetAllSpriteNames()
    {
        string[] names = new string[targetSprites.Length];
        int index = 0;

        foreach (Sprite sprite in targetSprites)
        {
            names[index++] = sprite.name;
        }

        return names;
    }

    private Sprite FindSpriteByName(string name)
    {
        foreach (Sprite sprite in targetSprites)
        {
            if (name == sprite.name) 
				return sprite;
        }
        return null;
    }

    public void SetRandomSprites()
    {
        for (int i = 0; i < maxPlayers; i++)
        {
			print("old target: " + activeTargets[i]);
            activeTargets[i] = targetSprites[Random.Range(0, targetSprites.Length)].name;
			print("new target: " + activeTargets[i]);
        }

        UpdateTargetImages();
    }

    private int GetConnectedPlayers()
    {
        return playerInpMgr.playerCount;
    }

    public void SetMaxPlayers(int maxPlayers)
    {
        this.maxPlayers = maxPlayers;
    }
}
