using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadly : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();
        
        if (player)
        {
            // just restart for now
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
