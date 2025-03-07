using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;
    public GameObject gameoverUI;
    public Image health1;
    public Image health2;
    public Image health3;
    public Image health4;
    public Image health5;

    void Update()
    {
        // Disable health image based on health value
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
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameoverUI.SetActive(true);
        Destroy(gameObject);
    }   
}
