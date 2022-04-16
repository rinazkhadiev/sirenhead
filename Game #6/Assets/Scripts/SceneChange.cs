using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public static SceneChange Singleton { get; private set; }

    [SerializeField] private Toggle _fogTog;
    private int _isFog = 1;

    [SerializeField] private Button _startTwoButton;
    [SerializeField] private Button _startThreeButton;
    [SerializeField] private AnalyticsEventManager _analyticsEvent;

    [SerializeField] private Image _bg;
    [SerializeField] private Sprite[] _bgSprites;

    private void Start()
    {
        Singleton = this;
        _analyticsEvent.OnEvent("Launch");

        PlayerPrefs.SetInt("DNFR", 0);

        if (PlayerPrefs.HasKey("Fog"))
        {
            if(PlayerPrefs.GetInt("Fog") == 0)
            {
                _fogTog.isOn = false;
            }
            else
            {
                _fogTog.isOn = true;
            }
        }

        if(PlayerPrefs.GetInt("Part 2") == 1)
        {
            _startTwoButton.interactable = true;
        }

        if (PlayerPrefs.GetInt("Part 3") == 1)
        {
            _startThreeButton.interactable = true;
        }

        _bg.sprite = _bgSprites[Random.Range(0, _bgSprites.Length)];

Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
       Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

    // Set a flag here to indicate whether Firebase is ready to use by your app.
  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});
    }

    public void SceneLoad(int partNumber)
    {
        PlayerPrefs.SetInt("Part", partNumber);
        PlayerPrefs.SetInt("Fog", _isFog);
        SceneManager.LoadScene("CastScene");
        PlayerPrefs.SetInt("Dialog", 0);
    }

    public void IsFog()
    {
        if (_fogTog.isOn)
        {
            _isFog = 1;
        }
        else
        {
            _isFog = 0;
        }
        PlayerPrefs.SetInt("Fog", _isFog);
    }
}
