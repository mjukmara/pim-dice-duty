using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
	public Transform bottomLeft;
	public Transform topRight;
	RectTransform rt;

	void Start()
	{
		rt = gameObject.GetComponent<RectTransform>();
	}

    void LateUpdate()
    {
		Vector2 bl = Camera.main.WorldToScreenPoint(bottomLeft.position);
		Vector2 tr = Camera.main.WorldToScreenPoint(topRight.position);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 4, 0);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 4, 0);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 4, 0);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 4, 0);
    }
}
