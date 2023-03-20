using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

	private const string POWER_UP_TAG = "PowerUp";

	private GameObject player;
	private BossEnemyController bossEnemyController;
	private bool _isBossOn;

	public bool IsBossOn
	{
		get { return _isBossOn; }
		set { _isBossOn = value; }
	}

	[SerializeField]
	private GameObject[]
		enemiesPrefabs,
		powerUpsPrefabs;
	[SerializeField]
	private GameObject bossEnemyPrefabs;

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
		// bossEnemyController = bossEnemyPrefabs.GetComponent<BossEnemyController>();
		SpawnEnemyWave(GameManager.instance.enemiesToSpawnCounter);
		ReSpawnPowerUpsWave(GameManager.instance.powerUpsToSpawnCounter);
	}

	void Update()
	{
		if (GameManager.instance.enemiesSpawnedCounter <= 0)
		{
			IsBossOn = false;
			GameManager.instance.enemiesToSpawnCounter++;
			GameManager.instance.powerUpsToSpawnCounter = (int)Mathf.Ceil(GameManager.instance.enemiesToSpawnCounter / 2);
			SpawnEnemyWave(GameManager.instance.enemiesToSpawnCounter);
			ReSpawnPowerUpsWave(GameManager.instance.powerUpsToSpawnCounter);
			GameManager.instance.currentLevel++;
		}
	}

	void FixedUpdate()
	{
		if (!IsBossOn && GameManager.instance.currentLevel % 3 == 0)
		{
			GameManager.instance.enemiesToSpawnCounter++;
			bossEnemyController =
				Instantiate(bossEnemyPrefabs, GenerateSpawnPosition(), bossEnemyPrefabs.transform.rotation)
				.GetComponent<BossEnemyController>(); ;
			bossEnemyController.isOn = true;
			IsBossOn = bossEnemyController.isOn;
			bossEnemyController.spawnTime = bossEnemyController.spawnTimeBase + ((GameManager.instance.currentLevel % 3) * 0.25f);
		}
	}

	public void SpawnEnemyWave(int spawnCounter)
	{
		if (!player) return;
		int randomEnemyPrefabIndex;

		for (int i = 0; i < spawnCounter; i++)
		{
			randomEnemyPrefabIndex = Random.Range(0, enemiesPrefabs.Length);
			Instantiate(enemiesPrefabs[randomEnemyPrefabIndex], GenerateSpawnPosition(), enemiesPrefabs[randomEnemyPrefabIndex].transform.rotation);
			GameManager.instance.enemiesSpawnedCounter++;
		}
	}
	public void SpawnPowerUpsWave(int spawnCounter)
	{
		if (!player) return;
		int randomPowerUpsPrefabIndex;

		for (int i = 0; i < spawnCounter; i++)
		{
			randomPowerUpsPrefabIndex = Random.Range(0, powerUpsPrefabs.Length);
			Instantiate(
				powerUpsPrefabs[randomPowerUpsPrefabIndex],
				GenerateSpawnPosition(),
				powerUpsPrefabs[randomPowerUpsPrefabIndex].transform.rotation
			);
			GameManager.instance.powerUpsSpawnedCounter++;
		}
	}
	public void ReSpawnPowerUpsWave(int spawnCounter)
	{
		// if (!player) return;
		GameObject[] powerUps = GameObject.FindGameObjectsWithTag(POWER_UP_TAG);
		foreach (GameObject powerUp in powerUps)
			Destroy(powerUp);

		SpawnPowerUpsWave(spawnCounter);

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