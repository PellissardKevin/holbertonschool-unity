using UnityEngine;
using Vuforia;

public class MarkerDetectionAnimation : MonoBehaviour
{
    private Animator animator;
    private ObserverBehaviour observerBehaviour;
    public GameObject[] buttons;  // Assign your buttons in the Inspector

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponentInChildren<Animator>();

        // Get the ObserverBehaviour component (ImageTargetBehaviour)
        observerBehaviour = GetComponent<ObserverBehaviour>();

        // Subscribe to the OnTargetStatusChanged event
        observerBehaviour.OnTargetStatusChanged += OnTrackableStateChanged;

        // Initially hide buttons
        SetButtonsActive(false);
    }

    private void OnTrackableStateChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        bool isTargetVisible = targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED;
        Debug.Log("Target status changed. IsTargetVisible: " + isTargetVisible);

        if (isTargetVisible)
        {
            // Target is detected, start or continue the animation and show buttons
            animator.SetBool("IsTargetVisible", true);
            SetButtonsActive(true);

            // Play button animations if needed
            foreach (GameObject button in buttons)
            {
                Animator buttonAnimator = button.GetComponent<Animator>();
                if (buttonAnimator != null)
                {
                    Debug.Log("Activating button animation for: " + button.name);
                    buttonAnimator.SetBool("IsTargetVisible", true);
                }
                else
                {
                    Debug.LogWarning("No Animator found on button: " + button.name);
                }
            }
        }
        else
        {
            // Target is lost, stop the animation and hide buttons
            animator.SetBool("IsTargetVisible", false);
            SetButtonsActive(false);

            // Stop button animations if needed
            foreach (GameObject button in buttons)
            {
                Animator buttonAnimator = button.GetComponent<Animator>();
                if (buttonAnimator != null)
                {
                    Debug.Log("Deactivating button animation for: " + button.name);
                    buttonAnimator.SetBool("IsTargetVisible", false);
                }
                else
                {
                    Debug.LogWarning("No Animator found on button: " + button.name);
                }
            }
        }
    }

    private void SetButtonsActive(bool isActive)
    {
        Debug.Log("Setting buttons active state to: " + isActive);
        // Toggle visibility of buttons
        foreach (GameObject button in buttons)
        {
            button.SetActive(isActive);
        }
    }
}
