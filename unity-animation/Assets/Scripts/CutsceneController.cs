using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject mainCamera;
    public MonoBehaviour playerController;
    public GameObject timerCanvas;

    private Animator cutsceneAnimator;

    void Start()
    {
        cutsceneAnimator = GetComponent<Animator>();

        if (cutsceneAnimator == null)
        {
            Debug.LogError("Animator component missing from CutsceneCamera.");
            return;
        }

        // Ensure "Intro01" animation is set as the default state
        if (cutsceneAnimator.HasState(0, Animator.StringToHash("Intro01")))
        {
            cutsceneAnimator.Play("Intro01");
        }
        else
        {
            Debug.LogError("The 'Intro01' animation state could not be found in the Animator Controller.");
        }
    }

    void Update()
    {
        // Check if the animation is finished
        AnimatorStateInfo stateInfo = cutsceneAnimator.GetCurrentAnimatorStateInfo(0);

        // Check if the current animation is "Intro01" and its normalized time is >= 1
        if (stateInfo.IsName("Intro01") && stateInfo.normalizedTime >= 1.0f)
        {
            mainCamera.SetActive(true);
            playerController.enabled = true;
            timerCanvas.SetActive(true);
            this.enabled = false;  // Disable CutsceneController
        }
    }
}
