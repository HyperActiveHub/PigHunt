using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
	[SerializeField, Min(0.1f), Tooltip("Seconds between target spawns")] float spawnRate = 2;

	[SerializeField] List<GameObject> targetPrefabs = new List<GameObject>();

	float timer;

    void Start()
    {
        
    }

    
    void Update()
    {
		timer += Time.deltaTime;
		
		if(timer >= spawnRate)
		{
			timer = 0;
			var randomIndex = Random.Range(0, targetPrefabs.Count);
			SpawnTarget(targetPrefabs[randomIndex]);
		}
    }

	void SpawnTarget(GameObject gameObject)
	{
		var newTarget = Instantiate(gameObject);

	}
}
