using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform shootFX;

    [SerializeField] private Sprite gunSprite;
    [SerializeField] private Sprite smgSprite;


    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private Image selectedGunUI;
    [SerializeField] private TextMeshProUGUI selectedGunText;
    [SerializeField] private float fireRate = 15f;


    private float nextTimeToFire = 0f;

    public bool isGun = true;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunSpriteRenderer.sprite = gunSprite;
            selectedGunUI.sprite = gunSprite;
            selectedGunText.text = "Pistol";
            isGun = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunSpriteRenderer.sprite = smgSprite;
            selectedGunUI.sprite = smgSprite;
            selectedGunText.text = "SMG";
            isGun = false;
        }

        Shoot();
    }

    void Shoot()
    {
        if (isGun)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.instance.PlayerShoot();
                Instantiate(shootFX, shootPoint.position, shootPoint.rotation, shootPoint);
                GameObject bulletGO = Instantiate(bullet.gameObject, shootPoint.position, shootPoint.rotation);
                bulletGO.transform.rotation = shootPoint.rotation;
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            {
                AudioManager.instance.PlayerShoot();
                nextTimeToFire = Time.time + 1f / fireRate;
                    Instantiate(shootFX, shootPoint.position, shootPoint.rotation, shootPoint);
                    GameObject bulletGO = Instantiate(bullet.gameObject, shootPoint.position, shootPoint.rotation);
                    bulletGO.transform.rotation = shootPoint.rotation;
            }
        }
    }


}