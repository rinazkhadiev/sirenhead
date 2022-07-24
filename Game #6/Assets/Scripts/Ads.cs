using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CAS;

public class Ads : MonoBehaviour
{
    public static Ads Singleton { get; private set; }

    public bool InterstitialShowed { get; set; }

    [SerializeField] private AnalyticsEventManager _analyticsEventManager;

    [SerializeField] private Button _fourButton;
    [SerializeField] private Button _fiveButton;
    [SerializeField] private Text _fourAdButtonText;
    [SerializeField] private Text _fiveAdButtonText;

    private string _whatButton;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Text _continuteErrorText;

    [SerializeField] private Button _spellButton;
    [SerializeField] private Text _spellErrorText;

    private float _adButtonTimer = 10;
    private bool _forMenu;
    private string _partName;

    public IMediationManager manager { get; set; }

    private void Start()
    {
        Singleton = this;


        manager = MobileAds.BuildManager()
          .WithManagerIdAtIndex(0)
          .WithInitListener((success, error) => {
           })
           .WithMediationExtras(MediationExtras.facebookDataProcessing, "LDU")
          .Initialize();

        manager.OnLoadedAd += (adType) =>
        {
        };
        manager.OnFailedToLoadAd += (adType, error) =>
        {
        };

        manager.OnInterstitialAdClosed += InterstitialAdClosedEvent;
        manager.OnRewardedAdCompleted += RewardedVideoAdRewardedEvent;

        MobileAds.settings.allowInterstitialAdsWhenVideoCostAreLower = true;

        MobileAds.settings.isExecuteEventsOnUnityThread = true;

	    MobileAds.settings.analyticsCollectionEnabled = true;
	
	    manager.SetAppReturnAdsEnabled(true);


        if (SceneManager.GetActiveScene().name != "Main")
        {
            _adButtonTimer = 0;
        }
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

                _spellErrorText.gameObject.SetActive(true);
                _spellErrorText.text = ((int)_adButtonTimer).ToString();
                _spellButton.interactable = false;
            }
            else
            {
                _continuteErrorText.gameObject.SetActive(false);
                _continueButton.interactable = true;

                _spellErrorText.gameObject.SetActive(false);
                _spellButton.interactable = true;
            }
        }
    }

    void InterstitialAdClosedEvent()
    {
        if (_forMenu)
        {
            AllObjects.Singleton.AnalyticsEvent.OnEvent("AdsEvent_Showed");
            SceneManager.LoadScene("Main");
        }
        else
        {
            AllObjects.Singleton.AnalyticsEvent.OnEvent("AdsEvent_Showed");
        }
        _forMenu = false;
    }



    void RewardedVideoAdRewardedEvent()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (_partName == "Four")
            {
                _analyticsEventManager.OnEvent("4PStart");
                _analyticsEventManager.OnEvent("AdsEvent_Showed");
                SceneChange.Singleton.SceneLoad(5);
            }
            else if (_partName == "Five")
            {
                _analyticsEventManager.OnEvent("5PStart");
                _analyticsEventManager.OnEvent("AdsEvent_Showed");
                SceneChange.Singleton.SceneLoad(6);
            }
        }
        else
        {
            if (_whatButton == "Continue")
            {
                Character.Singleton.Respawn();
                AllObjects.Singleton.AnalyticsEvent.OnEvent("Continue");
                AllObjects.Singleton.AnalyticsEvent.OnEvent("AdsEvent_Showed");
            }
            else if (_whatButton == "Spell")
            {
                Character.Singleton.GravityValue = 0;
                AllObjects.Singleton.AnalyticsEvent.OnEvent("Spell");
                AllObjects.Singleton.AnalyticsEvent.OnEvent("AdsEvent_Showed");
                AllObjects.Singleton.SpellPanel.SetActive(false);
                AllObjects.Singleton.SirenIsStop = false;
            }
        }
    }

    public void Restart()
    {
        AllObjects.Singleton.AnalyticsEvent.OnEvent("Restart");
        SceneManager.LoadScene("Play");
    }

    public void Continue()
    {
        if (manager.IsReadyAd(AdType.Rewarded))
        {
            _whatButton = "Continue";
            manager.ShowAd(AdType.Rewarded);
        }
        else
        {
            _adButtonTimer = 5; 
            AllObjects.Singleton.AnalyticsEvent.OnEvent("AdsEvent_Fail");
        }
    }

    public void Spell()
    {
        if (manager.IsReadyAd(AdType.Rewarded))
        {
            _whatButton = "Spell";
            AllObjects.Singleton.SirenIsStop = true;
            manager.ShowAd(AdType.Rewarded);
        }
        else
        {
            _adButtonTimer = 5;
            AllObjects.Singleton.AnalyticsEvent.OnEvent("AdsEvent_Fail");
        }
    }

    public void OpenPart(string partName)
    {
        if (manager.IsReadyAd(AdType.Rewarded))
        {
            _partName = partName;
            manager.ShowAd(AdType.Rewarded);
        }
        else
        {
            _adButtonTimer = 5;
            AllObjects.Singleton.AnalyticsEvent.OnEvent("AdsEvent_Fail");
        }
    }

    public void GoToMenu()
    {
        if (manager.IsReadyAd(AdType.Interstitial))
        {
            manager.ShowAd(AdType.Interstitial);
            _forMenu = true;
        }
        else
        {
            AllObjects.Singleton.AnalyticsEvent.OnEvent("AdsEvent_Fail");
            SceneManager.LoadScene("Main");
        }
    }
}

