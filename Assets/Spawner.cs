using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public LevelDefinition levelDefinition;
    private Plant _plant;
    public float spawnInterval;

    public int plantSpawnCountNear;
    public int plantSpawnCountFar;
    public float plantSpawnRadiusNear;
    public float plantSpawnRadiusFar;

    private void Awake()
    {
        _plant = GetComponent<Plant>();
    }

    private void Start()
    {
        SpawnPlants(plantSpawnCountNear, plantSpawnRadiusNear);
        SpawnPlants(plantSpawnCountFar, plantSpawnRadiusFar);
        
        StartCoroutine(SpawnLoop());
    }

    private void SpawnPlants(int count, float radius)
    {
        for (var i = 0; i < count; i++)
        {
            Instantiate(levelDefinition.plants[Random.Range(0, levelDefinition.plants.Length)],
                transform.position + (Vector3)Random.insideUnitCircle * radius, Quaternion.identity);
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (_plant.destroyed) break;

            var enemy = levelDefinition.enemies[Random.Range(0, levelDefinition.enemies.Length)];
            var v = Random.insideUnitCircle;
            Instantiate(enemy, transform.position + (Vector3)(v.normalized * 2 + v * 5), Quaternion.identity);
        }
    }
}
