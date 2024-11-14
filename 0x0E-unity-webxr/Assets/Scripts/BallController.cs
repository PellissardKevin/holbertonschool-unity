using UnityEngine;

public class BallController : MonoBehaviour
{
	public Animator anim;
	public Rigidbody rb;
	public float speed = 15f;
	private Vector3 _moveDirection;

	private void Start() => Destroy(gameObject, 20f);

	private void Update() => TakeInput();

	private void TakeInput() => _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;

	private void FixedUpdate() => MovePlayer();

	private void MovePlayer() => rb.AddForce(10f * speed * _moveDirection, ForceMode.Force);

	private void OnDestroy()
	{
		if (!CheckXRDevice.isPresent())
		{
			anim?.ResetTrigger("BallPresent");
			anim?.SetTrigger("BallDestroyed");
		}
	}
}
