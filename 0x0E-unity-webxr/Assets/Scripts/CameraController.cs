using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float _mouseSensitivity = 2f;
	private float _camVerticalRotation = 0f;
	private float zPosition = -24f;

	void Update()
	{
		MoveCameraWithMouse();
		ZoomCamera();
	}

	private void MoveCameraWithMouse()
	{
		if (!CheckXRDevice.isPresent())
		{
			float inputX = Input.GetAxis("Mouse X") * _mouseSensitivity;
			float inputY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

			_camVerticalRotation -= inputY;
			_camVerticalRotation = Mathf.Clamp(_camVerticalRotation, -90f, 90f);

			transform.localEulerAngles = new Vector3(_camVerticalRotation, transform.localEulerAngles.y + inputX, 0f);
		}
	}

	private void ZoomCamera()
	{
		zPosition += Input.GetAxisRaw("Vertical") * 0.004f;

		transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
	}
}
