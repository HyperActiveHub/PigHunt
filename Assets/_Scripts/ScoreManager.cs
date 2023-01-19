using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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


    public int numberOfPlayers = 4;
    private int maxPlayers = 4;

    private int[] playerScores;

    public GameObject textFieldPlayerOne;
    public GameObject textFieldPlayerTwo;
    public GameObject textFieldPlayerThree;
    public GameObject textFieldPlayerFour;

    private GameObject[] playerInfo;

    public Sprite[] targetSprites;

    public string[] activeTargets;

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
        playerScores = new int[numberOfPlayers];

        activeTargets = new string[numberOfPlayers];
        playerInfo = new GameObject[4] { textFieldPlayerOne, textFieldPlayerTwo, textFieldPlayerThree, textFieldPlayerFour };

        // Deactivate not used players
        for (int i = numberOfPlayers; i < maxPlayers; i++)
        {
            playerInfo[i].SetActive(false);
        }

        SetRandomSprites();

        UpdateTextFields();
    }

    void UpdateTargetImages()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerInfo[i].GetComponentInChildren<Image>().sprite = FindSpriteByName(activeTargets[i]);
        }
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
        foreach (string targetType in GetAllSpriteNames())
        {
            if (targetName.Contains(targetType)) return true;
        }

        return false;
    }

    private string[] GetAllSpriteNames()
    {
        string[] names = new string[targetSprites.Length];
        int index = 0;

        foreach (Sprite sprite in targetSprites)
        {
            names[index] = sprite.name;
        }

        return names;
    }

    private Sprite FindSpriteByName(string name)
    {
        foreach (Sprite sprite in targetSprites)
        {
            if (name == sprite.name) return sprite;
        }
        return null;
    }

    public void SetRandomSprites()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            activeTargets[i] = targetSprites[Random.Range(0, targetSprites.Length)].name;
        }

        UpdateTargetImages();
    }

}
