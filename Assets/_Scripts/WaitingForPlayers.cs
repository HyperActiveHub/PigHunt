using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class WaitingForPlayers : MonoBehaviour
{
    public int desiredPlayers = 4;
    public int numberOfPlayers = 0;

    public TextMeshProUGUI playersText;
    public string textString = " connected";

    PlayerInputManager playerInpMgr;

    // Start is called before the first frame update
    void Start()
    {
        playerInpMgr = FindObjectOfType<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfPlayers = playerInpMgr.playerCount;
        if (numberOfPlayers == desiredPlayers)
        {

        } else
        {
            playersText.text = "" + numberOfPlayers + "/" + desiredPlayers + textString;
        }
    }
}
