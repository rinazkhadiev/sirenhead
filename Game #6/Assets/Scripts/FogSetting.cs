using UnityEngine;
using UnityEngine.Rendering;

public class FogSetting : MonoBehaviour
{
    [SerializeField] private Material _noFog;
    [SerializeField] private Material _fog;

    [SerializeField] private Volume _volume;
    [SerializeField] private VolumeProfile _noFogProfile;
    [SerializeField] private VolumeProfile _fogProfile;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Part") == 1 || PlayerPrefs.GetInt("Part") == 2 || PlayerPrefs.GetInt("Part") == 5 || PlayerPrefs.GetInt("Part") == 6)
        {
            if (PlayerPrefs.GetInt("Fog") == 0)
            {
                RenderSettings.skybox = _noFog;
                RenderSettings.fog = false;
                _volume.profile = _noFogProfile;
            }
            else
            {
                RenderSettings.skybox = _fog;
                RenderSettings.fog = true;
                _volume.profile = _fogProfile;
            }
        }
        else if (PlayerPrefs.GetInt("Part") == 3)
        {
            RenderSettings.skybox = _fog;
            RenderSettings.fog = true;
            _volume.profile = _fogProfile;
        }
        else if (PlayerPrefs.GetInt("Part") == 4)
        {
            if (PlayerPrefs.GetInt("Fog") == 0)
            {
                RenderSettings.skybox = _noFog;
                RenderSettings.fog = false;
            }
            else
            {
                RenderSettings.skybox = _fog;
                RenderSettings.fog = true;
            }
            _volume.profile = _noFogProfile;
        }
    }
}
