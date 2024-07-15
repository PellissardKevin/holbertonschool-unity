using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timer playerTimer = other.GetComponent<Timer>();
            if (playerTimer != null)
            {
                playerTimer.enabled = true; // Enable the Timer script
                playerTimer.StartTimer(); // Start the timer
            }
        }
    }
}
