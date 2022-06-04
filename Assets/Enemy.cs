using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Damage>()?.amount != null)
        {
            Destroy(gameObject);
        }
    }
}
