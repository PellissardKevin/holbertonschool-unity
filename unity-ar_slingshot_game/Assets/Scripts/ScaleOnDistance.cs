using UnityEngine;

/// <summary>
/// Manages the scale of the target depending on the distance with the camera.
/// </summary>
public class ScaleOnDistance : MonoBehaviour
{
    private Transform _arCamera;

    private void Start() => _arCamera = Camera.main.transform;

    private void Update()
    {
        transform.localScale = CalculateScaleFactor(Vector3.Distance(transform.position, _arCamera.position));
    }

    private Vector3 CalculateScaleFactor(float distance)
    {
        float scaleFactor = Mathf.Clamp01(1 - (distance / 5f));

        scaleFactor = Mathf.Lerp(0.0002f, 0.15f, scaleFactor);

        return new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
