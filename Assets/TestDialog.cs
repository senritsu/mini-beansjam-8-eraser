using System.Collections;
using Droni;
using UnityEngine;

public class TestDialog : MonoBehaviour
{
    private bool _started;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>() || _started) return;
        
        StartCoroutine(DisplayDialogue());
        _started = true;
    }

    private IEnumerator DisplayDialogue()
    {
        DialogMaster.master.PrintDialog("Du solltest nicht hier sein...", Color.red, 4f);
        yield return new WaitForSeconds(6f);
        DialogMaster.master.PrintDialog("Schreite durch das Portal, vielleicht findest du einen Weg", Color.red, 4f);
        yield return new WaitForSeconds(6f);
        DialogMaster.master.PrintDialog("Du hast nicht viel Zeit", Color.red, 4f);
        yield return new WaitForSeconds(6f);

        Destroy(FindObjectOfType<Boss>().gameObject);
    }
}
