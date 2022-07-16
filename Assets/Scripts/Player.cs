using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject body;
	public float speed = 1000.0f;
	public float maxPickupDistance = 0.5f;

	Rigidbody2D rb;
	Animator bodyAnimator;
	Inventory inventory;
	Chef chef;

    void Start()
    {
		rb = gameObject.GetComponent<Rigidbody2D>();
		inventory = gameObject.GetComponent<Inventory>();
		chef = gameObject.GetComponent<Chef>();
		bodyAnimator = body.GetComponent<Animator>();
	}

    void Update()
    {
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
			bool isHoldingItems = this.inventory.items.Count > 0;
			bool canCookWithResources = chef.CanCookWithExtraResources(pickupPoint.GetResources());

			if (!isHoldingItems || (isHoldingItems && canCookWithResources))
            {
				while (pickupPoint.GetItems().Count > 0)
				{
					Resource resource = pickupPoint.PopResource();
					inventory.AddItem(resource);
				}

				Recipe cookedRecipe = chef.TryCookAnyRecipe();
			}
		}
		else
		{
			while (this.inventory.items.Count > 0)
			{
				Resource resource = this.inventory.PopItem();
				pickupPoint.AddResource(resource);
			}
		}
	}
}
