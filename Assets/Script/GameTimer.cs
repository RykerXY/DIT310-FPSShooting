using UnityEngine;
using System.Collections;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 60f;
    public float delayBeforeStart = 3f; 
    public TextMeshProUGUI timerText; 

    private float currentTime;
    private bool isCounting = false;

    void Start()
    {
        currentTime = startTime;
        StartCoroutine(StartCountdownWithDelay());
    }

    IEnumerator StartCountdownWithDelay()
    {
        for (int i = (int)delayBeforeStart; i > 0; i--)
        {
            if (timerText != null)
                timerText.text = i.ToString() + "...";
            yield return new WaitForSeconds(1f);
        }

        isCounting = true;
    }

    void Update()
    {
        if (isCounting && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timerText.text = FormatTime(currentTime);
        }
    }

    string FormatTime(float time)
    {
        time = Mathf.Max(0, time);
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}
