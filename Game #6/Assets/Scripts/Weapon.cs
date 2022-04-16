using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private RaycastHit _hit;

    private float _damage = 1f;
    private int _bulletValue = 10;


    private void Start()
    {
        if (PlayerPrefs.GetInt("Part") == 3)
        {
            AllObjects.Singleton.Weapon.SetActive(true);
            AllObjects.Singleton.AimUI.SetActive(true);
            AllObjects.Singleton.ShootButton.SetActive(true);
            AllObjects.Singleton.HpBar.gameObject.SetActive(true);
            AllObjects.Singleton.BulletValueText.gameObject.SetActive(true);
            AllObjects.Singleton.WeaponReloadButton.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Part") == 4)
        {
            AllObjects.Singleton.Weapon.SetActive(true);
            AllObjects.Singleton.AimUI.SetActive(true);
            AllObjects.Singleton.ShootButton.SetActive(true);
            _bulletValue = 10000;
        }
        else if (PlayerPrefs.GetInt("Part") == 6)
        {
            AllObjects.Singleton.Weapon.SetActive(true);
            AllObjects.Singleton.AimUI.SetActive(true);
            AllObjects.Singleton.ShootButton.SetActive(true);
            AllObjects.Singleton.WeaponReloadButton.gameObject.SetActive(true);
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

            AllObjects.Singleton.WeaponAnim.Play("Fire");
            AllObjects.Singleton.WeaponShootSound.PlayOneShot(AllObjects.Singleton.WeaponShootSound.clip);
            AllObjects.Singleton.ShootVFX.Play();

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
