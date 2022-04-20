using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastScene : MonoBehaviour
{
    [SerializeField] private GameObject[] _firstPart;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _dialogText;
    [SerializeField] private GameObject _closeFace;
    [SerializeField] private AudioSource _accident;
    [SerializeField] private Transform _firstPartCar;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject[] _secondPart;
    [SerializeField] private Transform _secondPartCar;
    [SerializeField] private Image _loadBarImage;
    [SerializeField] private Text _loadText;
    private Transform _transform;
    private bool _go;
    [SerializeField] private GameObject _volumeGameObject;


    [SerializeField] private GameObject _nonCastPart;

    [SerializeField] private GameObject[] _secondDialog;
    [SerializeField] private GameObject[] _thirdDialog;
    [SerializeField] private GameObject[] _mazeDialog;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Part") == 1)
        {
            for (int i = 0; i < _firstPart.Length; i++)
            {
                _firstPart[i].SetActive(true);
            }

            StartCoroutine(Dialog());
            _transform = _firstPartCar;
        }
        else if(PlayerPrefs.GetInt("Part") == 2)
        {
            for (int i = 0; i < _secondPart.Length; i++)
            {
                _secondPart[i].SetActive(true);
            }

            StartCoroutine(SecondDialog());
            _transform = _secondPartCar;
        }
        else if (PlayerPrefs.GetInt("Part") == 3)
        {
            _nonCastPart.SetActive(true);
            StartCoroutine(ThirdDialog());
        }
        else if (PlayerPrefs.GetInt("Part") == 4)
        { 
	        _nonCastPart.SetActive(true);
            StartCoroutine(MazeDialog());
        }
        else if (PlayerPrefs.GetInt("Part") == 5 || PlayerPrefs.GetInt("Part") == 6)
        {
             _nonCastPart.SetActive(true);
            StartCoroutine(NonDialog());
        }

	if (PlayerPrefs.HasKey("Graphics") && PlayerPrefs.GetInt("Graphics") == 0)
        {
            _volumeGameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_go)
        {
            _transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    IEnumerator Dialog()
    {
        yield return new WaitForSeconds(5);
        _go = true;
        yield return new WaitForSeconds(0.5f);
        _dialogText.SetActive(false);
        yield return new WaitForSeconds(2);
        _accident.Play();
        _text.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        _closeFace.SetActive(true);
        yield return new WaitForSeconds(2);
        _loadBarImage.gameObject.SetActive(true);
        _loadText.gameObject.SetActive(true);
        StartCoroutine(AsyncLoad());
    }

    IEnumerator SecondDialog()
    {
        yield return new WaitForSeconds(5);
        _go = true;
        yield return new WaitForSeconds(0.5f);
        _secondDialog[0].SetActive(false);
        yield return new WaitForSeconds(2);
        _secondDialog[1].SetActive(true);
        yield return new WaitForSeconds(3);
        _secondDialog[1].SetActive(false);
        yield return new WaitForSeconds(2);
        _secondDialog[2].SetActive(true);
        yield return new WaitForSeconds(4);
        _secondDialog[2].SetActive(false);
        _secondDialog[3].SetActive(true);
        _loadBarImage.gameObject.SetActive(true);
        _loadText.gameObject.SetActive(true);
        StartCoroutine(AsyncLoad());
    }

    IEnumerator ThirdDialog()
    {
	_thirdDialog[0].SetActive(true);
        yield return new WaitForSeconds(3);
        _thirdDialog[0].SetActive(false);
        _thirdDialog[1].SetActive(true);
        yield return new WaitForSeconds(3);
        _thirdDialog[1].SetActive(false);
        _thirdDialog[2].SetActive(true);
        yield return new WaitForSeconds(3);
        _thirdDialog[2].SetActive(false);
        _thirdDialog[3].SetActive(true);
        yield return new WaitForSeconds(3);
        _thirdDialog[3].SetActive(false);
        _loadBarImage.gameObject.SetActive(true);
        _loadText.gameObject.SetActive(true);
        StartCoroutine(AsyncLoad());
    }

    IEnumerator MazeDialog()
    {
        _mazeDialog[0].SetActive(true);
        yield return new WaitForSeconds(3);
        _mazeDialog[0].SetActive(false);
        _mazeDialog[1].SetActive(true);
        yield return new WaitForSeconds(3);
        _mazeDialog[1].SetActive(false);
        _mazeDialog[2].SetActive(true);
        yield return new WaitForSeconds(3);
        _mazeDialog[2].SetActive(false);
        _loadBarImage.gameObject.SetActive(true);
        _loadText.gameObject.SetActive(true);
        StartCoroutine(AsyncLoad());
    }
    
    IEnumerator NonDialog()
    {
        yield return new WaitForSeconds(0.1f);
        _loadBarImage.gameObject.SetActive(true);
        _loadText.gameObject.SetActive(true);
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation opertaion = SceneManager.LoadSceneAsync("Play");
        while (!opertaion.isDone)
        {
            float progress = opertaion.progress / 0.9f;
            if(progress <= 0.98f)
            {
                _loadBarImage.fillAmount = progress;
                _loadText.text = string.Format("{0:0}%", progress * 100);
            }
            yield return null;
        }
    }
}
