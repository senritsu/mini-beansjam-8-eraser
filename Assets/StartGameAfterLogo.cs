using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameAfterLogo : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Hub");
    }
}
