using Unity.AI.Navigation;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Manages the target's movements.
/// </summary>
public class TargetMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    public int Points { get; set; } = 10;
    public ARPlane Plane { get; set; }

    private void OnEnable() => RandomizeTargetPosition();

    private void Update()
    {
        if (_navMeshAgent.remainingDistance < 0.1f)
            RandomizeTargetPosition();
    }

    private void RandomizeTargetPosition()
    {
        Vector3 randomPoint = RandomPointOnNavMesh(30f);

        while (Vector3.Distance(randomPoint, transform.position) < 1.0f)
            randomPoint = RandomPointOnNavMesh(30f);

        _navMeshAgent.SetDestination(randomPoint);
    }

    private Vector3 RandomPointOnNavMesh(float samplingRadius)
    {
        NavMeshHit hit;
        Vector3 randomPoint = Vector3.zero;

        Vector3 randomPosition = transform.position + Random.insideUnitSphere * samplingRadius;

        if (NavMesh.SamplePosition(randomPosition, out hit, samplingRadius, NavMesh.AllAreas))
            randomPoint = hit.position;

        return new Vector3(randomPoint.x, Plane.transform.position.y, randomPoint.z);
    }
}