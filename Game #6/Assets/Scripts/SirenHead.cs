using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SirenHead : MonoBehaviour
{
    public static SirenHead Singleton { get; private set; }
    public Transform Transform { get; private set; }

    private NavMeshAgent _navMesh;
    private Animator _anim;
    private Vector3 _spawnTransform;

    [SerializeField] private float _walkSpeed = 2;
    [SerializeField] private float _runSpeed = 4;

    private float _distanceAlpha;
    private bool _isAfraid;
    private int _partNumber;
    private float _failImageAlpha = 1f;
    private bool _isDead;

    private bool _point;
    private float _pointTimer;

    private string _tag;

    private float _sirenTimer;

    public float Hp { get; private set; }

    private void Start()
    {
        Singleton = this;

        _partNumber = PlayerPrefs.GetInt("Part");

        _navMesh = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        Transform = GetComponent<Transform>();
        _spawnTransform = Transform.localPosition;
        _tag = gameObject.tag;

        if (_partNumber == 1)
        {
            if (PlayerPrefs.GetInt("Dialog") != 1)
            {
                StartCoroutine(HowToPlay());
            }

            Hp = 20f;
            AllObjects.Singleton.HpBar.maxValue = Hp;
            AllObjects.Singleton.HpBar.value = Hp;
        }
        else if(_partNumber == 3)
        {
            if(_tag == "MiniSiren")
            {
                Hp = 1f;
            }
            else
            {
                Hp = 100f;
                AllObjects.Singleton.HpBar.maxValue = Hp;
                AllObjects.Singleton.HpBar.value = Hp;
            }

            AllObjects.Singleton.GiveUpsButton.SetActive(false);
        }
        else if(_partNumber == 4)
        {
            Hp = 1f;
            AllObjects.Singleton.GiveUpsButton.SetActive(false);
        }
        else if(_partNumber == 5)
        {
            AllObjects.Singleton.GiveUpsButton.SetActive(false);
        }
        else if(_partNumber == 6)
        {
            AllObjects.Singleton.GiveUpsButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (_partNumber == 1)
        {
            AllObjects.Singleton.CameraShootButton.gameObject.SetActive(false);
            Transform.position = new Vector3(Transform.position.x, 0, Transform.position.z);

            if (!AllObjects.Singleton.SirenIsStop)
            {
                _navMesh.isStopped = false;
            }
            else
            {
                _navMesh.isStopped = true;
            }

            if (!_isDead)
            {
                if (Character.Singleton.Transform.position.y < 30)
                {
                    _navMesh.SetDestination(Character.Singleton.Transform.position);

                    if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 15)
                    {
                        AllObjects.Singleton.HeardAudio.mute = true;
                        _anim.SetInteger("State", 1);

                        AllObjects.Singleton.SirenDistanceImage.gameObject.SetActive(false);
                        _navMesh.speed = _walkSpeed;
                    }
                    else if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 15 && Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 2)
                    {
                        _navMesh.speed = _runSpeed;
                        AllObjects.Singleton.HeardAudio.mute = false;
                        _anim.SetInteger("State", 2);
                        _distanceAlpha = 0.3f - ((Mathf.Abs(Transform.position.x - Character.Singleton.Transform.position.x) + Mathf.Abs(Transform.position.z - Character.Singleton.Transform.position.z)) / 2 / 50);
                        AllObjects.Singleton.SirenDistanceImage.gameObject.SetActive(true);
                        AllObjects.Singleton.SirenDistanceImage.color = new Color(AllObjects.Singleton.SirenDistanceImage.color.r, AllObjects.Singleton.SirenDistanceImage.color.g, AllObjects.Singleton.SirenDistanceImage.color.b, _distanceAlpha);
                    }
                    else if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 2 && !Character.Singleton.IsDead)
                    {
                        Dead();
                    }
                }
                else
                {
                    _navMesh.SetDestination(AllObjects.Singleton.HouseForSiren.transform.position);
                    AllObjects.Singleton.HeardAudio.mute = true;
                    _anim.SetInteger("State", 1);
                    AllObjects.Singleton.SirenDistanceImage.gameObject.SetActive(false);
                }
            }
            else
            {
                AllObjects.Singleton.SirenDistanceImage.gameObject.SetActive(false);
                if (_sirenTimer > 0)
                {
                    _sirenTimer -= Time.deltaTime;
                    AllObjects.Singleton.SirenTimerText.text = $"{(int)_sirenTimer}";
                }
                else
                {

                    AllObjects.Singleton.SirenTimerText.gameObject.SetActive(false);
                    Hp = 20f;
                    AllObjects.Singleton.HpBar.value = AllObjects.Singleton.FirstPartSirenHeadScript.Hp;
                    _isDead = false;
                }
            }
        }

        else if (_partNumber == 2)
        {
            AllObjects.Singleton.CameraShootButton.gameObject.SetActive(false);

            if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 5)
            {
                AllObjects.Singleton.HeardAudio.mute = true;

                if (!AllObjects.Singleton.SirenIsStop)
                {
                    _navMesh.isStopped = false;
                }
                else
                {
                    _navMesh.isStopped = true;
                }

                _anim.SetInteger("State", 1);
                _navMesh.speed = _walkSpeed;


                _pointTimer += Time.deltaTime;

                if (_pointTimer > 20)
                {
                    _pointTimer = 0;

                    if (_point)
                    {
                        _point = false;
                    }
                    else
                    {
                        _point = true;
                    }
                }

                if (_point)
                {
                    _navMesh.SetDestination(AllObjects.Singleton.SirenFirstPoint.transform.position);
                }
                else
                {
                    _navMesh.SetDestination(AllObjects.Singleton.SirenSecondPoint.transform.position);
                }
            }
            else if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 5 && Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 1)
            {
                _navMesh.SetDestination(Character.Singleton.Transform.position);
                AllObjects.Singleton.HeardAudio.mute = false;
                _anim.SetInteger("State", 2);
                _navMesh.speed = _runSpeed;
            }
            else if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 1 && !Character.Singleton.IsDead)
            {
                Dead();
            }

        }
        else if (_partNumber == 3)
        {
            Transform.position = new Vector3(Transform.position.x, 0, Transform.position.z);

            if (!AllObjects.Singleton.ThirdPartSirenHeadScript._isDead)
            {
                AllObjects.Singleton.CameraShootButton.gameObject.SetActive(true);
            }

            if (!_isDead)
            {
                if (_isAfraid)
                {
                    _navMesh.isStopped = true;
                    _anim.SetInteger("State", 0);
                }
                else
                {
                    if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 2)
                    {
                        if (!AllObjects.Singleton.SirenIsStop)
                        {
                            _navMesh.isStopped = false;
                        }
                        else
                        {
                            _navMesh.isStopped = true;
                        }
                        _navMesh.SetDestination(Character.Singleton.Transform.position);
                        _navMesh.speed = _runSpeed;
                        AllObjects.Singleton.HeardAudio.mute = false;
                        _anim.SetInteger("State", 2);
                    }
                    if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 2 && !Character.Singleton.IsDead)
                    {
                        Dead();
                    }
                }
            }

            if (_tag == "MiniSiren" && AllObjects.Singleton.ThirdPartSirenHeadScript._isDead && !_isDead && gameObject.activeSelf)
            {
                StartCoroutine(SirenDead());
            }
        }
        else if (_partNumber == 4)
        {
            Transform.position = new Vector3(Transform.position.x, 0, Transform.position.z);
            AllObjects.Singleton.CameraShootButton.gameObject.SetActive(false);

            if (!_isDead)
            {
                if (!AllObjects.Singleton.SirenIsStop)
                {
                    _navMesh.isStopped = false;
                }
                else
                {
                    _navMesh.isStopped = true;
                }
                _navMesh.SetDestination(Character.Singleton.Transform.position);

                if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 10)
                {
                    AllObjects.Singleton.HeardAudio.mute = true;
                    _anim.SetInteger("State", 1);
                    _navMesh.speed = _walkSpeed;
                }
                else if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 10 && Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 2)
                {
                    AllObjects.Singleton.HeardAudio.mute = false;
                    _anim.SetInteger("State", 2);
                    _navMesh.speed = _runSpeed;
                }
                if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 2 && !Character.Singleton.IsDead)
                {
                    Dead();
                }
            }

            if ((_partNumber == 4) && _tag == "MiniSiren" && _isDead && gameObject.activeSelf)
            {
                Transform.localScale = new Vector3(Transform.localScale.x - 0.025f, Transform.localScale.y - 0.025f, Transform.localScale.z - 0.025f);
                if (Transform.localScale.x < 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else if (_partNumber == 5)
        {
            AllObjects.Singleton.CameraShootButton.gameObject.SetActive(false);
            if (!AllObjects.Singleton.SirenIsStop)
            {
                _navMesh.isStopped = false;
            }
            else
            {
                _navMesh.isStopped = true;
            }
            _navMesh.SetDestination(Character.Singleton.Transform.position);
            AllObjects.Singleton.HeardAudio.mute = false;

            if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 10)
            {
                _anim.SetInteger("State", 1);
                _navMesh.speed = _walkSpeed;
            }
            else if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 10 && Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 2)
            {
                _navMesh.speed = _runSpeed;
                _anim.SetInteger("State", 2);
            }
            else if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 2 && !Character.Singleton.IsDead)
            {
                Dead();
            }
        }
        else if (_partNumber == 6)
        {
            AllObjects.Singleton.CameraShootButton.gameObject.SetActive(false);

            if (!_isDead)
            {
                if (!AllObjects.Singleton.SirenIsStop)
                {
                    _navMesh.isStopped = false;
                }
                else
                {
                    _navMesh.isStopped = true;
                }
                _navMesh.SetDestination(Character.Singleton.Transform.position);

                
                if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) > 2)
                {
                    AllObjects.Singleton.HeardAudio.mute = false;
                    _anim.SetInteger("State", 2);
                    _navMesh.speed = _runSpeed;
                }
                if (Vector3.Distance(Character.Singleton.Transform.position, Transform.position) < 2 && !Character.Singleton.IsDead)
                {
                    Dead();
                }
            }

            if (_partNumber == 6 && _tag == "MiniSiren" && _isDead && gameObject.activeSelf)
            {
                Transform.localScale = new Vector3(Transform.localScale.x - 0.025f, Transform.localScale.y - 0.025f, Transform.localScale.z - 0.025f);
                if (Transform.localScale.x < 0)
                {
                    Destroy(gameObject);
                    if (AllObjects.Singleton.CartoonsCounter > 0)
                    {
                        AllObjects.Singleton.CartoonsCounter--;
                    }
                }
            }
        }

        if (AllObjects.Singleton.FailImage.gameObject.activeSelf)
        {
            AllObjects.Singleton.FailImage.sprite = AllObjects.Singleton.FailImageSprite[_partNumber - 1];
            _failImageAlpha -= Time.deltaTime / 3f;
            AllObjects.Singleton.FailImage.color = new Color(_failImageAlpha, _failImageAlpha, _failImageAlpha, AllObjects.Singleton.FailImage.color.a);

            if (_failImageAlpha <= 0)
            {
                AllObjects.Singleton.FailMenu.SetActive(true);

                if (!Ads.Singleton.InterstitialShowed)
                {
                    if (Ads.Singleton.manager.IsReadyAd(CAS.AdType.Interstitial))
                    {
                        Ads.Singleton.manager.ShowAd(CAS.AdType.Interstitial);
                        AllObjects.Singleton.AnalyticsEvent.OnEvent("AfterDeadSuccess");
                    }
                    else
                    {
                        AllObjects.Singleton.AnalyticsEvent.OnEvent("AfterDeadFailed");
                    }
                    Ads.Singleton.InterstitialShowed = true;
                }
            }
        }
        else
        {
            _failImageAlpha = 1f;
        }

        if (Detail.Singleton.IsFinish)
        {
            Detail.Singleton.AlphaFinish += Time.deltaTime / 2f;
            AllObjects.Singleton.ThirdFinishPanel.color = new Color(AllObjects.Singleton.ThirdFinishPanel.color.r, AllObjects.Singleton.ThirdFinishPanel.color.g, AllObjects.Singleton.ThirdFinishPanel.color.b, Detail.Singleton.AlphaFinish);
        }
    }

    private void Dead()
    {
        AllObjects.Singleton.FailImage.gameObject.SetActive(true);
        AllObjects.Singleton.FailTexts[Random.Range(0, AllObjects.Singleton.FailTexts.Length)].SetActive(true);
        for (int i = 0; i < AllObjects.Singleton.MoveUI.Length; i++)
        {
            AllObjects.Singleton.MoveUI[i].SetActive(false);
        }

        AllObjects.Singleton.FailAudio.PlayOneShot(AllObjects.Singleton.FailAudio.clip);
        AllObjects.Singleton.HeardAudio.mute = true;
        Transform.localPosition = _spawnTransform;

        Character.Singleton.IsDead = true;

        AllObjects.Singleton.AnalyticsEvent.OnEvent("Death");
    }

    public void RespawnSet()
    {
        AllObjects.Singleton.FailImage.gameObject.SetActive(false);
        AllObjects.Singleton.FailMenu.SetActive(false);

        for (int i = 0; i < AllObjects.Singleton.FailTexts.Length; i++)
        {
            AllObjects.Singleton.FailTexts[i].SetActive(false);
        }

        for (int i = 0; i < AllObjects.Singleton.MoveUI.Length; i++)
        {
            AllObjects.Singleton.MoveUI[i].SetActive(true);
        }

        Character.Singleton.IsDead = false;
        Ads.Singleton.InterstitialShowed = false;
    }

    public void CameraShoot()
    {
        if (!_isAfraid && gameObject.activeSelf)
        {
            StartCoroutine(CamShoot());
        }
    }

    public void GetDamage(float damage)
    {
        Hp -= damage;

        if(Hp <= 0 && _tag == "MiniSiren")
        {
            StartCoroutine(SirenDead());
        }

        if (_partNumber == 1)
        {
            if (AllObjects.Singleton.FirstPartSirenHeadScript.Hp <= 0)
            {
                AllObjects.Singleton.AnalyticsEvent.OnEvent("1PSirenIsDead");
                _isDead = true;
                _anim.SetInteger("State", 3);
                _sirenTimer = 15f;
                AllObjects.Singleton.SirenTimerText.gameObject.SetActive(true);
                AllObjects.Singleton.MiniSirenDead.PlayOneShot(AllObjects.Singleton.MiniSirenDead.clip);
            }
            AllObjects.Singleton.HpBar.value = AllObjects.Singleton.FirstPartSirenHeadScript.Hp;
        }
        else if (_partNumber == 3)
        {
            if (AllObjects.Singleton.ThirdPartSirenHeadScript.Hp <= 0 && _tag != "MiniSiren")
            {
                StartCoroutine(Winner());
            }
            AllObjects.Singleton.HpBar.value = AllObjects.Singleton.ThirdPartSirenHeadScript.Hp;
        }
    }

    IEnumerator CamShoot()
    {
        AllObjects.Singleton.CameraShootButton.fillAmount = 0;
        AllObjects.Singleton.CameraShootButtonComponent.interactable = false;

        _isAfraid = true;
        yield return new WaitForSeconds(5);
        _isAfraid = false;
    }

    IEnumerator HowToPlay()
    {
        yield return new WaitForSeconds(2);
        AllObjects.Singleton.HowToPlay[0].SetActive(true);
        yield return new WaitForSeconds(5);
        AllObjects.Singleton.HowToPlay[0].SetActive(false);
        yield return new WaitForSeconds(1);
        AllObjects.Singleton.HowToPlay[1].SetActive(true);
        yield return new WaitForSeconds(5);
        AllObjects.Singleton.HowToPlay[1].SetActive(false);
        PlayerPrefs.SetInt("Dialog", 1);
    }

    IEnumerator SirenDead()
    {
        AllObjects.Singleton.MiniSirenDead.PlayOneShot(AllObjects.Singleton.MiniSirenDead.clip);
        if (_partNumber == 3)
        {
            _isDead = true;
            _anim.SetInteger("State", 3);
            yield return new WaitForSeconds(4);
            Destroy(gameObject);
        }
        else if(_partNumber == 4)
        {
            _isDead = true;
            yield return new WaitForSeconds(3);
            gameObject.SetActive(false);
        }
        else if(_partNumber == 6)
        {
            _isDead = true;
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }

    IEnumerator Winner()
    {
        AllObjects.Singleton.AnalyticsEvent.OnEvent("3PFinish");
        _isDead = true; 
        _anim.SetInteger("State", 3);
        for (int i = 0; i < AllObjects.Singleton.SirenWinUi.Length; i++)
        {
            AllObjects.Singleton.SirenWinUi[i].SetActive(false);
        }
        yield return new WaitForSeconds(10);
        Detail.Singleton.IsFinish = true;
        AllObjects.Singleton.ThirdFinishPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        AllObjects.Singleton.ThirdFinishText.SetActive(true);
    }
}
