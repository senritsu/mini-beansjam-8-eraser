using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Player _player;
    public float speed;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        var direction = _player.transform.position - transform.position;
        _rigidbody2D.AddForce(direction.normalized * speed);
    }
}
