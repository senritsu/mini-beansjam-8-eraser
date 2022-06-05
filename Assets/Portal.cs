using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Boss _boss;
    public bool isActivated;

    public string targetSceneName;
    private static readonly int Activated = Animator.StringToHash("Activate");
    private static readonly int FadeOutParameter = Animator.StringToHash("FadeOut");
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
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        GameObject.Find("GhettoCameraFade").GetComponent<Animator>().SetTrigger(FadeOutParameter);
        
        yield return new WaitForSeconds(1);
        
        SceneManager.LoadScene(targetSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated && (!_boss || !_boss.gameObject.activeInHierarchy))
        {
            Activate();
        }
    }
}
