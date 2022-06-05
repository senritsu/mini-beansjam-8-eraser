using System;
using System.Collections.Generic;
using Droni;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    private HashSet<Plant> _energySources = new();
    public string[] deathLines;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Damage>()?.amount != null)
        {
            Die();
        }

        var plant = other.GetComponent<Plant>();
        if (!plant || plant.destroyed) return;
        
        plant.OnDeath += Disconnect;
        _energySources.Add(plant);
    }

    private void Disconnect(object plant, EventArgs args)
    {
        _energySources.Remove(plant as Plant);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var plant = other.GetComponent<Plant>();
        if (other.GetComponent<Plant>())
        {
            _energySources.Remove(plant);
        }
    }

    private void Update()
    {
        if (_energySources.Count == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        if (Random.value <= 0.01f)
        {
            DialogMaster.master.PrintDialog(deathLines[Random.Range(0, deathLines.Length)],Color.red, 2f);
        }
    }
}
