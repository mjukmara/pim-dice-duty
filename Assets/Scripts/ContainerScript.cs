using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ContainerScript : MonoBehaviour
{
	public Canvas canvas;
	public Transform bottomLeft;
	public Transform topRight;
	RectTransform rt;

	public Vector2 bl;
	public Vector2 tr;

	void Start()
	{
		rt = gameObject.GetComponent<RectTransform>();
	}

    void LateUpdate()
    {
		bl = Camera.main.WorldToScreenPoint(bottomLeft.position);
		tr = Camera.main.WorldToScreenPoint(topRight.position);

		/* rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, bl.x, tr.x - bl.x); */
		/* rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, tr.y, tr.y - bl.y); */
		/* rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 2000, 0); */
		/* rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 2000, 0); */
    }
}
