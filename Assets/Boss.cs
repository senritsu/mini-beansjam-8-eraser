using System;
using System.Collections;
using Droni;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Boss : MonoBehaviour
{
    public LevelDefinition levelDefinition;
    public float spawnInterval;
    public int spawnCount;
    public int health;
    private Player _player;
    public bool isAwake;
    private SpriteRenderer _renderer;
    private Color _defaultColor;
    private static readonly int IsAwakeParameter = Animator.StringToHash("IsAwake");
    private Animator _animator;
    private MoveTowardsPlayer _moveTowardsPlayer;
    public GameObject deathEffect;

    private void Awake()
    {
        _moveTowardsPlayer = GetComponent<MoveTowardsPlayer>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
        _renderer = GetComponent<SpriteRenderer>();
        
        _defaultColor = _renderer.color;
        _renderer.color = new Color(_defaultColor.grayscale, _defaultColor.grayscale, _defaultColor.grayscale);
    }

    private void Start()
    {
        // check for scene name is not pretty, but it should work for now
        // if boss is disabled, portal will open
        if (GameProgression.Instance.smithQuestProgress == GameProgression.SmithQuestProgress.DestroyedFirstBoss && SceneManager.GetActiveScene().name == "Green")
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isAwake) return;
        
        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= 15f)
        {
            WakeUp();
        }
    }

    private void WakeUp()
    {
        isAwake = true;
        _renderer.color = _defaultColor;
        _animator.SetBool(IsAwakeParameter, true);
        
        StartCoroutine(BossBehaviour());
    }

    private IEnumerator BossBehaviour()
    {
        DialogMaster.master.PrintDialog("DU SOLLTEST NICHT HIER SEIN!!!",Color.red, 3f);
        
        yield return new WaitForSeconds(1.5f);
        
        _moveTowardsPlayer.enabled = true;
        
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            var groupPosition = (Vector3)Random.insideUnitCircle * 6;
            var bossSafetyZone = groupPosition.normalized * 4;

            var temporaryPosition = groupPosition + bossSafetyZone;

            var offsetToPlayer = temporaryPosition - _player.transform.position;
            if (offsetToPlayer.magnitude <= 2)
            {
                temporaryPosition += offsetToPlayer.normalized * 2;
            }

            for (var i = 0; i < spawnCount; i++)
            {
                var enemy = levelDefinition.enemies[Random.Range(0, levelDefinition.enemies.Length)];
                var localPosition = (Vector3)Random.insideUnitCircle * 2;
                Instantiate(enemy, transform.position + temporaryPosition + localPosition, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var damage = col.GetComponent<Damage>()?.amount;
        if (damage != null)
        {
            health -= damage.Value;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        
        var container = new GameObject();
        container.transform.position = transform.position;
        container.transform.localScale = Vector3.one * 4;
        for (var i = 0; i < 3; i++)
        {
            var instance = Instantiate(deathEffect, transform.position + (Vector3)Random.insideUnitCircle * 2, Quaternion.identity);
            instance.transform.SetParent(container.transform, true);
        }

        if (GameProgression.Instance.smithQuestProgress < GameProgression.SmithQuestProgress.DestroyedFirstBoss)
        {
            GameProgression.Instance.smithQuestProgress =
                GameProgression.SmithQuestProgress.DestroyedFirstBoss;
        }

        FindObjectOfType<Exterminator>().Exterminate();
    }
}
