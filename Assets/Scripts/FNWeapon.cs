using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class FNWeapon : MonoBehaviour
{
    public int damage = 15;
    public float fireRate = 1f;
    public float force = 155f;
    public float range = 15f;
    public ParticleSystem muzzleFlash;
    public Transform bulletSpawn;
    public AudioClip shotSFX;
    public AudioClip reloadSFX;
    public AudioSource _audioSource;
    public GameObject hitEffect;
    public Camera _cam;

    public FirstPersonController player;
    public float recoilX;
    public float recoilY;

    [SerializeField] public Text text;
    public Image aiming_point;

    [Space]
    public int curAmmoCount = 20;
    public int maxAmmoCount = 20;
    public int inventoryAmmoCount = 120;

    private bool isReload = false;
   

    void Update()
    {
        text.text = "" + curAmmoCount + " / " + inventoryAmmoCount;

        if (Input.GetButtonDown("Fire1") && curAmmoCount > 0 && isReload == false)
        {
            Shoot();
        }


        else if (Input.GetKeyDown(KeyCode.R) && inventoryAmmoCount > 0 && curAmmoCount < maxAmmoCount )
        {
            isReload = true;
            _audioSource.PlayOneShot(reloadSFX);
            Invoke("Reload", 3f);
        }
    }

    void Shoot()
    {
        curAmmoCount = curAmmoCount - 1;

        _audioSource.PlayOneShot(shotSFX);
        muzzleFlash.Play();

        RaycastHit hit;

        player.m_MouseLook.ChangeOffset(UnityEngine.Random.Range(0, recoilX), UnityEngine.Random.Range(-recoilY, recoilY));

        if (Physics.Raycast(_cam.transform.position,_cam.transform.forward,out hit,range))
        {
            Debug.Log(hit.collider);
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    void Reload()
    {
        int differenceAmmo = maxAmmoCount - curAmmoCount;
        if (inventoryAmmoCount >= differenceAmmo)
        {
            inventoryAmmoCount = inventoryAmmoCount - differenceAmmo;
            curAmmoCount = curAmmoCount + differenceAmmo;
            isReload = false;
        }
        else
        {
            curAmmoCount = curAmmoCount + inventoryAmmoCount;
            inventoryAmmoCount = 0;
            isReload = false;
        }
    }
}
