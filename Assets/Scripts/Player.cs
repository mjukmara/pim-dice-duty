using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 1000.0f;

	Rigidbody2D rb;

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
    }

	void OnColliderStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "PickupPoint" && Input.GetKeyDown(KeyCode.Space))
		{
			// Do pickup stuff
		}
	}
}
