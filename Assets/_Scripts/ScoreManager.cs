using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int numberOfPlayers = 4;

    private int[] playerScores = new int[4];

    public GameObject textFieldPlayerOne;
    public GameObject textFieldPlayerTwo;
    public GameObject textFieldPlayerThree;
    public GameObject textFieldPlayerFour;

    private GameObject[] playerInfo;

    public Sprite[] targetSprites;

    public string[] activeTargets;

    // Start is called before the first frame update
    void Start()
    {

        playerInfo = new GameObject[4] { textFieldPlayerOne, textFieldPlayerTwo, textFieldPlayerThree, textFieldPlayerFour };



        UpdateTextFields();
        UpdateTargetImages();
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
        if (CheckIfRightTarget(targetName))
        {
            playerScores[playerID] += 1;
        }

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


}
