using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<Player>().transform.position = transform.position;
    }
}
