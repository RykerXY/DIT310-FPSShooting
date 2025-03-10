using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool IsDoDamage = true;
    public bool PushBack = true;
    public float health = 50f;
    public float deathForce = 10f;
    public GameObject hitEffectPrefab;
    public GameObject deathEffectPrefab;
    public AudioClip hitSound;
    public float destroyDelay = 2f;
    private NavMeshAgent agent;
    private PlayerHealth playerHealth; 
    private NavmeshController navmeshController;

    private Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip attackSound;

    void Start()
    {
        navmeshController = GetComponent<NavmeshController>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        playerHealth = GameObject.FindAnyObjectByType<PlayerHealth>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>(); // เพิ่ม AudioSource ถ้าไม่มี
    }
    void Update()
    {
        if(playerHealth == null)
        {
            Destroy(this);
        }
    }

    public void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitForce)
    {
        health -= damage;

        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(hitEffect, 1.5f);
        }

        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (health <= 0)
        {
            Destroy(agent);
            Destroy(navmeshController);
            Die(hitPoint, hitForce);
        }
    }

    void Die(Vector3 hitPoint, Vector3 hitForce)
    {
        if (deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 2f);
        }

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForceAtPosition(hitForce * deathForce, hitPoint, ForceMode.Impulse);
        }

        Destroy(gameObject, destroyDelay);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (IsDoDamage)
            {
                audioSource.PlayOneShot(attackSound);

                playerHealth.TakeDamage(1);
                Debug.Log("Player Health: " + playerHealth.health);
            }

            if (PushBack)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0;
                pushDirection.Normalize();

                CharacterController playerController = collision.gameObject.GetComponent<CharacterController>();
                if (playerController != null)
                {
                    StartCoroutine(ApplyKnockback(playerController, pushDirection, 12f, 0.5f));
                }
            }
            

        }
    }

    IEnumerator ApplyKnockback(CharacterController playerController, Vector3 direction, float force, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            float currentForce = Mathf.Lerp(force, 0, timer / duration);
            playerController.Move(direction * currentForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
