using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private IEnumerator Start()
    {
        if (GameProgression.Instance.smithQuestProgress > GameProgression.SmithQuestProgress.Start)
        {
            Destroy(gameObject);
            yield return null;
        }
        
        yield return new WaitForSeconds(3f);
        
        Destroy(gameObject);
    }
}
