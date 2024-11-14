using UnityEngine;

public class Explosion : MonoBehaviour
{
	private Animator _anim;

	private void Start() => _anim = GameObject.FindWithTag("Player").GetComponent<Animator>();

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Interactable"))
		{
			transform.GetChild(1).gameObject.SetActive(true);
			if (!CheckXRDevice.isPresent())
			{
				_anim.ResetTrigger("BallPresent");
				_anim.SetTrigger("BallDestroyed");
			}
			Destroy(gameObject);
			Destroy(collision.gameObject);
		}
	}
}
