using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    private float startTime;
    private bool timerRunning = false;

    void Update()
    {
        if (timerRunning)
        {
            float timePassed = Time.time - startTime;
            string minutes = ((int)timePassed / 60).ToString("00");
            string seconds = (timePassed % 60).ToString("00.00");
            TimerText.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        if (!timerRunning)
        {
            startTime = Time.time;
            timerRunning = true;
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void Win()
    {
        float finishTime = Time.time - startTime;
        string formattedTime = FormatTime(finishTime);

        GameObject winCanvas = GameObject.Find("WinCanvas");
        if (winCanvas != null)
        {
            TextMeshProUGUI finalTimeText = winCanvas.transform.Find("MenuBG/FinalTime").GetComponent<TextMeshProUGUI>();
            if (finalTimeText != null)
            {
                finalTimeText.text = formattedTime;
            }
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        float seconds = time % 60;
        return string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }
}
