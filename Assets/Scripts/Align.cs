using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{
    void Update()
	{
		transform.localScale = transform.localScale - new Vector3(
			transform.localScale.x % (2.0f / 16),
			transform.localScale.y % (2.0f / 16),
			transform.localScale.z
		);
	}
}
