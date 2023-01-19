using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static int numberOfPlayers = 4;

    private int[] playerScores = new int[4];

    public TextMeshProUGUI textFieldPlayerOne;
    public TextMeshProUGUI textFieldPlayerTwo;
    public TextMeshProUGUI textFieldPlayerThree;
    public TextMeshProUGUI textFieldPlayerFour;

    private TextMeshProUGUI[] textFields;

    public string[] targetTypes;

    // Start is called before the first frame update
    void Start()
    {

        textFields = new TextMeshProUGUI[4] { textFieldPlayerOne, textFieldPlayerTwo, textFieldPlayerThree, textFieldPlayerFour };

        UpdateTextFields();
    }

    void UpdateTextFields()
    {
        for (int i = 0; i < textFields.Length; i++)
        {
            textFields[i].text = "Player " + (i + 1) + ": " + playerScores[i];
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
        foreach (string targetType in targetTypes)
        {
            if (targetName.Contains(targetType)) return true;
        }

        return false;
    }


}
