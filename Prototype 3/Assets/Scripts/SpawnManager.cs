using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField]
	private GameObject obstaclePrefab;

	[SerializeField]
	private UnityEngine.Vector3 spawnPos = new UnityEngine.Vector3(25, 0.01f, 0);

	[SerializeField]
	private float
		startDelay = 2,
		repeatRate = 2;

	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
	}

	// Update is called once per frame
	void Update()
	{
	}

	void SpawnObstacle()
	{
		Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);

		if (GameManager.instance.isGameOver) CancelInvoke(nameof(SpawnObstacle));
	}
}
