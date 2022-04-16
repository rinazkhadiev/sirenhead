using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AllObjects : MonoBehaviour
{
    [Header("Другие скрипты")]
    public JoystickController JoyController;
    public AnalyticsEventManager AnalyticsEvent;
    public Restart Restart;

    [Header("Рекламный блок")]
    public GameObject FailMenu;
    public Image RewardImage;
    public GameObject ErrorText;

    [Header("Персонаж")]
    public Text OutWorldCountText;
    public GameObject DeadZoneVinet;
    public GameObject FirstPartCharPos;
    public GameObject SecondPartCharPos;
    public GameObject ThirdPartCharPos;
    public GameObject MazePartCharPos;
    public GameObject FirthPartCharPos;
    public GameObject FivePartCharPos;

    public GameObject Weapon;
    public GameObject RaycastTarget;
    public Animator WeaponAnim;
    public ParticleSystem ShootVFX;

    public GameObject FirstPartSirenHead;
    public GameObject ThirdPartSirenHead;
    public SirenHead ThirdPartSirenHeadScript;
    public GameObject MiniSirenPrefab;

    public GameObject MiniCartoonPrefabA;
    public GameObject MiniCartoonPrefabB;

    [Header("Детали")]
    public List<GameObject> FirstDetails = new List<GameObject>();
    public List<GameObject> FirstUpBtn = new List<GameObject>();
    public Image[] FirstDetailsImgs;

    public List<GameObject> SecondDetails = new List<GameObject>();
    public List<GameObject> SecondUpBtn = new List<GameObject>();
    public Image[] SecondDetailsImgs;
    public Text NeetToGetText;

    public GameObject DetailInstalBtn;

    public Text CurrentFindText;
    public GameObject WhatIsFloorUp;
    public GameObject WhatIsFloorDown;

    public GameObject Arrow;

    [Header("Окружение")]
    public GameObject FirstObjectsParent;
    public GameObject ForestObject;
    public GameObject SecondObjectsParent;
    public GameObject ThirdObjectsParent;
    public GameObject MazeObjectsParent;
    public GameObject FirthObjectsParent;
    public GameObject FiveObjectsParent;
    public GameObject FirstPartCar;
    public GameObject RepairedCar;
    public GameObject SecondPartCar;
    public GameObject StartEngBtn;
    public ParticleSystem CarLight;
    public GameObject MazePartCar;

   
    public GameObject ThatTowerUp;
    public GameObject TowerUpBtn;
    public GameObject ThatTowerDown;
    public GameObject TowerDownBtn;

    public GameObject KeyObject;
    public GameObject KeyUpButton;

    public GameObject ThatDoorInPos;
    public GameObject ThatDoorOutPos;
    public GameObject DoorInBtn;
    public GameObject DoorOutBtn;


    public GameObject ThatLadderUp;
    public GameObject LadderUpBtn;
    public GameObject ThatLadderDown;
    public GameObject LadderDownBtn;

    public GameObject HouseForSiren;
    public GameObject SirenFirstPoint;
    public GameObject SirenSecondPoint;

    public GameObject KeyFirth;
    public GameObject FirthCar;

    public GameObject FiveMyHouse;

    [Header("UI")]
    public Image FirstFinishPanel;
    public GameObject FirstFinishText;
    public Image SecondFinishPanel;
    public GameObject SecondFinishText;
    public Image ThirdFinishPanel;
    public GameObject ThirdFinishText;
    public Image MazeFinishPanel;
    public GameObject MazeFinishText;
    public Image FirthFinishPanel;
    public GameObject FirthFinishText;
    public Image FiveFinishPanel;
    public GameObject FiveFinishText;

    public Image FailImage;
    public Sprite[] FailImageSprite;
    public GameObject[] FailTexts;
    public GameObject[] MoveUI;

    public Image SirenDistanceImage;
    public Image CameraShootButton;
    public Button CameraShootButtonComponent;
    public GameObject HowToPlay;

    public Slider HpBar;
    public GameObject AimUI;
    public GameObject ShootButton;
    public Text BulletValueText;
    public GameObject WeaponReloadButton;

    public GameObject ContinueButton;

    public GameObject[] SirenWinUi;
    public GameObject GiveUpsButton;

    public GameObject MazeCarButton;

    public GameObject FirthPartCarText;
    public GameObject InvertiorHowToPlay;

    public GameObject SettingsMenu;
    public Slider SensitivityBar;

    public GameObject FiveNeedToDeadCartoon;
    public int CartoonsCounter = 50;

    public GameObject[] FirstTexts;
    public Slider CartoonSlider;
    public Text CartoonText;

    [Header("Audio")]
    public AudioSource FailStartAudio;
    public AudioSource GoAudio;
    public AudioSource RepairAudio;
    public AudioSource BgAudio;
    public AudioClip BgPartFirst;
    public AudioClip BgPartSecond;
    public AudioClip BgPartThird;

    public AudioSource WeaponReloadSound;
    public AudioSource WeaponShootSound;
    public AudioSource MiniSirenDead;

    public AudioSource FailAudio;
    public AudioSource HeardAudio;

    public AudioSource IsKeyNot;
    public AudioSource DoorOpen;

    public AudioSource StepAudio;
    public AudioClip[] FirstSteps;
    public AudioClip[] SecondSteps;






    public static AllObjects Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;
        
    }
}
