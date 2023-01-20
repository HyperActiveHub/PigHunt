using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWorld : MonoBehaviour
{
	static float startTime = float.MaxValue;
	[SerializeField] float switchAfterSeconds = 90;
	public static void StartedInFirstWorld()
	{
		startTime = Time.timeSinceLevelLoad;
	}

    void Update()
    {
		if(Time.timeSinceLevelLoad > startTime + switchAfterSeconds)
		{
			GetComponent<Animator>().SetTrigger("Switch");
		}
		else if(startTime != float.MaxValue)
			print("Switching world in " + ((startTime + switchAfterSeconds) - Time.timeSinceLevelLoad).ToString("N") + " seconds.".ToString());
        
    }
}
