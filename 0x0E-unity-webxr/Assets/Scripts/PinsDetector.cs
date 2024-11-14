using UnityEngine;

public class PinsDetector : MonoBehaviour
{
	[SerializeField] private ScoreUpdater _score;
	private bool _hasScored = false;

	private void Update()
	{
		if (!_hasScored && Mathf.Abs(transform.rotation.z) > 0.04f)
		{
			_score.Score++;
			_hasScored = true;
			Destroy(gameObject, 1f);
		}
	}
}
