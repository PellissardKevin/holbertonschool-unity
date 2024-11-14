using UnityEngine;

public class Booster : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Interactable"))
		{
			BallController controller = other.GetComponent<BallController>();
			controller.rb.velocity *= 2.05f;
		}
	}
}
