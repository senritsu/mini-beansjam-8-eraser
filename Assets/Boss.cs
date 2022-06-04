using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Boss : MonoBehaviour
{
    public LevelDefinition levelDefinition;
    public float spawnInterval;
    public int spawnCount;
    public int health;
    private Player _player;
    public bool isAggressive;
    private SpriteRenderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _renderer = GetComponent<SpriteRenderer>();

        _defaultColor = _renderer.color;
        _renderer.color = new Color(_defaultColor.grayscale, _defaultColor.grayscale, _defaultColor.grayscale);
    }

    private void Update()
    {
        if (isAggressive) return;
        
        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= 15f)
        {
            BecomeAggressive();
        }
    }

    private void BecomeAggressive()
    {
        isAggressive = true;
        _renderer.color = _defaultColor;
        GetComponent<MoveTowardsPlayer>().enabled = true;
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            var groupPosition = (Vector3)Random.insideUnitCircle * 6;
            var bossSafetyZone = groupPosition.normalized * 4;

            var temporaryPosition = groupPosition + bossSafetyZone;

            var offsetToPlayer = temporaryPosition - _player.transform.position;
            if (offsetToPlayer.magnitude <= 2)
            {
                temporaryPosition += offsetToPlayer.normalized * 2;
            }

            for (var i = 0; i < spawnCount; i++)
            {
                var enemy = levelDefinition.enemies[Random.Range(0, levelDefinition.enemies.Length)];
                var localPosition = (Vector3)Random.insideUnitCircle * 2;
                Instantiate(enemy, transform.position + temporaryPosition + localPosition, Quaternion.identity);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var damage = col.GetComponent<Damage>()?.amount;
        if (damage != null)
        {
            health -= damage.Value;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}