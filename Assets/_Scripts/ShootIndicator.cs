using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShootIndicator : MonoBehaviour
{
	public Transform crosshair;
	Player player;

	Image image;

	private void Start()
	{
		image = GetComponent<Image>();
		player = crosshair.GetComponent<Player>();
	}

	void Update()
    {
		transform.position = crosshair.position;
		if(player.isReloading)
		{
			image.fillAmount = player.reloadTimer / player.reloadSpeed;
		}
		else
		{
			image.fillAmount = (float)player.ammo / (float)player.ammoCount;
		}
    }
}
