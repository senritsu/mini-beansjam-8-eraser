using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Boss _boss;
    public bool isActivated;

    public string targetSceneName;
    private static readonly int Activated = Animator.StringToHash("Activate");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _boss = FindObjectOfType<Boss>();
    }

    private void Activate()
    {
        isActivated = true;
        _animator.SetTrigger(Activated);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isActivated) return;
        
        var player = col.GetComponent<Player>();

        if (player)
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated && !_boss)
        {
            Activate();
        }
    }
}
