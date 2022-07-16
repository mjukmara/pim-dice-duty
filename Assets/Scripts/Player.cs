using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject body;
	public float speed = 1000.0f;

	Rigidbody2D rb;
	Animator bodyAnimator;
	Inventory inventory;
	Chef chef;

	bool interact = false;

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
			interact = true;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			interact = false;
		}

		bodyAnimator.SetBool("holding", inventory.items.Count > 0);

		float speedx = Mathf.Abs(rb.velocity.x);
		bodyAnimator.SetFloat("velocityx", speedx);
		if (speedx < 0.1f)
			return;

		Vector3 bodyScale = body.transform.localScale;
		int scalex = rb.velocity.x > 0 ? 1 : -1;
		body.gameObject.transform.localScale = new Vector3(scalex, bodyScale.y, bodyScale.z);
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
				while (pickupPoint.items.Count > 0)
				{
					Resource resource = pickupPoint.PopResource();
					inventory.AddItem(resource);
				}

				Recipe cookedRecipe = chef.TryCookAnyRecipe();
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
}
