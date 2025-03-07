using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public float deathForce = 10f;
    public GameObject hitEffectPrefab; // Particle Effect ตอนโดนยิง
    public GameObject deathEffectPrefab; // Particle Effect ตอนตาย
    public AudioClip hitSound; // เสียงตอนโดนยิง
    //public AudioClip deathSound; // เสียงตอนตาย
    public float destroyDelay = 2f; // เวลาที่ศพจะหายไป

    private Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>(); // เพิ่ม AudioSource ถ้าไม่มี
    }

    public void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitForce)
    {
        health -= damage;

        // เอฟเฟกต์ Particle ตอนโดนยิง
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(hitEffect, 1.5f); // ทำลาย Effect หลัง 1.5 วินาที
        }

        // เล่นเสียงตอนโดนยิง
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // ถ้าตาย
        if (health <= 0)
        {
            Die(hitPoint, hitForce);
        }
    }

    void Die(Vector3 hitPoint, Vector3 hitForce)
    {
        // เอฟเฟกต์ Particle ตอนตาย
        if (deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 2f); // ทำลาย Effect หลัง 2 วินาที
        }

        // เล่นเสียงตอนตาย
        /*if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }*/

        // เพิ่มแรงกระแทกตอนตาย
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForceAtPosition(hitForce * deathForce, hitPoint, ForceMode.Impulse);
        }

        Destroy(gameObject, destroyDelay); // ทำลาย Enemy หลังจากเวลาที่กำหนด
    }
}