//public class Ads : MonoBehaviour
//{
//    public static Ads Singleton { get; private set; }

//    public bool InterstitialShowed { get; set; }

//    [SerializeField] private AnalyticsEventManager _analyticsEventManager;

//    [SerializeField] private Button _fourButton;
//    [SerializeField] private Button _fiveButton;
//    [SerializeField] private Text _fourAdButtonText;
//    [SerializeField] private Text _fiveAdButtonText;

//    private string _whatButton;

//    [SerializeField] private Button _continueButton;
//    [SerializeField] private Text _continuteErrorText;

//    [SerializeField] private Button _spellButton;
//    [SerializeField] private Text _spellErrorText;

//    private float _adButtonTimer = 10;
//    private bool _forMenu;
//    private string _partName;


//    private void Start()
//    {
//        Singleton = this;

//#if unity_android
//                string appkey = "13a239601";
//#else
//        string appkey = "unexpected_platform";
//#endif


//        IronSource.Agent.init(appKey);
//        if (SceneManager.GetActiveScene().name != "Main")
//        {
//            StartCoroutine(InterstitialLoad());
//            _adButtonTimer = 0;
//        }
//    }

//    private void OnEnable()
//    {
//        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
//        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
//    }

//    private void Update()
//    {
//        if (SceneManager.GetActiveScene().name == "Main")
//        {
//            if (_adButtonTimer > 0)
//            {
//                _adButtonTimer -= Time.deltaTime;

//                _fourAdButtonText.gameObject.SetActive(true);
//                _fourButton.interactable = false;
//                _fiveAdButtonText.gameObject.SetActive(true);
//                _fiveButton.interactable = false;

//                _fourAdButtonText.text = ((int)_adButtonTimer).ToString();
//                _fiveAdButtonText.text = ((int)_adButtonTimer).ToString();
//            }
//            else
//            {
//                _fourAdButtonText.gameObject.SetActive(false);
//                _fourButton.interactable = true;
//                _fiveAdButtonText.gameObject.SetActive(false);
//                _fiveButton.interactable = true;
//            }
//        }
//        else
//        {
//            if (_adButtonTimer > 0)
//            {
//                _adButtonTimer -= Time.deltaTime;
//                _continuteErrorText.gameObject.SetActive(true);
//                _continuteErrorText.text = ((int)_adButtonTimer).ToString();
//                _continueButton.interactable = false;

