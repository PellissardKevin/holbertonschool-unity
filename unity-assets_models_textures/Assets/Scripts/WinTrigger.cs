using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Timer timer; // Reference to the Timer script attached to the player
    public Text winText; // Reference to the Text component for displaying win message

    private bool hasWon = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            // Stop the timer
            timer.StopTimer();

            // Increase text size and change color
            winText.fontSize = 60;
            winText.color = Color.green;

            // Set flag to indicate player has won to prevent triggering multiple times
            hasWon = true;
        }
    }
}
