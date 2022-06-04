using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Color _defaultColor;
    private Boss _boss;
    public bool isActivated;

    public string targetSceneName;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _defaultColor = _renderer.color;
        _renderer.color = new Color(_defaultColor.grayscale, _defaultColor.grayscale, _defaultColor.grayscale);

        _boss = FindObjectOfType<Boss>();
    }

    private void Activate()
    {
        isActivated = true;
        _renderer.color = _defaultColor;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isActivated) return;
        
        var player = col.GetComponent<Player>();

        if (player)
        {
            // just restart for now
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
