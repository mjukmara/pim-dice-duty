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

	void Spawn(int addScore, Vector3 position, bool raw)
	{
		GameObject scoreInstance = Instantiate(score, position, Quaternion.identity);

		float newScore = addScore;
		if (!raw)
        {
			newScore *= Game.multiplier;
		}

		string suffix = "";
		if (newScore > 100) { suffix = "!"; }
		if (newScore >= 300) { suffix = "!!"; }
		if (newScore >= 600) { suffix = "!!!"; }
		if (newScore >= 1000) { suffix = "?!"; }
		if (newScore >= 2500) { suffix = "?!?!?!"; }

		string text = addScore + " X" + Game.multiplier + suffix;
		if (raw)
        {
			text = addScore + suffix;
		}
		scoreInstance.GetComponent<Score>().Set(text);

		Game.AddScore((int)newScore);
	}

	public static void SpawnScore(int addScore, Vector3 position, bool raw)
	{
		instance.Spawn(addScore, position, raw);
	}
}
