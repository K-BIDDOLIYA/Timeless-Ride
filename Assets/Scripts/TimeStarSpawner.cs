using System.Collections;
using UnityEngine;

public class ClockSpawner : MonoBehaviour
{
    public GameObject clockPrefab;

    [Header("Spawn Position")]
    public float spawnX = 15f;
    public float minY = -4f;
    public float maxY = 4f;

    [Header("Random Spawn Time")]
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Wait for a random amount of time
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            SpawnClock();
        }
    }

    void SpawnClock()
    {
        Vector3 spawnPos = new Vector3(
            spawnX,
            Random.Range(minY, maxY),
            0f);

        Instantiate(clockPrefab, spawnPos, Quaternion.identity);
    }
}