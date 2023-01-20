using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
	[SerializeField, Range(0.2f, 5f)] float animSpeedMultiplierMin = 0.5f;
	[SerializeField, Range(0.2f, 5f)] float animSpeedMultiplierMax = 1.5f;

	[SerializeField] GameObject deathParticlesPrefab;

	Animator animator;
	[HideInInspector]
	public GameObject RenderParent;
	List<AnimatorControllerParameter> parameters;
	string animSpeedParamName;
	string spriteName;

	private void Awake()
	{
		RenderParent = transform.GetChild(0).gameObject;
		spriteName =  RenderParent.GetComponent<SpriteRenderer>().sprite.texture.name;
	}

	void Start()
    {
		animator = GetComponent<Animator>();
		
		parameters = new List<AnimatorControllerParameter>(animator.parameters);

		for(int i = 0; i < parameters.Count; i++)
		{
			if(parameters[i].type is AnimatorControllerParameterType.Float)
			{
				animSpeedParamName = parameters[i].name;
				parameters.RemoveAt(i);
				break;
			}
		}

		var randomAnimIndex = Random.Range(0, parameters.Count);
		animator.SetTrigger(parameters[randomAnimIndex].name);	//Set which movement animation to play

		var randomSpeedMult = Random.Range(animSpeedMultiplierMin, animSpeedMultiplierMax);
		animator.SetFloat(animSpeedParamName, randomSpeedMult);	//Set animation speed multiplier
	}

    void Update()
    {
		//Destroy target after its movement anination is done
		if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
		{
			Destroy(gameObject);
		}
	}

	public void TargetHit(Player player)
	{
		//ScoreManager.Instance.OnScore(player.Id, spriteName);
		//print($"Player <{player.name}> hit target [{name}]!");

		Destroy(gameObject);
		var go = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
		go.GetComponent<ParticleSystemRenderer>().sortingOrder = RenderParent.GetComponent<SpriteRenderer>().sortingOrder;
	}

}