//                _spellErrorText.gameObject.SetActive(true);
//                _spellErrorText.text = ((int)_adButtonTimer).ToString();
//                _spellButton.interactable = false;
//            }
//            else
//            {
//                _continuteErrorText.gameObject.SetActive(false);
//                _continueButton.interactable = true;

//                _spellErrorText.gameObject.SetActive(false);
//                _spellButton.interactable = true;
//            }
//        }
//    }

//    void InterstitialAdClosedEvent()
//    {
//        if (_forMenu)
//        {
//            AllObjects.Singleton.AnalyticsEvent.OnEvent("GoToMenu");
//            SceneManager.LoadScene("Main");
//        }
//        else
//        {
//            AllObjects.Singleton.AnalyticsEvent.OnEvent("AfterDeadShowed");
//        }
//        _forMenu = false;
//    }



//    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
//    {
//        if (SceneManager.GetActiveScene().name == "Main")
//        {
//            if (_partName == "Four")
//            {
//                _analyticsEventManager.OnEvent("4PStart");
//                SceneChange.Singleton.SceneLoad(5);
//            }
//            else if (_partName == "Five")
//            {
//                _analyticsEventManager.OnEvent("5PStart");
//                SceneChange.Singleton.SceneLoad(6);
//            }
//        }
//        else
//        {
//            if (_whatButton == "Continue")
//            {
//                Character.Singleton.Respawn();
//                AllObjects.Singleton.AnalyticsEvent.OnEvent("Continue");
//                AllObjects.Singleton.AnalyticsEvent.OnEvent("ContinuedOrRestarted");
//            }
//            else if (_whatButton == "Spell")
//            {
//                Character.Singleton.GravityValue = 0;
//                AllObjects.Singleton.AnalyticsEvent.OnEvent("Spell");
//                AllObjects.Singleton.SpellPanel.SetActive(false);
//                AllObjects.Singleton.SirenIsStop = false;
//            }
//        }
//    }

//    public void Restart()
//    {
//        AllObjects.Singleton.AnalyticsEvent.OnEvent("ContinuedOrRestarted");
//        AllObjects.Singleton.AnalyticsEvent.OnEvent("Restart");
//        SceneManager.LoadScene("Play");
//    }

//    public void Continue()
//    {
//        if (IronSource.Agent.isRewardedVideoAvailable())
//        {
//            IronSource.Agent.showRewardedVideo();
//            _whatButton = "Continue";
//        }
//        else
//        {
//            _adButtonTimer = 5;
//            AllObjects.Singleton.AnalyticsEvent.OnEvent("ContinueFail");
//        }
//    }

//    public void Spell()
//    {
//        if (IronSource.Agent.isRewardedVideoAvailable())
//        {
//            IronSource.Agent.showRewardedVideo();
//            _whatButton = "Spell";
//            AllObjects.Singleton.SirenIsStop = true;
//        }
//        else
//        {
//            _adButtonTimer = 5;
//            AllObjects.Singleton.AnalyticsEvent.OnEvent("SpellFail");
//        }
//    }

//    public void OpenPart(string partName)
//    {
//        if (IronSource.Agent.isRewardedVideoAvailable())
//        {
//            _partName = partName;
//            IronSource.Agent.showRewardedVideo();
//        }
//        else
//        {
//            _adButtonTimer = 5;
//            _analyticsEventManager.OnEvent("OpenPartFail");
//        }
//    }

//    public void GoToMenu()
//    {
//        if (IronSource.Agent.isInterstitialReady())
//        {
//            IronSource.Agent.showInterstitial();
//            _forMenu = true;
//        }
//        else
//        {
//            AllObjects.Singleton.AnalyticsEvent.OnEvent("GoToMenuFail");
//            SceneManager.LoadScene("Main");
//        }
//    }

//    IEnumerator InterstitialLoad()
//    {
//        yield return new WaitForSeconds(5);
//        IronSource.Agent.loadInterstitial();
//    }

//    private void OnApplicationPause(bool pause)
//    {
//        IronSource.Agent.onApplicationPause(pause);
//    }
//}

