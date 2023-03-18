using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] enemies;
	[SerializeField]
	private GameObject powerUp;

	private float
		zEnemySpawn = 12.0f,
		xSpawnRange = 9.0f,
		zPowerUpRange = 5.0f,
		ySpawn = 0.75f,
		powerUpSpawnTime = 5.0f,
		enemySpawnTime = 1.0f,
		startDelay = 1.0f;

	private void SpawnPowerUp()
	{
		float randomX = Random.Range(-xSpawnRange, xSpawnRange);

		Vector3 spawnPos = new Vector3(randomX, ySpawn, zPowerUpRange);
		Instantiate(powerUp, spawnPos, powerUp.gameObject.transform.rotation);
	}
	private void SpawnRandomEnemy()
	{
		float randomX = Random.Range(-xSpawnRange, xSpawnRange);
		int randomIndex = Random.Range(0, enemies.Length);

		Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);
		Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
	}

	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating(nameof(SpawnPowerUp), startDelay, enemySpawnTime);
		InvokeRepeating(nameof(SpawnRandomEnemy), startDelay, powerUpSpawnTime);
	}
}
