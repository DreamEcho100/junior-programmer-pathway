using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static int
		_powerUpsSpawned = 0,
		_powerUpsToSpawnCounter = 1;
	private static int
		_enemiesSpawned = 0,
		_enemiesToSpawnCounter = 1;

	public static GameManager instance;
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

	public bool SpawnCountMonitor()
	{
		if (enemiesSpawnedCounter <= 0)
		{
			enemiesToSpawnCounter++;
			// enemiesSpawnedCounter = enemiesToSpawnCounter;

			powerUpsToSpawnCounter = (int)Mathf.Ceil(enemiesToSpawnCounter / 2);
			// powerUpsSpawnedCounter = powerUpsToSpawnCounter;

			return true;
		}

		return false;
	}
}
