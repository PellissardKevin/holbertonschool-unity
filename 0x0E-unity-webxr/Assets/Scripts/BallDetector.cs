using UnityEngine;

public class BallDetector : MonoBehaviour
{
	[SerializeField] private Animator _anim;
	[SerializeField] private ObstaclesSpawner _spawn;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Interactable"))
		{
			other.GetComponent<BallController>().enabled = true;
			if (!CheckXRDevice.isPresent())
				_anim.SetTrigger("BallPresent");
			_spawn.enabled = true;
		}
	}
}
