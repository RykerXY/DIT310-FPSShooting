using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 5f;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public Animator gunAnimator;
    public Camera fpsCamera;
    public LayerMask hitLayers;
    public AudioSource audioSource;
    public AudioClip FireSound;
    public AudioClip ReloadSound;

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunAnimator.SetTrigger("Shoot");
        audioSource.PlayOneShot(FireSound);
        
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range, hitLayers))
        {
            Debug.Log("Hit: " + hit.transform.name);
            //If layer is enemy
            if (hit.transform.gameObject.layer == 6)
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage, hit.point, -hit.normal);
                }
            }
        }
    }
}
