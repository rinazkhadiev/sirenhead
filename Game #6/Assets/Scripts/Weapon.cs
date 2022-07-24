using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private RaycastHit _hit;

    private float _damage = 1f;
    private int _bulletValue = 10;
    private bool _aiming;
    private Camera _camera;

    private int _gunNumber;

    private void Start()
    {
        _camera = Camera.main;

        if (PlayerPrefs.GetInt("Part") == 1)
        {
            AllObjects.Singleton.AimUI.SetActive(true);
            AllObjects.Singleton.ShootButton.SetActive(true);
            AllObjects.Singleton.HpBar.gameObject.SetActive(true);
            AllObjects.Singleton.BulletValueText.gameObject.SetActive(true);
            AllObjects.Singleton.WeaponReloadButton.gameObject.SetActive(true);
            AllObjects.Singleton.GunsPanel.SetActive(true);
            AllObjects.Singleton.SirenIsStop = true;
        }
        else if (PlayerPrefs.GetInt("Part") == 3)
        {
            AllObjects.Singleton.AimUI.SetActive(true);
            AllObjects.Singleton.ShootButton.SetActive(true);
            AllObjects.Singleton.HpBar.gameObject.SetActive(true);
            AllObjects.Singleton.BulletValueText.gameObject.SetActive(true);
            AllObjects.Singleton.WeaponReloadButton.gameObject.SetActive(true);
            AllObjects.Singleton.GunsPanel.SetActive(true);
            AllObjects.Singleton.SirenIsStop = true;
        }
        else if (PlayerPrefs.GetInt("Part") == 4)
        {
            AllObjects.Singleton.AimUI.SetActive(true);
            AllObjects.Singleton.ShootButton.SetActive(true);
            _bulletValue = 10000;
            AllObjects.Singleton.GunsPanel.SetActive(true);
            AllObjects.Singleton.SirenIsStop = true;
        }
        else if (PlayerPrefs.GetInt("Part") == 6)
        {
            AllObjects.Singleton.AimUI.SetActive(true);
            AllObjects.Singleton.ShootButton.SetActive(true);
            AllObjects.Singleton.WeaponReloadButton.gameObject.SetActive(true);
            AllObjects.Singleton.GunsPanel.SetActive(true);
            AllObjects.Singleton.SirenIsStop = true;
        }
    }

    private void Update()
    {
        if (_aiming)
        {
            _camera.fieldOfView = AllObjects.Singleton.AimSlider.value;
        }
    }

    public void Shoot()
    {
        if (_bulletValue > 0)
        {
            if (Physics.Raycast(AllObjects.Singleton.RaycastTarget.transform.position, AllObjects.Singleton.RaycastTarget.transform.forward, out _hit))
            {   
                if (_hit.collider.GetComponent<SirenHead>())
                {
                    _hit.collider.GetComponent<SirenHead>().GetDamage(_damage);
                }
            }

            AllObjects.Singleton.WeaponShootSound.PlayOneShot(AllObjects.Singleton.WeaponShootSound.clip);
            AllObjects.Singleton.ShootVFX[_gunNumber - 1].Play();

            _bulletValue--;
            AllObjects.Singleton.BulletValueText.text = $"{_bulletValue}/10";
        }
        else
        {
            StartCoroutine(WeaponReloadWait());
        }
    }

    public void WeaponReload()
    {
        if (_bulletValue <= 9)
        {
            StartCoroutine(WeaponReloadWait());
        }
    }

    public void SelectWeapon(int number)
    {
        if(number == 3)
        {
            _damage = 2;
        }
        _gunNumber = number;

        AllObjects.Singleton.WeaponShootSound.clip = AllObjects.Singleton.WeaponSounds[_gunNumber - 1];
        AllObjects.Singleton.AnalyticsEvent.OnEvent("WeaponSelected");
        AllObjects.Singleton.SirenIsStop = false;
    }

    public void Aiming()
    {
        _aiming = true;
    }

    public void AimingOff()
    {
        _camera.fieldOfView = 75;
        _aiming = false;
    }


    IEnumerator WeaponReloadWait()
    {
        AllObjects.Singleton.ShootButton.GetComponent<Button>().interactable = false;
        AllObjects.Singleton.WeaponReloadButton.GetComponent<Button>().interactable = false;
        AllObjects.Singleton.WeaponReloadSound.PlayOneShot(AllObjects.Singleton.WeaponReloadSound.clip);
        yield return new WaitForSeconds(2);
        _bulletValue = 10;
        AllObjects.Singleton.BulletValueText.text = $"{_bulletValue}/10";
        AllObjects.Singleton.WeaponReloadButton.GetComponent<Button>().interactable = true;
        AllObjects.Singleton.ShootButton.GetComponent<Button>().interactable = true;
    }
}
