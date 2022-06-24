using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IDragHandler
{
    private Transform _cameraTransform;
    private float _moveX;
    private float _moveY;

    [SerializeField] private float _sensitivity = 6f;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;

        if (PlayerPrefs.HasKey("Sens"))
        {
            AllObjects.Singleton.SensitivityBar.value = PlayerPrefs.GetFloat("Sens");
        }
        else
        {
            AllObjects.Singleton.SensitivityBar.value = 6f;
        }

        if (PlayerPrefs.GetInt("Part") == 1)
        {
            _moveX = 87;
        }
        else if(PlayerPrefs.GetInt("Part") == 3)
        {
            _moveX = 136;
        }
    }
    private void Update()
    {
        _sensitivity = AllObjects.Singleton.SensitivityBar.value;
        if (AllObjects.Singleton.SettingsMenu.activeSelf)
        {
            PlayerPrefs.SetFloat("Sens", AllObjects.Singleton.SensitivityBar.value);
        }

        _cameraTransform.position = Character.Singleton.Transform.position;
        _cameraTransform.rotation = Quaternion.Euler(_moveY, _moveX, 0);
        Character.Singleton.Transform.rotation = Quaternion.Euler(new Vector3(0, _moveX, 0));
    } 

    public void OnDrag(PointerEventData eventData)
    {
        _moveY -= eventData.delta.y / _sensitivity;
        _moveY = Mathf.Clamp(_moveY, -40, 40);

        _moveX += eventData.delta.x / _sensitivity;
        if (_moveX < -360) _moveX += 360;
        if (_moveX > 360) _moveX -= 360;
        _moveX = Mathf.Clamp(_moveX, -360, 360);
    }
}
