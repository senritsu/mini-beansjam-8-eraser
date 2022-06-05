using System.Linq;
using UnityEngine;

public class Deadly : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();

        if (!player) return;
        
        ResetPlayerToLastCheckpoint(player);
    }

    private static void ResetPlayerToLastCheckpoint(Player player)
    {
        var lastActiveCheckpoint = FindObjectsOfType<Checkpoint>()
            .Where(x => x.IsActive)
            .OrderBy(x => x.checkpoint)
            .Last();

        player.transform.position = lastActiveCheckpoint.transform.position;
    }
}
