using UnityEngine;

public class DestroyBall : MonoBehaviour
{
	[SerializeField] private Animator _anim;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Interactable"))
		{
			if (!CheckXRDevice.isPresent())
			{
				_anim.ResetTrigger("BallPresent");
				_anim.SetTrigger("BallDestroyed");
			}
			Destroy(other.gameObject);
		}
	}
}
