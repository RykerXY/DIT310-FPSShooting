using UnityEngine;

public class CheckExcrement : MonoBehaviour
{
    //If excrement is up to X amount, game over
    public int maxExcrement = 10;
    public GameObject gameOverPanel;

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Excrement").Length >= maxExcrement)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
}
