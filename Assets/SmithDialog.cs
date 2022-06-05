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
        
        DialogMaster.master.PrintDialog("This is no place for you",Color.red, 3f);

        yield return new WaitForSeconds(4f);
        
        DialogMaster.master.PrintDialog("Portal's right there...\ntry to find...anything.", Color.white, 3f);
    }

    private IEnumerator DisplayReminder()
    {
        DialogMaster.master.PrintDialog("Portal's right there...\ntry to find...anything.", Color.white, 3f);

        yield return null;
    }

    private IEnumerator DisplayPity()
    {
        DialogMaster.master.PrintDialog("Seems like that place got the better of you for now.", Color.white, 3f);

        yield return null;
    }

    private IEnumerator DisplaySecondDialogue()
    {
        DialogMaster.master.PrintDialog("That place, you erased all life in it.",Color.white, 2f);

        yield return new WaitForSeconds(5f);
        
        DialogMaster.master.PrintDialog("And that is the end of Eraser. Thanks for playing!",Color.green, 2f);
    }
}
