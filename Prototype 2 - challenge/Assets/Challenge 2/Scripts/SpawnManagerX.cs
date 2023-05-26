using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{

    [SerializeField]
    private float
        spawnLimitXLeft = -22,
        spawnLimitXRight = 7,
        spawnPosY = 40,
        startDelay,
        minStartDelay = 0.0f,
        maxStartDelay = 2.0f,
        spawnInterval,
        minSpawnInterval = 0.0f,
        maxSpawnInterval = 4.0f;


    private int ballPrefabIndex;

    [SerializeField]
    private GameObject[] ballPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        startDelay = GetRandomStartDelay();
        spawnInterval = GetRandomSpawnInterval();
        InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        ballPrefabIndex = Random.Range(0, ballPrefabs.Length);

        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[ballPrefabIndex], spawnPos, ballPrefabs[ballPrefabIndex].transform.rotation);


        CancelInvoke();
        startDelay = GetRandomStartDelay();
        spawnInterval = GetRandomSpawnInterval();
        InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }

    private float GetRandomSpawnInterval()
    {
        return Random.Range(maxSpawnInterval, maxSpawnInterval);
    }

    private float GetRandomStartDelay()
    {
        return Random.Range(maxStartDelay, maxStartDelay);
    }

}
