using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	public TMPro.TextMeshProUGUI text;
	public float velocity = 5.0f;

    void Awake()
    {
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

		float x = Random.Range(-velocity, velocity);
		Vector2 newVelocity = new Vector2(x, velocity);
		newVelocity.Normalize();
		rb.velocity = newVelocity;
    }

	public void Set(string text)
	{
		this.text.text = text;
	}
}
