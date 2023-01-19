using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
	[SerializeField, Range(0.2f, 5f)] float animSpeedMultiplierMin = 0.5f;
	[SerializeField, Range(0.2f, 5f)] float animSpeedMultiplierMax = 1.5f; 

	Animator animator;
	List<AnimatorControllerParameter> parameters;
	string animSpeedParamName;

	string spriteName;
    void Start()
    {
		spriteName = GetComponent<SpriteRenderer>().sprite.texture.name;
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
		//KC: call score-system, was this target assigned to the player that hit?
		//add points if player was assigned this target, or remove points if not
		//Play Death-effect - confetti
		ScoreManager.Instance.OnScore(player.id, spriteName);

		Destroy(gameObject);
		//print($"Player <{player.name}> hit target [{name}]!");
	}


}
