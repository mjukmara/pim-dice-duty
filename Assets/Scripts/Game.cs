using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	static Game instance;
	public Animator animator;

    void Start()
    {
		instance = this;
		animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

    }

	public void StartGame()
	{
		instance.animator.ResetTrigger("Start");
		instance.animator.SetTrigger("Start");
	}
}
