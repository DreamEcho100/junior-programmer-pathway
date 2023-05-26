using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static float _gameSpeed = 1.0f;
	private static bool _isGameOver = false;
	private static bool _isGameStarting = false;
	private static float _score = 0;
	private static float _volumeDownRate = 0.15f;

	public float gameSpeed
	{
		get { return _gameSpeed; }
		set
		{
			if (value > 0)
				_gameSpeed = value;
		}
	}
	public static GameManager instance;
	public bool isGameOver { get { return _isGameOver; } }
	public bool isGameStarting
	{
		get { return _isGameStarting; }
		// set { _isGameStarting = value; }
	}
	public static PlayerController playerController;
	public float score { get { return _score; } }
	public float volumeDownRate { get { return _volumeDownRate; } }

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

	public void SetIsGameOverTrue()
	{
		_isGameStarting = false;
		_isGameOver = true;
	}
	public void StartGame()
	{
		_isGameStarting = true;
		_isGameOver = false;
	}

	void Start()
	{
		playerController = GameObject.Find(PLAYER_TAG).GetComponent<PlayerController>();
	}

	void Update()
	{
		if (_isGameOver || !isGameStarting) return;

		_score += Time.deltaTime * _gameSpeed;

		Debug.Log($"Score: {(int)_score}");
	}
}
