using UnityEngine;

public class FlipSpriteBasedOnVelocity : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    public bool defaultRightwards;
    public SpriteRenderer target;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = target ? target : GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.flipX = defaultRightwards ? _rigidbody2D.velocity.x < 0 : _rigidbody2D.velocity.x >= 0;
    }
}
