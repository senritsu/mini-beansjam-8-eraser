using UnityEngine;

public class Deadly : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();

        if (!player) return;

        player.TakeDamage();
    }
}
