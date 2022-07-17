using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject body;
	public GameObject craftExplosion;
	public float speed = 1000.0f;
	public float maxPickupDistance = 0.5f;
	public bool alive = true;

	Rigidbody2D rb;
	Animator bodyAnimator;
	Inventory inventory;
	Chef chef;
	public Hand hand;

	public GameObject dice;

	private void OnEnable()
	{
		Chef.onCookedItem += OnCookedItem;
	}

	private void onDisable()
	{
		Chef.onCookedItem -= OnCookedItem;
	}
	void Start()
    {
		rb = gameObject.GetComponent<Rigidbody2D>();
		inventory = gameObject.GetComponent<Inventory>();
		chef = gameObject.GetComponent<Chef>();
		bodyAnimator = body.GetComponent<Animator>();
	}

    void Update()
    {
		if (!alive)
			return;

		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		Vector2 movement = new Vector2(x, y);
		movement.Normalize();

		rb.velocity = movement * speed;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			PickupPoint closestPickupPoint = this.FindClosestPickupPoint(this.maxPickupDistance);
			this.InteractWithPickupPoint(closestPickupPoint);
		}

		bodyAnimator.SetBool("holding", inventory.items.Count > 0);

		float mag = rb.velocity.magnitude;
		bodyAnimator.SetFloat("speed", mag);

		float speedx = Mathf.Abs(rb.velocity.x);
		if (speedx < 0.1f)
			return;

		Vector3 bodyScale = body.transform.localScale;
		int scalex = rb.velocity.x > 0 ? 1 : -1;
		body.gameObject.transform.localScale = new Vector3(scalex, bodyScale.y, bodyScale.z);
	}

	void OnTriggerStay2D(Collider2D other)
	{

	}

	public PickupPoint FindClosestPickupPoint(float maxDistance)
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("PickupPoint");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance && curDistance < maxDistance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		if (!closest) return null;
		return closest.GetComponent<PickupPoint>();
	}

	public void InteractWithPickupPoint(PickupPoint pickupPoint)
    {
		if (!pickupPoint) return;

		// Do pickup stuff
		if (pickupPoint.GetItems().Count > 0)
		{
			if (pickupPoint.IsInteractable() && pickupPoint.pickup)
			{
				bool isHoldingItems = this.inventory.items.Count > 0;

				if (!isHoldingItems)
				{
					AudioManager.Instance.PlaySfx("Pickup");
					Item item = pickupPoint.PickupItem();
					inventory.AddItem(item);
				}
				else if (isHoldingItems)
				{
					Item item1 = this.inventory.items[0];
					Item item2 = pickupPoint.GetItems()[0];

					bool canCookWithResources = chef.CanCookWithItems(item1, item2);
					if (canCookWithResources)
					{
						while (pickupPoint.GetItems().Count > 0)
						{
							pickupPoint.PickupItem();
							GameObject newItemObject = chef.TryCookWith(item1, item2);

							if (item1 && item1.gameObject)
							{
								this.inventory.RemoveItem(item1);
								Destroy(item1.gameObject);
							}
							if (item2 && item2.gameObject)
							{
								Destroy(item2.gameObject);
							}

							Item newItem = newItemObject?.GetComponent<Item>();
							this.inventory?.AddItem(newItem);
						}

						AudioManager.Instance.PlaySfx("Success");
						// chef.TryCookWith(this.inventory.items[0], this.inventory.items[1]);
					}
					else
					{
						AudioManager.Instance.PlaySfx("FailCraft");
					}
				}
			}
		}
		else
		{
			if (pickupPoint.IsInteractable() && pickupPoint.dropoff)
			{
				if (pickupPoint.GetItems().Count == 0)
				{
					while (this.inventory.items.Count > 0)
					{
						pickupPoint.DropOffItem(this.inventory.PopItem());
					}

					AudioManager.Instance.PlaySfx("Place");
				}
				else
				{
					AudioManager.Instance.PlaySfx("FailCraft");
				}
			}
		}
	}

	void OnCookedItem(Item item)
	{
		int score = 0;
		switch (item.number)
		{
			case Item.ItemNumber.ONE: score = 150; break;
			case Item.ItemNumber.TWO: score = 75; break;
			case Item.ItemNumber.THREE: score = 75; break;
			case Item.ItemNumber.FOUR: score = 50; break;
			case Item.ItemNumber.FIVE: score = 50; break;
			case Item.ItemNumber.SIX: score = 25; break;
		}

		switch (item.color)
		{
			case Item.ItemColor.WHITE:
			case Item.ItemColor.YELLOW:
			case Item.ItemColor.RED:
			case Item.ItemColor.BLUE:
				score *= 1;
				break;
			case Item.ItemColor.ORANGE:
			case Item.ItemColor.VIOLET:
			case Item.ItemColor.GREEN:
				score *= 2;
				break;
		}

		GameObject explosion = Instantiate(craftExplosion, body.transform.GetChild(0).position, Quaternion.identity);
		AudioManager.Instance.PlaySfx("Craft1");
		CameraManager.instance.Shake(0.1f, 0.2f);
		ScoreSpawner.SpawnScore(score, body.transform.GetChild(0).position, true);
	}
}
