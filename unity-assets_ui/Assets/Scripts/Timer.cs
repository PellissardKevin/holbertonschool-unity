using UnityEngine;
using UnityEngine.UI;

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
}
