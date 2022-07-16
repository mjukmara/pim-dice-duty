using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	public float timeToLive = 3.0f;
    void Start()
    {
		Destroy(gameObject, timeToLive);
    }
}
