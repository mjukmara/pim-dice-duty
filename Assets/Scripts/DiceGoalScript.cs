using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGoalScript : MonoBehaviour
{
	public List<Sprite> diceSprites;
	public List<SpriteRenderer> diceRenderers;

	public void Set(Color diceColor, int diceValue)
	{
		for (int i = 0; i < diceRenderers.Count; i++)
		{
			diceRenderers[i].color = diceColor;
			diceRenderers[i].sprite = diceSprites[i];

		}
		diceRenderers[0].sprite = diceSprites[diceValue];
	}
}
