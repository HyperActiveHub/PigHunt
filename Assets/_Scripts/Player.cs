using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] private float moveSpeed;

    private float leftBound = -8.5f;
    private float rightBound = 8.5f;
    private float upperBound = 4.5f;
    private float lowerBound = -4.5f;
    private float nudgeDistance = 0.01f;

    void Start()
    {
        RandomizePosition();
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        float amountToMoveX = moveSpeed * Time.deltaTime * moveInput.x;
        float amountToMoveY = moveSpeed * Time.deltaTime * moveInput.y;

        if (pos.x < leftBound)
            amountToMoveX = nudgeDistance;

        if (pos.x > rightBound)
            amountToMoveX = -nudgeDistance;

        if (pos.y < lowerBound)
            amountToMoveY = nudgeDistance;

        if (pos.y > upperBound)
            amountToMoveY = -nudgeDistance;


        transform.position += new Vector3(amountToMoveX, amountToMoveY, 0);
    }

    void OnMove(InputValue inputVal)
    {
        moveInput = inputVal.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    public void RandomizePosition()
    {
        transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
    }

    public void SwapPosition(Vector3 playerToSwapTo)
    {
        Vector3 oldPos = transform.position;
        //Vector3 oldPosSwapper = transform.position;

        //transform.position = oldPosSwapper;
        transform.position = playerToSwapTo;
    }

}
