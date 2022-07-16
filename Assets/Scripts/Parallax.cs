using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	public float distance = 1.0f;
	public float velocity = 10.0f;
	public bool wrap = true;
	public float wrapDistance = 15.0f;


    void Start()
    {
		float initialScale = transform.localScale.x;
		float scale = (1.0f / Mathf.Sqrt(distance)) * initialScale;
		transform.localScale = new Vector3(scale, scale, scale);

		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(velocity * distance, 0);
    }

    void Update()
    {
		float distance = Mathf.Abs(transform.position.x);
		if (distance > wrapDistance && wrap)
		{
			transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
		}
    }
}
