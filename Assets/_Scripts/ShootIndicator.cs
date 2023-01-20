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
		if(player.IsReloading)
		{
			image.fillAmount = player.ReloadTimer / player.ReloadSpeed;
		}
		else
		{
			image.fillAmount = (float)player.Ammo / (float)player.AmmoCount;
		}
    }
}
