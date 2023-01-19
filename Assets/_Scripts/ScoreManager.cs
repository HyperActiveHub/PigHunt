using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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


    public static int numberOfPlayers = 4;

    private int[] playerScores = new int[4];

    public GameObject textFieldPlayerOne;
    public GameObject textFieldPlayerTwo;
    public GameObject textFieldPlayerThree;
    public GameObject textFieldPlayerFour;

    private GameObject[] playerInfo;

    public string[] targetTypes;

	private void Awake()
	{
		if(instance != null)
			Destroy(gameObject);
		else
			instance = this;
	}

	void Start()
    {
		//TODO Winning Screen
		//End game after X timer endings/rounds
		//The player with the most score wins
		//if 2 or more players have same score, let the player who first shot their target win, i.e
		//save Time.time of last time player shot their target.  
		//In end screen, hide all other players' crosshairs, only show winner so they can move around and see it's them. 
		//Also display winning text: "Player X wins!"

        playerInfo = new GameObject[4] { textFieldPlayerOne, textFieldPlayerTwo, textFieldPlayerThree, textFieldPlayerFour };

        UpdateTextFields();
    }

    void UpdateTextFields()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerInfo[i].GetComponentInChildren<TextMeshProUGUI>().text = "Player " + (i + 1) + ": " + playerScores[i];
        }
    }

    public void OnScore(int playerID, string targetName)
    {
		if(CheckIfRightTarget(targetName))
		{
			playerScores[playerID] += 10;
		}
		else
			playerScores[playerID] -= 8;

        UpdateTextFields();
    }

    private bool CheckIfRightTarget(string targetName)
    {
        foreach (string targetType in targetTypes)
        {
            if (targetName.Contains(targetType)) return true;
        }

        return false;
    }


}
