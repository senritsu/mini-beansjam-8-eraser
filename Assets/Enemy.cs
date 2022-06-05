using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    
   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Damage>()?.amount != null)
        {
            Die();
        }
    }

   public void Die()
   {
       Instantiate(deathEffect, transform.position, Quaternion.identity);
       Destroy(gameObject);
   }
}
