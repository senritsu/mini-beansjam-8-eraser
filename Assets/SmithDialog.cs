using System.Collections;
using Droni;
using UnityEngine;

public class SmithDialog : MonoBehaviour
{
    private bool _started;
    private GameProgression _progression;

    private void Awake()
    {
        _progression = FindObjectOfType<GameProgression>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>()) return;

        StartCoroutine(_progression.smithQuestProgress switch
        {
            GameProgression.SmithQuestProgress.Start => DisplayInitialDialogue(),
            GameProgression.SmithQuestProgress.TalkedForTheFirstTime => DisplayReminder(),
            GameProgression.SmithQuestProgress.DestroyedFirstBoss => DisplaySecondDialogue()
        });
    }

    private IEnumerator DisplayInitialDialogue()
    {
        _progression.smithQuestProgress = GameProgression.SmithQuestProgress.TalkedForTheFirstTime;
        
        DialogMaster.master.PrintDialog("Du solltest nicht hier sein...",Color.red, 3f);

        yield return new WaitForSeconds(4f);
        
        DialogMaster.master.PrintDialog("Schreite durch das Portal, vielleicht findest du einen Weg\nDu hast nicht viel Zeit", Color.white, 3f);
    }

    private IEnumerator DisplayReminder()
    {
        DialogMaster.master.PrintDialog("Schreite durch das Portal, vielleicht findest du einen Weg\nDu hast nicht viel Zeit", Color.white, 3f);

        yield return null;
    }

    private IEnumerator DisplaySecondDialogue()
    {
        DialogMaster.master.PrintDialog("TODO TODO TODO TODO",Color.white, 2f);

        yield return null;
    }
}
