using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static int
	_currentLevel = 1,
		_powerUpsSpawned = 0,
		_powerUpsToSpawnCounter = 1,
		_enemiesSpawned = 0,
		_enemiesToSpawnCounter = 1;
	private static int _bulletsSpawned = 0;

	public static GameManager instance;
	public int currentLevel
	{
		get { return _currentLevel; }
		set { if (value > 0) _currentLevel = value; }
	}
	public int bulletsSpawned
	{
		get { return _bulletsSpawned; }
		set { if (value >= 0) _bulletsSpawned = value; }
	}
	public int enemiesToSpawnCounter
	{
		get { return _enemiesToSpawnCounter; }
		set { _enemiesToSpawnCounter = value; }
	}
	public int enemiesSpawnedCounter
	{
		get { return _enemiesSpawned; }
		set { _enemiesSpawned = value; }
	}

	public int powerUpsToSpawnCounter
	{
		get { return _powerUpsToSpawnCounter; }
		set { _powerUpsToSpawnCounter = value; }
	}
	public int powerUpsSpawnedCounter
	{
		get { return _powerUpsSpawned; }
		set { _powerUpsSpawned = value; }
	}

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
}
