using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _obstaclePrefab;
	[SerializeField] private BoxCollider _boxCollider;

	private void Start()
	{
		for (int i = 0; i < Random.Range(2, 10); i++)
			Instantiate(_obstaclePrefab, GetRandomSpawnPoint(), Quaternion.identity);
	}

	Vector3 GetRandomSpawnPoint()
	{
		Vector3 randomSpawnPoint = new Vector3(
			Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x),
			Random.Range(_boxCollider.bounds.min.y, _boxCollider.bounds.max.y),
			Random.Range(_boxCollider.bounds.min.z, _boxCollider.bounds.max.z)
		);

		foreach (Collider collider in Physics.OverlapSphere(randomSpawnPoint, 0.5f))
		{
			if (collider.CompareTag("Obstacle"))
				return GetRandomSpawnPoint();
		}

		return randomSpawnPoint;
	}
}
