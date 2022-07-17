using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{
	static ScoreSpawner instance;
	public GameObject score;

	void Start()
	{
		instance = this;
	}

	void Spawn(int addScore, Vector3 position)
	{
		GameObject scoreInstance = Instantiate(score, position, Quaternion.identity);

		float newScore = addScore * Game.multiplier;

		string suffix = "";
		if (newScore > 100) { suffix = "!"; }
		if (newScore >= 300) { suffix = "!!"; }
		if (newScore >= 600) { suffix = "!!!"; }
		if (newScore >= 1000) { suffix = "?!"; }
		if (newScore >= 2500) { suffix = "?!?!?!"; }

		string text = addScore + " X" + Game.multiplier + suffix;
		scoreInstance.GetComponent<Score>().Set(text);

		Game.AddScore(addScore * Game.multiplier);
	}

	public static void SpawnScore(int addScore, Vector3 position)
	{
		instance.Spawn(addScore, position);
	}
}
