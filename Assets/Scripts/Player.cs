using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 1000.0f;

	Rigidbody2D rb;
	Inventory inventory;
	Chef chef;

	bool interact = false;

    void Start()
    {
		rb = gameObject.GetComponent<Rigidbody2D>();
		inventory = gameObject.GetComponent<Inventory>();
		chef = gameObject.GetComponent<Chef>();
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
			interact = true;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			interact = false;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "PickupPoint" && interact)
		{
			this.interact = false;
			// Do pickup stuff
			PickupPoint pickupPoint = other.gameObject.GetComponent<PickupPoint>();
			if (pickupPoint.GetItems().Count > 0)
            {
				List<Resource> resources = pickupPoint.GetItems();
				if (resource)
				{
					inventory.AddItem(resource);

					Recipe cookedRecipe = chef.TryCookAnyRecipe();
				}
			}
			else
			{
				while (this.inventory.items.Count > 0) {
					Resource resource = this.inventory.PopItem();
					pickupPoint.AddResource(resource);
				}
			}
			
		}
	}
}
