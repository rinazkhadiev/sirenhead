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
	Ads.Singleton.manager.SkipNextAppReturnAds();
        Application.OpenURL(url);
    }
}
