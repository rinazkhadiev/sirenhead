using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public static Character Singleton { get; private set; }
    public bool IsDead { get; set; }
    public Transform Transform { get; private set; }
    public Vector3 SpawnPosition { get; set; }
    public CharacterController CharController { get; set; }

    private Transform _cameraTransform;
    private float _outWorldCount = 5;

    private int _partNumber;

    private int _miniSirenCounter;
    private float _miniSirenTimer;
    private float _miniSirenDeltaTime;
    private GameObject _miniSirenObject;

    private int _cartoonsCounter;
    private float _cartoonsTimer = 10;
    private float _cartoonDeltaTime;
    private GameObject _cartoon;

    [SerializeField] private float _stepTime;
    private bool _isStepping;

    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private bool _isJumping;

    private void Start()
    {
        Singleton = this;
        CharController = GetComponent<CharacterController>();
        Transform = GetComponent<Transform>();
        _cameraTransform = Camera.main.transform;
        _partNumber = PlayerPrefs.GetInt("Part");
        _miniSirenTimer = Random.Range(20, 30);

        if (PlayerPrefs.HasKey("Graphics") && PlayerPrefs.GetInt("Graphics") == 0)
        {
            AllObjects.Singleton.VolumeGameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (AllObjects.Singleton.CameraShootButton.fillAmount < 1)
        {
            AllObjects.Singleton.CameraShootButton.fillAmount += Time.deltaTime / 10;
            RenderSettings.fogDensity = 0.03f;
        }
        else if (AllObjects.Singleton.CameraShootButton.fillAmount >= 1)
        {
            AllObjects.Singleton.CameraShootButtonComponent.interactable = true;
        }

        if (_partNumber == 3)
        {
            _miniSirenDeltaTime += Time.deltaTime;

            if (AllObjects.Singleton.ThirdPartSirenHeadScript.Hp > 50)
            {
                if (_miniSirenDeltaTime >= _miniSirenTimer && _miniSirenCounter < 50)
                {
                   _miniSirenObject =  Instantiate(AllObjects.Singleton.MiniSirenPrefab, AllObjects.Singleton.MiniSirenPrefab.transform.position, Quaternion.identity);
                    _miniSirenObject.SetActive(true);
                    _miniSirenDeltaTime = 0;
                    _miniSirenTimer = Random.Range(20, 30);
                }
            }
            else if (AllObjects.Singleton.ThirdPartSirenHeadScript.Hp < 50 && (AllObjects.Singleton.ThirdPartSirenHeadScript.Hp > 25))
            {
                if (_miniSirenDeltaTime >= _miniSirenTimer && _miniSirenCounter < 50)
                {
                    _miniSirenObject = Instantiate(AllObjects.Singleton.MiniSirenPrefab, AllObjects.Singleton.MiniSirenPrefab.transform.position, Quaternion.identity);
                    _miniSirenObject.SetActive(true);
                    _miniSirenDeltaTime = 0;
                    _miniSirenTimer = Random.Range(10, 15);
                }
            }
            else if (AllObjects.Singleton.ThirdPartSirenHeadScript.Hp < 25)
            {
                if (_miniSirenDeltaTime >= _miniSirenTimer && _miniSirenCounter < 50)
                {
                    _miniSirenObject = Instantiate(AllObjects.Singleton.MiniSirenPrefab, AllObjects.Singleton.MiniSirenPrefab.transform.position, Quaternion.identity);
                    _miniSirenObject.SetActive(true);
                    _miniSirenDeltaTime = 0;
                    _miniSirenTimer = Random.Range(5, 10);
                }
            }

            if (AllObjects.Singleton.CameraShootButton.fillAmount >= 1)
            {
                if (AllObjects.Singleton.ThirdPartSirenHeadScript.Hp > 50)
                {
                    RenderSettings.fogDensity = 0.03f;
                }
                else if (AllObjects.Singleton.ThirdPartSirenHeadScript.Hp <= 50)
                {
                    if (RenderSettings.fogDensity <= 0.3f)
                    {
                        RenderSettings.fogDensity += Time.deltaTime / 100f;
                    }
                }
            }
        }

        if (_partNumber == 6)
        {
            _cartoonDeltaTime += Time.deltaTime;

            if (_cartoonDeltaTime >= _cartoonsTimer && _cartoonsCounter < 50)
            {
                _cartoon =  Instantiate(AllObjects.Singleton.MiniCartoonPrefabA, AllObjects.Singleton.MiniCartoonPrefabA.transform.position, Quaternion.identity);
                _cartoon.SetActive(true);

                _cartoon = Instantiate(AllObjects.Singleton.MiniCartoonPrefabB, AllObjects.Singleton.MiniCartoonPrefabB.transform.position, Quaternion.identity);
                _cartoon.SetActive(true);

                _cartoonsCounter += 2;

                AllObjects.Singleton.CartoonSlider.value = _cartoonsCounter;
                AllObjects.Singleton.CartoonText.text = $"{50 - _cartoonsCounter}";

                _cartoonsTimer -= 0.2f;
                _cartoonDeltaTime = 0;
            }
        }


        _groundedPlayer = CharController.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        CharController.Move(AllObjects.Singleton.JoyController.Horizontal() * _cameraTransform.right * Time.deltaTime * _playerSpeed);
        CharController.Move(AllObjects.Singleton.JoyController.Vertical() * _cameraTransform.forward * Time.deltaTime * _playerSpeed);

        if (!_isStepping && !_isJumping)
        {
            if (CharController.velocity.magnitude > 4f)
            {
                _stepTime = 0.5f;
                StartCoroutine(Step());
            }
            else if (CharController.velocity.magnitude > 2f && CharController.velocity.magnitude < 4f)
            {
                _stepTime = 0.8f;
                StartCoroutine(Step());
            }
            else if (CharController.velocity.magnitude < 2f && CharController.velocity.magnitude > 0.1f)
            {
                _stepTime = 1.5f;
                StartCoroutine(Step());
            }
        }

        if (_isJumping && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        CharController.Move(_playerVelocity * Time.deltaTime);

        if(!_groundedPlayer && Transform.position.y < -10f)
        {
            if(_partNumber == 2)
            {
                Transform.position = new Vector3(Transform.position.x, 30f, Transform.position.z);
            }
            else if(_partNumber == 6) 
            {
                Transform.position = AllObjects.Singleton.FivePartCharPos.transform.position;
            }
            else
            {
                Transform.position = new Vector3(Transform.position.x, 2f, Transform.position.z);
            }
        }
    }

    public void Jump()
    {
        StartCoroutine(JumpWait());
    }

    IEnumerator JumpWait()
    {
        _isJumping = true;
        yield return new WaitForSeconds(0.1f);
        _isJumping = false;
    }

    IEnumerator Step()
    {
        _isStepping = true;
        if (_partNumber == 1 || _partNumber == 3 || _partNumber == 6)
        {
            AllObjects.Singleton.StepAudio.PlayOneShot(AllObjects.Singleton.FirstSteps[Random.Range(0, AllObjects.Singleton.FirstSteps.Length)]);
        }
        else
        {
            AllObjects.Singleton.StepAudio.PlayOneShot(AllObjects.Singleton.SecondSteps[Random.Range(0, AllObjects.Singleton.SecondSteps.Length)]);
        }
        yield return new WaitForSeconds(_stepTime);
        _isStepping = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "DeadZone")
        {
            AllObjects.Singleton.DeadZoneVinet.SetActive(true);

            if (_outWorldCount > 0)
            {
                AllObjects.Singleton.OutWorldCountText.gameObject.SetActive(true);
                _outWorldCount -= Time.deltaTime;
                AllObjects.Singleton.OutWorldCountText.text = $"{(int)_outWorldCount}";
            }
            else
            {
                if (_partNumber == 1)
                {
                    Transform.position = Vector3.MoveTowards(Transform.position, AllObjects.Singleton.FirstPartSirenHead.transform.position, 1f);
                    Transform.position = new Vector3(Transform.position.x, 3f, Transform.position.z);
                }
                if (_partNumber == 3)
                {
                    Transform.position = Vector3.MoveTowards(Transform.position, AllObjects.Singleton.ThirdPartSirenHead.transform.position, 1f);
                    Transform.position = new Vector3(Transform.position.x, 3f, Transform.position.z);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "DeadZone")
        {
            AllObjects.Singleton.DeadZoneVinet.SetActive(false);
            AllObjects.Singleton.OutWorldCountText.gameObject.SetActive(false);
            _outWorldCount = 5;
        }
    }

    public void Respawn()
    {
        Transform.position = SpawnPosition;
        SirenHead.Singleton.RespawnSet();
    }
}
