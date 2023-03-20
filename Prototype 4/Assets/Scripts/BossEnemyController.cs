using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : MonoBehaviour
{
	private SpawnManager spawnManager;
	public bool _isOn = false;
	private float
		spawnTimeMultiplier = 3.0f,
		_spawnTime,
		_spawnTimeBase = 1.5f;

	public bool isOn
	{
		get { return _isOn; }
		set { _isOn = value; }
	}
	public float spawnTime
	{
		get { return _spawnTime; }
		set { _spawnTime = value; }
	}
	public float spawnTimeBase
	{ get { return _spawnTimeBase; } }

	// Start is called before the first frame update
	void Start()
	{
		spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
		StartCoroutine(nameof(SpawnMinions));
	}

	public IEnumerator SpawnMinions()
	{
		yield return new WaitForSeconds(spawnTime);
		if (isOn)
		{
			int spawnMinionCount = GameManager.instance.currentLevel % 3;
			GameManager.instance.enemiesToSpawnCounter++;
			GameManager.instance.powerUpsToSpawnCounter = GameManager.instance.powerUpsToSpawnCounter++;
			spawnManager.SpawnEnemyWave(spawnMinionCount);
			spawnManager.SpawnPowerUpsWave(spawnMinionCount);
			spawnTime *= spawnTimeMultiplier;
			StartCoroutine(nameof(SpawnMinions));
		}

	}

	void OnDestroy()
	{
		isOn = false;
		StopCoroutine(nameof(SpawnMinions));
	}
}
