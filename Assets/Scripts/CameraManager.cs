using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager instance;

	public float shakeMult = 1.0f;

	float shakeIntensity = 0.0f;
	float shakeTime = 0.0f;
	Vector3 shakeOffset;

	GameObject cameraObject;
	Vector3 offset;

    void Start()
    {
		Time.timeScale = 1.0f;
		cameraObject = gameObject.transform.GetChild(0).gameObject;
		offset = cameraObject.transform.position;

		instance = this;
    }

    void Update()
    {
		shakeOffset = Vector3.zero;

		if (shakeTime > 0.0f)
		{
			shakeTime = Mathf.Max(shakeTime - Time.deltaTime, 0.0f);

			if (shakeTime == 0.0f)
				shakeIntensity = 0.0f;

			shakeOffset.x = Random.value * shakeIntensity;
			shakeOffset.y = Random.value * shakeIntensity;
		}

		cameraObject.transform.position = offset + shakeOffset * shakeMult;
    }

	public void Shake(float time, float intensity)
	{
		if (intensity > shakeIntensity)
		{
			shakeTime = time;
			shakeIntensity = intensity;
		}
	}

	public void Freeze(float length)
	{
		StartCoroutine(FreezeEvent(length));
	}

	IEnumerator FreezeEvent(float length)
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(length);
		Time.timeScale = 1.0f;
	}
}
