using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;
    public GameObject gameoverUI;
    public GameObject gun;
    public Collider HealArea;
    public Image health1;
    public Image health2;
    public Image health3;
    public Image health4;
    public Image health5;

    private bool isInvincible = false;
    private float invincibleTime = 1f;
    private float invincibleTimer = 0f;

    void Update()
    {
        if (health == 4)
        {
            health5.enabled = false;
        }
        else if (health == 3)
        {
            health4.enabled = false;
        }
        else if (health == 2)
        {
            health3.enabled = false;
        }
        else if (health == 1)
        {
            health2.enabled = false;
        }
        else if (health == 0)
        {
            health1.enabled = false;
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        Debug.Log("Player Health: " + health);
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            isInvincible = true;
            invincibleTimer = invincibleTime;
        }
    }

    void Die()
    {
        gameoverUI.SetActive(true);
        Destroy(gun);
    }

    void OnTriggerStay(Collider other)
    {
        if (other == HealArea)
        {
            health = 5;
            health1.enabled = true;
            health2.enabled = true;
            health3.enabled = true;
            health4.enabled = true;
            health5.enabled = true;
        }
    }
}
