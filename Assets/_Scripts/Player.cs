using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int port = -1;
    public GameObject characterPrefab;
    private GameObject characterInstance;
    private PlayerInput input;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        input = GetComponent<PlayerInput>();
        port = input.playerIndex;
        // TODO: Initialize elsewhere
        BuildCharacter();
    }
    public GameObject BuildCharacter()
    {
        var charObject = Instantiate(characterPrefab);
        characterInstance = charObject;
        return charObject;
    }
    private string DeviceString()
    {
        var str = "[";
        foreach (var device in input.devices)
        {
            str += device.displayName + ", ";
        }
        str += "]";
        return str;
    }
}
