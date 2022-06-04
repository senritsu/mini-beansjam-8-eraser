using UnityEngine;

public class Enemy : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Damage>()?.amount != null)
        {
            Destroy(gameObject);
        }
    }
}
