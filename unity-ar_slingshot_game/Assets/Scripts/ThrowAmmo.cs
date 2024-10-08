using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ThrowAmmo : MonoBehaviour
{
    [SerializeField]
    private float _dragDistanceMax;
    [SerializeField]
    private float _maxDistanceSelection = 5f;
    [SerializeField]
    private FollowCamera _followCamera;
    private Vector3 _originalPosition;
    private bool _isDragging = false;
    private bool _isThrown = false;

    private void Update() => OnFingerDown();

    private void OnFingerDown()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, _maxDistanceSelection))
                {
                    if (hit.transform.tag == "Ammo")
                    {
                        _isDragging = true;
                        _originalPosition = transform.position;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && _isDragging)
            {
                transform.position = touch.position.y > _originalPosition.y - _dragDistanceMax ? 
                                     touch.position : new Vector2(touch.position.x, _originalPosition.y - _dragDistanceMax);
            }
        }
    }
}
