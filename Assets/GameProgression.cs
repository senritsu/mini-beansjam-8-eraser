using UnityEngine;

public class GameProgression : MonoBehaviour
{
    public enum SmithQuestProgress
    {
        Start,
        TalkedForTheFirstTime,
        DestroyedFirstBoss,
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
}
