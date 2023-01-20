using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillSpin : MonoBehaviour
{
	[SerializeField] float rotationSpeed = 100;
    void Update()
    {
		transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}