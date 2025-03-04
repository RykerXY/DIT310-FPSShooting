using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public float deathForce = 10f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitForce)
    {
        health -= damage;

        // ถ้าตาย
        if (health <= 0)
        {
            Die(hitPoint, hitForce);
        }
    }

    void Die(Vector3 hitPoint, Vector3 hitForce)
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForceAtPosition(hitForce * deathForce, hitPoint, ForceMode.Impulse);
        }

        Destroy(gameObject, 2f);
    }
}
