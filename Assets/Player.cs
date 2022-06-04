using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public GameObject projectile;
    public GameObject crossHair;
    public float speed;
    public float shotSpeed;
    public float shotSpeedDeviation;
    public AudioClip shotSound;
    
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

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

        if (shotSound)
        {
            _audioSource.PlayOneShot(shotSound);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _direction * speed;
    }
}
