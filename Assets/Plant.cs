using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool destroyed;

    private Collider2D _player;
    public float exposureTime;

    public float maxExposureTime;
    private Animator _animator;
    private static readonly int Destroyed = Animator.StringToHash("Destroyed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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

        if (exposureTime >= maxExposureTime)
        {
            Die();
        }
    }

    private void Die()
    {
        destroyed = true;
        _animator.SetBool(Destroyed, true);
    }
}
