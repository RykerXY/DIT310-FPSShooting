using UnityEngine;

public class Gameover : MonoBehaviour
{
    public bool isGameover;

    public GameObject gameoverUI;
    public AudioSource audioSource;
    public AudioClip gameoverSound;
    bool isPlay = false;
    
    void Update()
    {
        CheckifGameOver();
        if(isGameover) DestroyEnemy();
    }

    void CheckifGameOver()
    {
        if(gameoverUI.activeSelf)
        {
            isGameover = true;
            if(isGameover) GameOver();
        }
    }

    void GameOver()
    {
        if (!isPlay)
        {
            audioSource.PlayOneShot(gameoverSound);
            isPlay = true;
        }
    }
    //Destroy everyobject with tag "Enemy"
    void DestroyEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
