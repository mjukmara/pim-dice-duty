using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 1000.0f;

	Rigidbody2D rb;

	bool interact = false;

    void Start()
    {
		transform.localScale = transform.localScale - new Vector3(
			transform.localScale.x % (1.0f / 16),
			transform.localScale.y % (1.0f / 16),
			transform.localScale.z % (1.0f / 16)
		);
		rb = gameObject.GetComponent<Rigidbody2D>();
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
			Debug.Log("Space pressed");
			interact = true;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Debug.Log("Space released");
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
			Resource resource = pickupPoint.RemoveResource();
			if (resource)
			{
				Debug.Log("Picked up resource:" + resource.label);
			}
		}
	}
}
