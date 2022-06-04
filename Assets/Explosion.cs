using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _time;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= 0.1f)
        {
            _collider.enabled = false;
        }

        if (_time >= 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
