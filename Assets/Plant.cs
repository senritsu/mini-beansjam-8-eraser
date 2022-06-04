using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private SpriteRenderer _renderer;
    
    public bool destroyed;

    private Collider2D _player;
    public float exposureTime;

    public PlantDefinition plantDefinition;
        
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = plantDefinition.defaultColor;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>())
        {
            _player = col;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == _player)
        {
            _player = null;
        }
    }

    private void Update()
    {
        if (destroyed) return;
        
        if (_player)
        {
            exposureTime += Time.deltaTime;
        }

        if (exposureTime >= plantDefinition.maxExposureTime)
        {
            Die();
        }
    }

    private void Die()
    {
        destroyed = true;
        _renderer.color = plantDefinition.destroyedColor;
    }
}
