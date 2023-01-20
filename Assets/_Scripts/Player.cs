using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	static Canvas indicatorCanvas;

    public int Id = -1;

    private Vector2 moveInput;
    [SerializeField] private float moveSpeed;

	[Header("Gun Stuff")]
	[SerializeField, Min (0.1f)] float fireRate = 0.3f;
	[SerializeField, Min (1)] public int AmmoCount = 6;
	[SerializeField, Min(1)] public float ReloadSpeed = 5;
	[SerializeField] GameObject indicatorUIPrefab;
	[SerializeField] GameObject indicatorCanvasPrefab;

	[HideInInspector]
	public int Ammo;
	[HideInInspector]
	public float FireRateTimer = 0;
	[HideInInspector]
	public float ReloadTimer = 0;

	private float leftBound = -8.5f;
    private float rightBound = 8.5f;
    private float upperBound = 4.5f;
    private float lowerBound = -4.5f;
    private float nudgeDistance = 0.01f;

	public bool IsReloading;

	private void Awake()
	{
		if(indicatorCanvas == null)
			indicatorCanvas = Instantiate(indicatorCanvasPrefab).GetComponent<Canvas>();

		var indicator = Instantiate(indicatorUIPrefab, indicatorCanvas.transform);
		indicator.GetComponent<ShootIndicator>().crosshair = transform;
	}

	void Start()
    {
        RandomizePosition();

		Id = GetComponent<PlayerInput>().playerIndex;

		Ammo = AmmoCount;

		enabled = false;
	}

    private void Update()
    {
		if (FireRateTimer < fireRate)
			FireRateTimer += Time.deltaTime;

		if (ReloadTimer < ReloadSpeed)   //player can only shoot while "reloading"
		{
			ReloadTimer += Time.deltaTime;
			IsReloading = true;
		} else
        {
			IsReloading = false;
        }

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

	void Shoot()
	{
		//print("shot");
		//only shoot if fireRateTimer >= fireRate

		//raycast all
		//if hit, call GetComponent<TargetBehaviour>().TargetHit(this);
		//if no hit, instantiate bullet-hole 
		//Use a bullet, start reloading if bullets == 0
		//Flicker crosshair
		//Play shoot audio

		Vector3 origin = transform.position;
		origin.z = -10;

		RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(origin.x, origin.y), Vector2.zero);
		//RaycastHit[] hits = Physics.RaycastAll(origin, Vector3.forward);
		if(hits.Length > 0)
		{
			//var orderedHits = hits.OrderBy(x => (x.collider.GetComponentInChildren<Renderer>().sortingOrder)).ThenBy(x => x.transform.position.z).Reverse();
			//foreach(var hit in orderedHits)	//make sure hits is in correct order, front to back. 
			foreach(var hit in hits)
			{
				//if(hit.collider.CompareTag("Background"))   //Background elements block shots... cant get it to work. 
				//{
				//	//Missed()
				//	string order = "Order: ";
				//	foreach(var h in orderedHits)
				//		order += h.collider.name;
				//	print(order);
				//	break;
				//}
				hit.collider.GetComponent<TargetBehaviour>()?.TargetHit(this);
			}
		}
		else
		{
			//Missed()
			//spawn bullet-hole at origin (origin.z needs to change)
		}
	}

    void OnMove(InputValue inputVal)
    {
        moveInput = inputVal.Get<Vector2>();
        //Debug.Log(moveInput);
    }

	void OnFire()
    {
		Debug.Log("NOW");
		
		if (IsReloading)
        {
			if (FireRateTimer >= fireRate)
			{
				FireRateTimer = 0;
				Shoot();
			}
        } else
        {
			if (FireRateTimer >= fireRate)
			{
				FireRateTimer = 0;
				//print("Ammo: " + ammo);
				Ammo--;
				if (Ammo == 0)
				{
					Ammo = AmmoCount;
					ReloadTimer = 0;
				}
				//consume ammo, play empty "click" audio
				//start "reloading" (enabling shooting) when ammo == 0
			}
        }
		
	}

    public void RandomizePosition()
    {
        transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), -5);
    }

    public void SwapPosition(Vector3 playerToSwapTo)
    {
        transform.position = playerToSwapTo;
    }

}
