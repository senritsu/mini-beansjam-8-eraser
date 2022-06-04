using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public GameObject projectile;
    public GameObject crossHair;
    public GameObject guide;
    public float speed;
    public float shotSpeed;
    public float shotSpeedDeviation;
    
    private Camera _camera;
    private Portal _portal;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    // Start is called before the first frame update
    private void Awake()
    {
        _camera = Camera.main;
        _portal = FindObjectOfType<Portal>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        var toPortal = _portal.transform.position - transform.position;
        var distanceToPortal = toPortal.magnitude;
        
        guide.SetActive(distanceToPortal >= 12);
        guide.transform.localPosition = toPortal.normalized * 3f;

        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var shot = Instantiate(projectile, transform.position, Quaternion.identity);

        var deviation = (Random.value - 0.5f) * shotSpeedDeviation;
        
        shot.GetComponent<Rigidbody2D>().velocity =
            (crossHair.transform.position - transform.position).normalized * (shotSpeed + deviation);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _direction * speed;
    }
}
