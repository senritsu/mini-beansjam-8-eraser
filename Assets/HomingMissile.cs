using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{

    public Enemy target;

    public float force;

    public float lifeTime = 5.0F;

    public float explosionRange = 5.0F;

    public GameObject explosion;

    private Rigidbody2D _rigidbody2D;

    private float time;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > lifeTime)
        {
            Destroy(this.gameObject);
        }

        Explode();
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (target)
        {
            _rigidbody2D.AddForce((target.transform.position - transform.position).normalized * force,ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.Equals(target.gameObject))
        {
            target.Die();
            Explode();
        }
    }
    
    private void Explode()
    {
        var enemies = FindObjectsOfType<Enemy>()
            .Where(enemy => Vector2.Distance(enemy.transform.position, transform.position) < explosionRange)
            .ToList();

        if (enemies.Count > 0)
        {
            enemies.ForEach(enemy => enemy.Die());
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        }
    }
}
