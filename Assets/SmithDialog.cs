using System;
using System.Collections;
using Droni;
using UnityEngine;

public class SmithDialog : MonoBehaviour
{
    private bool _started;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>()) return;

        StartCoroutine(GameProgression.Instance.smithQuestProgress switch
        {
            GameProgression.SmithQuestProgress.Start => DisplayInitialDialogue(),
            GameProgression.SmithQuestProgress.TalkedForTheFirstTime => DisplayReminder(),
            GameProgression.SmithQuestProgress.ForcedBackToHub => DisplayPity(),
            GameProgression.SmithQuestProgress.DestroyedFirstBoss => DisplaySecondDialogue(),
        });
    }

    private IEnumerator DisplayInitialDialogue()
    {
        GameProgression.Instance.smithQuestProgress = GameProgression.SmithQuestProgress.TalkedForTheFirstTime;
        
        DialogMaster.master.PrintDialog("Du solltest nicht hier sein...",Color.red, 3f);

        yield return new WaitForSeconds(4f);
        
        DialogMaster.master.PrintDialog("Schreite durch das Portal, vielleicht findest du einen Weg\nDu hast nicht viel Zeit.", Color.white, 3f);
    }

    private IEnumerator DisplayReminder()
    {
        DialogMaster.master.PrintDialog("Schreite durch das Portal, vielleicht findest du einen Weg\nDu hast nicht viel Zeit.", Color.white, 3f);

        yield return null;
    }

    private IEnumerator DisplayPity()
    {
        DialogMaster.master.PrintDialog("Ich hoffe du weißt was du tust...", Color.white, 3f);

        yield return null;
    }

    private IEnumerator DisplaySecondDialogue()
    {
        DialogMaster.master.PrintDialog("Was für eine Spur der Verwüstung.\nMeine Befürchtung war wohl wahr, du solltest nicht hier sein...\n",Color.white, 2f);

        yield return new WaitForSeconds(5f);
        
        DialogMaster.master.PrintDialog("Und das wars auch schon mit Eraser, vielen Dank fürs Spielen!",Color.green, 2f);
    }
}
