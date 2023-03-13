using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	private static bool _isGameOver = false;

	public static PlayerController playerController;
	public bool isGameOver { get { return _isGameOver; } }

	private const string PLAYER_TAG = "Player";

	private void Awake()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		playerController = GameObject.Find(PLAYER_TAG).GetComponent<PlayerController>();
	}

	public void SetIsGameOverTrue()
	{
		_isGameOver = true;
	}
}
