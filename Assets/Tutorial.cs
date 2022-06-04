using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        
        Destroy(gameObject);
    }
}
