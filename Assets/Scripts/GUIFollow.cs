using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GUIFollow : MonoBehaviour
{
	public Transform target;

    void Update()
    {
		if (target == null) {
			return;
		}

		Vector2 sp = Camera.main.WorldToScreenPoint(target.position);

		this.transform.position = sp;
    }

	void OnEnable() {
		if (gameObject.activeInHierarchy) {
			Update();
		}
	}
}
