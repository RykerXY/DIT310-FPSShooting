using System.Collections;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 5f;
    public int maxAmmo = 6;

    [Header("References")]
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public Animator gunAnimator;
    public Camera fpsCamera;
    public LayerMask hitLayers;
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public TextMeshProUGUI ammoCountText;

    private bool isReloading = false;
    private int currentAmmo;
    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (currentAmmo > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else
            {
                Reload();
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        muzzleFlash.Play();
        gunAnimator.SetTrigger("Shoot");
        audioSource.PlayOneShot(fireSound);
        UpdateAmmoUI();
        Hit();
    }

    void Hit()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, range, hitLayers))
        {
            Debug.Log("Hit: " + hit.transform.name);
            if (hit.transform.gameObject.layer == 6) // Enemy Layer
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage, hit.point, -hit.normal);
                }
            }
        }
    }

    void Reload()
    {
        if (!isReloading)
        {
            isReloading = true;
            ammoCountText.text = "Reloading...";
            audioSource.PlayOneShot(reloadSound);
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(3f);
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoCountText.text = isReloading ? "Reloading..." : currentAmmo + " / " + maxAmmo;
    }
}
