using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GUIFollow : MonoBehaviour
{
	public Transform target;

    void LateUpdate()
    {
		if (target == null) {
			return;
		}

		Vector2 sp = Camera.main.WorldToScreenPoint(target.position);

		this.transform.position = sp;
    }

	void Awake() {
		if (gameObject.activeInHierarchy) {
			LateUpdate();
		}
	}
}
