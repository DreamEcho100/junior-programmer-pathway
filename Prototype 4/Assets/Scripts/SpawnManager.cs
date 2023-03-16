using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	private GameObject player;

	[SerializeField]
	private GameObject enemyPrefab;
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
		Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
	}

	// Update is called once per frame
	void Update()
	{

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