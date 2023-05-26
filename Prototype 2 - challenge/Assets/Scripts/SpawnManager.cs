using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] animalPrefabs;
    [SerializeField]
    private float
        spawnRangeXMin = -18f,
        spawnRangeXMax = 18f,
        spawnRangeY = 0f,
        spawnRangeZMin = 25f,
        spawnRangeZMax = 25f,
        minSpawnTime = 2f,
        maxSpawnTime = 6f;
    [SerializeField]
    private Vector3 spawnRotation = new Vector3(0f, 180f, 0f);
    
    private int animalIndex;

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomAnimal();
        StartCoroutine(SpawnRandomAnimalCoroutine());
    }

    IEnumerator SpawnRandomAnimalCoroutine()
    {

        while (true)
        {
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
            SpawnRandomAnimal();
        }
    }
    void SpawnRandomAnimal()
    {
        animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(
            animalPrefabs[animalIndex],
            new Vector3(
                Random.Range(spawnRangeXMin, spawnRangeXMax),
                spawnRangeY,
                Random.Range(spawnRangeZMin, spawnRangeZMax)),
            Quaternion.Euler(spawnRotation)
        );
    }

    // Update is called once per frame
    void Update()
    {
    }
}
