using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{
	public enum AlignTo { ODD, EVEN };

	public AlignTo alignTo = AlignTo.ODD;
	public bool onStart = true;
	public bool onUpdate = true;

	void Start()
    {
		if (!this.onStart) return;
		this.alignLocalScaleToGrid();
	}
    void Update()
	{
		if (!this.onUpdate) return;
		this.alignLocalScaleToGrid();
	}

	void alignLocalScaleToGrid()
    {
		float factor = (this.alignTo == AlignTo.ODD ? 1f : 2f);
		transform.localScale = transform.localScale - new Vector3(
			transform.localScale.x % (factor / 16),
			transform.localScale.y % (factor / 16),
			transform.localScale.z
		);
	}
}
