using System;
using System.Collections;
using System.Linq;
using Droni;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public GameObject projectile;
    public GameObject homingMissile;
    public GameObject crossHair;
    public float speed;
    public float shotSpeed;
    public float shotSpeedDeviation;
    public AudioClip shotSound;
    
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private AudioSource _audioSource;


    public AudioSource chargeSound;
    public float chargingThreshold = 0.5F;
    private float audioTimer = 0.0F;
    private int chargeAmount = 25;
    private int chargeCounter = 0;
    private bool charging = false;

    public int RespawnsRemaining;
    private GameObject _respawnMarkers;
    private static readonly int MovingParameter = Animator.StringToHash("Moving");
    private Animator _animator;
    private static readonly int FadeOutDeathParameter = Animator.StringToHash("FadeOutDeath");
    private bool _dead;

    public string[] respawnLines;

    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _respawnMarkers = transform.Find("RespawnMarkers").gameObject;

        if (SceneManager.GetActiveScene().name == "Hub")
        {
            _respawnMarkers.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        _animator.SetBool(MovingParameter, _direction.magnitude >= double.Epsilon);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButton(1))
        {
            Charging();
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            chargeSound.Stop();
            if (chargeCounter >= chargeAmount)
                HomingMissile();
            chargeCounter = 0;
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

    private void Charging()
    {
        audioTimer += Time.deltaTime;
        if (audioTimer > 0.5F)
        {
            float progress = (audioTimer+ chargeCounter) * (chargeSound.clip.length / chargeAmount / 2);
            if (progress> chargeSound.clip.length - 0.5F)
            {
                chargeSound.time = chargeSound.clip.length - 0.5F;
            }
            else
            {
                chargeSound.time = chargeCounter * (chargeSound.clip.length / chargeAmount / 2);
            }
            chargeSound.Play();
            audioTimer = 0.0F;
        }
        switch (chargeCounter % 4)
        {
            case 0:
                if (Input.GetAxis("Mouse X") > chargingThreshold)
                    chargeCounter++;
                break;
            case 1:
                if (Input.GetAxis("Mouse Y") > chargingThreshold)
                    chargeCounter++;
                break;
            case 2:
                if (Input.GetAxis("Mouse X") < chargingThreshold)
                    chargeCounter++;
                break;
            case 3:
                if (Input.GetAxis("Mouse Y") < chargingThreshold)
                    chargeCounter++;
                break;
        }

        
    }
    
    private void HomingMissile()
    {
        var shot = Instantiate(homingMissile, transform.position, Quaternion.Euler(crossHair.transform.position - transform.position));

        var enemy = GameObject.FindObjectsOfType<Enemy>().ToList().
            OrderBy(enemy => Vector2.Distance(crossHair.transform.position, enemy.transform.position))
            .ToList();

        if (enemy.Count == 0)
            Destroy(gameObject);
        
        shot.GetComponent<HomingMissile>().target = enemy.First();
        Vector2 sideways = Vector2.Perpendicular((crossHair.transform.position - transform.position).normalized) * (Random.Range(0,2)*2-1);
        shot.GetComponent<Rigidbody2D>().AddForce(sideways * Random.Range(-1,1) * 2, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _direction * speed;
    }

    public void TakeDamage()
    {
        if (_dead) return;
        
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        _dead = true;
        
        var fadeAnimator = GameObject.Find("GhettoCameraFade").GetComponent<Animator>();
        fadeAnimator.SetTrigger(FadeOutDeathParameter);
        
        yield return new WaitForSeconds(1);
        
        if (RespawnsRemaining > 0)
        {
            ResetToLastCheckpoint();
            
            RespawnsRemaining--;
            Destroy(_respawnMarkers.transform.GetChild(0).gameObject);
            _dead = false;

            yield return new WaitForSeconds(1);
            
            DialogMaster.master.PrintDialog(respawnLines[Random.Range(0, respawnLines.Length)],Color.gray, 4f);
        }
        else
        {
            ReturnToHub();
        }
    }
    
    private void ResetToLastCheckpoint()
    {
        var lastActiveCheckpoint = FindObjectsOfType<Checkpoint>()
            .Where(x => x.IsActive)
            .OrderBy(x => x.checkpoint)
            .Last();

        transform.position = lastActiveCheckpoint.transform.position;
    }

    private void ReturnToHub()
    {
        if (GameProgression.Instance.smithQuestProgress == GameProgression.SmithQuestProgress.TalkedForTheFirstTime)
        {
            GameProgression.Instance.smithQuestProgress = GameProgression.SmithQuestProgress.ForcedBackToHub;
        }

        SceneManager.LoadScene("Hub");
    }
}
