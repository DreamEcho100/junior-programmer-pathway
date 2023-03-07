using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] animalPrefabs;
    [SerializeField]
    private float spawnRangeX = 18, spawnRangeZ = 20, minSpawnTime = 0, maxSpawnTime = 3;

    private int animalIndex;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAnimals());
    }

    IEnumerator SpawnAnimals()
    {

        while (true)
        {
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
            animalIndex = Random.Range(0, animalPrefabs.Length);
            Instantiate(
                animalPrefabs[animalIndex],
                new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ),
                animalPrefabs[animalIndex].transform.rotation
            );

        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}