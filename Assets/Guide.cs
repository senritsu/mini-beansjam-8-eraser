using UnityEngine;

public class Guide : MonoBehaviour
{
    private Portal _portal;
    private Player _player;
    private ParticleSystem _particleSystem;
    public float force;

    // Update is called once per frame
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _player = FindObjectOfType<Player>();
        _portal = FindObjectOfType<Portal>();
    }

    private void Update()
    {
        var toPortal = _portal.transform.position - _player.transform.position;
        var distanceToPortal = toPortal.magnitude;
        
        transform.localPosition = toPortal.normalized * 3f;

        var velocityOverLifetime = _particleSystem.velocityOverLifetime;
        var v = toPortal.normalized * force;
        velocityOverLifetime.x = v.x;
        velocityOverLifetime.y = v.y;

        if (distanceToPortal >= 12 && !_particleSystem.isEmitting)
        {
            _particleSystem.Play();
        }

        if (distanceToPortal < 12 && !_particleSystem.isEmitting)
        {
            _particleSystem.Stop();
        }
    }
}
