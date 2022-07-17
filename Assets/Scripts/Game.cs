using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public GameObject celebration;
	public static int multiplier = 1;
	public static int level = 1;

	static Game instance;
	public TMPro.TextMeshProUGUI scoreText;
	public TMPro.TextMeshProUGUI targetScoreText;
	public TMPro.TextMeshProUGUI multiplierText;
	public TMPro.TextMeshProUGUI levelText;

	public int score = 0;
	int renderedScore = 0;
	float scoreTickRate = 16f;
	float scoreTickTimer = 0f;
	public int targetScore = 20000;

	bool win = false;

	Animator animator;

    void Start()
    {
		instance = this;
		animator = gameObject.GetComponent<Animator>();
		targetScoreText.text = "Target: " + targetScore;
    }

    void Update()
    {
		scoreTickTimer += Time.deltaTime;
		if (scoreTickTimer > 1f/scoreTickRate)
        {
			scoreTickTimer = 0f;
			renderedScore += Mathf.CeilToInt((score - renderedScore) / 5f);
		}

		scoreText.text = "Score: " + renderedScore;
		multiplierText.text = multiplier + "X";
		levelText.text = "Level: " + level;

		if (score >= targetScore && !win)
		{
			win = true;
			StartCoroutine(WinSequence());
		}
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

	IEnumerator WinSequence()
	{
		Instantiate(celebration, Vector3.zero, Quaternion.identity);
		AudioManager.Instance.PlaySfx("Win");

		yield return new WaitForSeconds(1);

		animator.ResetTrigger("Win");
		animator.SetTrigger("Win");

		yield return new WaitForSeconds(1);

		Win();
	}

	public static void Reset()
	{

	}

	public void Close()
	{
		Application.Quit();
	}
}
