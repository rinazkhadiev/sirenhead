using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    public void RestartScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OpenSite(string url)
    {
        Application.OpenURL(url);
    }
}
