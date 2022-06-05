using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plant : MonoBehaviour
{
    public bool destroyed;

    private Collider2D _player;
    public float exposureTime;

    public float maxExposureTime;
    private Animator _animator;
    private static readonly int DestroyedParameter = Animator.StringToHash("Destroyed");
    private static readonly int ExposedParameter = Animator.StringToHash("Exposed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().flipX = Random.value < 0.5f;
    }

    private void Start()
    {
        // TODO if level 2 exists, determine which level you are in
        if (GameProgression.Instance.smithQuestProgress < GameProgression.SmithQuestProgress.DestroyedFirstBoss) return;
        
        Die();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>()) return;
        
        _player = col;
        _animator.SetBool(ExposedParameter, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != _player) return;
        
        _player = null;
        _animator.SetBool(ExposedParameter, false);
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
        _animator.SetBool(DestroyedParameter, true);
    }
}
