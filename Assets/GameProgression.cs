using System.Linq;
using UnityEngine;

public class GameProgression : MonoBehaviour
{
    public enum SmithQuestProgress
    {
        Start,
        TalkedForTheFirstTime,
        ForcedBackToHub,
        DestroyedFirstBoss
    }

    public enum Checkpoint
    {
        Green1,
        Green2,
        Green3,
    }

    public SmithQuestProgress smithQuestProgress;
    public Checkpoint[] activatedCheckpoints;

    public static GameProgression Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCheckpoint(Checkpoint checkpoint)
    {
        activatedCheckpoints =
            activatedCheckpoints.Concat(new[] { checkpoint }).Distinct().OrderBy(x => x).ToArray();
    }
}
