using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CAS.UserConsent;
using CAS;

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

    [SerializeField] private GameObject _highSettings;
    [SerializeField] private GameObject _lowSettings;

    private void Start()
    {
        Singleton = this;
        _analyticsEvent.OnEvent("Launch");

        if (PlayerPrefs.HasKey("Graphics") && PlayerPrefs.GetInt("Graphics") == 0)
        {
            _highSettings.SetActive(false);
	    _lowSettings.SetActive(true);
        }

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

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
        });

        _bg.sprite = _bgSprites[Random.Range(0, _bgSprites.Length)];
        PlayerPrefs.SetInt("Spell", 0);
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

    public void Graphics(int value)
    {
        PlayerPrefs.SetInt("Graphics", value);
    }
    
    public void VillageOpen()
    {
        PlayerPrefs.SetInt("Village", 2);
    }
}
