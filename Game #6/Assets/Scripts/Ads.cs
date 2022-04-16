using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ads : MonoBehaviour
{
    public static Ads Singleton { get; private set; }

    public bool InterstitialShowed { get; set; }

    [SerializeField] private AnalyticsEventManager _analyticsEventManager;

    [SerializeField] private Button _fourButton;
    [SerializeField] private Button _fiveButton;
    [SerializeField] private Text _fourAdButtonText;
    [SerializeField] private Text _fiveAdButtonText;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Text _continuteErrorText;

    private float _adButtonTimer = 10;
    private bool _forMenu;
    private string _partName;

    private void Start()
    {
        Singleton = this;

#if UNITY_ANDROID
        string appKey = "13a239601";
#else
        string appKey = "unexpected_platform";
#endif

        IronSource.Agent.init(appKey);
        StartCoroutine(InterstitialLoad());
    }

    private void OnEnable()
    {
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            if (_adButtonTimer > 0)
            {
                _adButtonTimer -= Time.deltaTime;

                _fourAdButtonText.gameObject.SetActive(true);
                _fourButton.interactable = false;
                _fiveAdButtonText.gameObject.SetActive(true);
                _fiveButton.interactable = false;

                _fourAdButtonText.text = ((int)_adButtonTimer).ToString();
                _fiveAdButtonText.text = ((int)_adButtonTimer).ToString();
            }
            else
            {
                _fourAdButtonText.gameObject.SetActive(false);
                _fourButton.interactable = true;
                _fiveAdButtonText.gameObject.SetActive(false);
                _fiveButton.interactable = true;
            }
        }
        else
        {
            if (_adButtonTimer > 0)
            {
                _adButtonTimer -= Time.deltaTime;
                _continuteErrorText.gameObject.SetActive(true);
                _continuteErrorText.text = ((int)_adButtonTimer).ToString();
                _continueButton.interactable = false;
            }
            else
            {
                _continuteErrorText.gameObject.SetActive(false);
                _continueButton.interactable = true;
            }
        }
    }

    void InterstitialAdClosedEvent()
    {
        if (_forMenu)
        {
            AllObjects.Singleton.AnalyticsEvent.OnEvent("GoToMenu");
            SceneManager.LoadScene("Main");
        }
        else
        {
            AllObjects.Singleton.AnalyticsEvent.OnEvent("AfterDeadShowed");
        }
        _forMenu = false;
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        StartCoroutine(InterstitialLoad());
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            if(_partName == "Four")
            {
                _analyticsEventManager.OnEvent("4PStart");
                SceneChange.Singleton.SceneLoad(5);
            }
            else if(_partName == "Five")
            {
                _analyticsEventManager.OnEvent("5PStart");
                SceneChange.Singleton.SceneLoad(6);
            }
        }
        else
        {
            Character.Singleton.Respawn();
            AllObjects.Singleton.AnalyticsEvent.OnEvent("Continue");
            AllObjects.Singleton.AnalyticsEvent.OnEvent("ContinuedOrRestarted");
        }
    }

    public void Restart()
    {
        AllObjects.Singleton.AnalyticsEvent.OnEvent("ContinuedOrRestarted");
        AllObjects.Singleton.AnalyticsEvent.OnEvent("Restart");
        SceneManager.LoadScene("Play");
    }

    public void Continue()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            _adButtonTimer = 5;
            AllObjects.Singleton.AnalyticsEvent.OnEvent("ContinueFail");
        }
    }

    public void OpenPart(string partName)
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            _partName = partName;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            _adButtonTimer = 5;
            _analyticsEventManager.OnEvent("OpenPartFail");
        }
    }

    public void GoToMenu()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
            _forMenu = true;
        }
        else
        {
            AllObjects.Singleton.AnalyticsEvent.OnEvent("GoToMenuFail");
            SceneManager.LoadScene("Main");
        }
    }

    IEnumerator InterstitialLoad()
    {
        yield return new WaitForSeconds(5);
        IronSource.Agent.loadInterstitial();
    }

    private void OnApplicationPause(bool pause)
    {
        IronSource.Agent.onApplicationPause(pause);
    }
}

