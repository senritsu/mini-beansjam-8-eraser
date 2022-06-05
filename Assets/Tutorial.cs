using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Awake()
    {
        if (GameProgression.Instance.smithQuestProgress > GameProgression.SmithQuestProgress.Start)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        
        Destroy(gameObject);
    }
}
