using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	static Canvas indicatorCanvas;

    public int id = -1;

    private Vector2 moveInput;
    [SerializeField] private float moveSpeed;

	[Header("Gun Stuff")]
	[SerializeField, Min (0.1f)] float fireRate = 0.3f;
	[SerializeField, Min (1)] public int ammoCount = 6;
	[SerializeField, Min(1)] public float reloadSpeed = 5;
	[SerializeField] GameObject IndicatorUIPrefab;
	[SerializeField] GameObject IndicatorCanvasPrefab;

	[HideInInspector]
	public int ammo;
	[HideInInspector]
	public float fireRateTimer = 0;
	[HideInInspector]
	public float reloadTimer = 0;

	private float leftBound = -8.5f;
    private float rightBound = 8.5f;
    private float upperBound = 4.5f;
    private float lowerBound = -4.5f;
    private float nudgeDistance = 0.01f;

	public bool isReloading;

	private void Awake()
	{
		if(indicatorCanvas == null)
			indicatorCanvas = Instantiate(IndicatorCanvasPrefab).GetComponent<Canvas>();

		var indicator = Instantiate(IndicatorUIPrefab, indicatorCanvas.transform);
		indicator.GetComponent<ShootIndicator>().crosshair = transform;
	}

	void Start()
    {
        RandomizePosition();

		ammo = ammoCount;
	}

    private void Update()
    {
		if(fireRateTimer < fireRate)
			fireRateTimer += Time.deltaTime;

		if(reloadTimer < reloadSpeed)   //player can only shoot while "reloading"
		{
			reloadTimer += Time.deltaTime;
			isReloading = true;
			//print("Reloading.. (shooting activated)");
			if(Input.GetKeyDown(KeyCode.Space)) //TODO: Use new input system
			{
				if(fireRateTimer >= fireRate)
				{
					fireRateTimer = 0;
					Shoot();
				}
			}
		}
		else
		{
			isReloading = false;
			if(Input.GetKeyDown(KeyCode.Space)) //TODO: Use new input system
			{
				if(fireRateTimer >= fireRate)
				{
					fireRateTimer = 0;
					print("Ammo: " + ammo);
					ammo--;
					if(ammo == 0)
					{
						ammo = ammoCount;
						reloadTimer = 0;
					}
					//consume ammo, play empty "click" audio
					//start "reloading" (enabling shooting) when ammo == 0
				}
			}
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

		//3D physics!
		RaycastHit[] hits = Physics.RaycastAll(origin, Vector3.forward, 20);
		if(hits.Length > 0)
		{
			foreach(var hit in hits)
			{
				hit.collider.GetComponent<TargetBehaviour>()?.TargetHit(this);
			}
		}
		else
		{
			//spawn bullet-hole at origin (origin.z needs to change)
		}
	}

    void OnMove(InputValue inputVal)
    {
        moveInput = inputVal.Get<Vector2>();
        Debug.Log(moveInput);
    }

    public void RandomizePosition()
    {
        transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), -5);
    }

    public void SwapPosition(Vector3 playerToSwapTo)
    {
        Vector3 oldPos = transform.position;
        transform.position = playerToSwapTo;
    }

}
