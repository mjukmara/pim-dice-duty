using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public static int multiplier = 1;

	static Game instance;
	public TMPro.TextMeshProUGUI scoreText;

	int score = 0;

	Animator animator;

    void Start()
    {
		instance = this;
		animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
		scoreText.text = "Score: " + score;
    }

	public void StartGame()
	{
		instance?.animator.ResetTrigger("Start");
		instance?.animator.SetTrigger("Start");
	}

	public static void AddScore(int amount)
	{
		instance.score += amount;
	}

	public static void Win()
	{
		SceneManager.LoadScene("Win");
	}

	public static void Reset()
	{

	}

	public void Close()
	{
		Application.Quit();
	}
}
