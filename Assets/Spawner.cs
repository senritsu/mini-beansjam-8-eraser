using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public LevelDefinition levelDefinition;
    private Plant _plant;
    public float spawnInterval;

    private void Awake()
    {
        _plant = GetComponent<Plant>();
        StartCoroutine(SpawnLoop());
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
