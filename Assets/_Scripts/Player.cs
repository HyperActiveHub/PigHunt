using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] private float moveSpeed; 
    

    void Start()
    {
        
    }

    private void Update()
    {
        transform.position += (new Vector3(moveInput.x, moveInput.y, 0)) * moveSpeed * Time.deltaTime;
    }

    void OnMove(InputValue inputVal)
    {
        moveInput = inputVal.Get<Vector2>();
        Debug.Log(moveInput);
    }

}
