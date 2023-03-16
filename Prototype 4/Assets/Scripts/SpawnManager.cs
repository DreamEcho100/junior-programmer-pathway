using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

	private const string POWER_UP_TAG = "PowerUp";

	private GameObject player;

	[SerializeField]
	private GameObject
		enemyPrefab,
		powerUpPrefab;

	[SerializeField]
	private float
		spawnPosRangeX = 9,
		spawnPosRangeZ = 9,
		randomPosRangeX,
		randomPosRangeZ;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Player");
		SpawnEnemyWave();
		ReSpawnPowerUpsWave();
	}

	void Update()
	{
		if (GameManager.instance.SpawnCountMonitor())
		{
			SpawnEnemyWave();
			ReSpawnPowerUpsWave();
		}
	}

	void SpawnEnemyWave()
	{
		if (!player) return;

		for (int i = 0; i < GameManager.instance.enemiesToSpawnCounter; i++)
		{
			Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
			GameManager.instance.enemiesSpawnedCounter++;
		}
	}
	void ReSpawnPowerUpsWave()
	{
		if (!player) return;

		GameObject[] powerUps = GameObject.FindGameObjectsWithTag(POWER_UP_TAG);
		foreach (GameObject powerUp in powerUps)
		{
			Destroy(powerUp);
		}

		PowerUpIndicatorController[] spawnPowerUps = FindObjectsOfType<PowerUpIndicatorController>();
		for (int i = 0; i < GameManager.instance.powerUpsToSpawnCounter; i++)
		{
			Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
			GameManager.instance.powerUpsSpawnedCounter++;
		}
	}

	private Vector3 GenerateSpawnPosition()
	{
		Vector3 spawnPosition;
		float distance;
		do
		{
			randomPosRangeX = Random.Range(-spawnPosRangeX, spawnPosRangeX);
			randomPosRangeZ = Random.Range(-spawnPosRangeZ, spawnPosRangeZ);

			spawnPosition = new Vector3(randomPosRangeX, 0.01f, randomPosRangeZ);
			distance = Vector3.Distance(spawnPosition, player.transform.position);
		} while (distance < player.transform.localScale.x);

		return spawnPosition;
	}
}


// private Vector3 GenerateSpawnPosition()
// {
// randomPosRangeX = Random.Range(-spawnPosRangeX, spawnPosRangeX);
// randomPosRangeZ = Random.Range(-spawnPosRangeZ, spawnPosRangeZ);

// return new Vector3(
// 	randomPosRangeX,
// 	0.01f,
// 	randomPosRangeZ
// );
// }