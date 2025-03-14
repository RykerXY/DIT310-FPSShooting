using TMPro;
using UnityEngine;

public class CheckExcrement : MonoBehaviour
{
    //If excrement is up to X amount, game over
    public int maxExcrement = 10;
    public GameObject gameOverPanel;
    public TextMeshProUGUI excrementCount;
    public GameObject WarningDP;

    void Update()
    {
        excrementCount.text = GameObject.FindGameObjectsWithTag("Excrement").Length.ToString();
        if (GameObject.FindGameObjectsWithTag("Excrement").Length >= maxExcrement)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        if (GameObject.FindGameObjectsWithTag("Excrement").Length >= maxExcrement - 2)
        {
            WarningDP.SetActive(true);
        }
        else
        {
            WarningDP.SetActive(false);
        }
    }
}
