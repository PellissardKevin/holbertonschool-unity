using UnityEngine;

/// <summary>
/// Makes the ammunitions follow the camera.
/// </summary>
public class FollowCamera : MonoBehaviour
{
    private void Update() => transform.position = Camera.main.transform.position + Camera.main.transform.forward;
}
