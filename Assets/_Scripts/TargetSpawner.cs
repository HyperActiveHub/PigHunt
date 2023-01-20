using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct BackgroundLayerSizePair
{
	public Transform BackgroundLayer;
	public float TargetSizeInLayer;
}

public class TargetSpawner : MonoBehaviour
{
	[SerializeField, Min(0.1f), Tooltip("Seconds between target spawns")] float spawnRate = 2;

	[SerializeField] List<GameObject> targetPrefabs = new List<GameObject>();

	//backgroundLayerPos, sizeInLayer
	[SerializeField] List<BackgroundLayerSizePair> layers;


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
		var layerIndex = Random.Range(0, layers.Count);
		var backgroundLayer = layers[layerIndex];
		var newTarget = Instantiate(gameObject);
		var renderParent = newTarget.GetComponent<TargetBehaviour>().RenderParent;

		foreach(var r in renderParent.GetComponentsInChildren<SpriteRenderer>())
		{
			r.sortingOrder -= layerIndex;
		}

		//var newPos = newTarget.transform.position;
		//newPos.z = backgroundLayer.BackgroundLayer.position.z;
		
		//newTarget.transform.position = newPos;	//animation overrides it..
		newTarget.transform.localScale = Vector3.one * backgroundLayer.TargetSizeInLayer;

	}
}
