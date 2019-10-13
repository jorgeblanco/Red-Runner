using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private Transform basePos;
    
    private NavMeshAgent _navMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var targetPosition = target.position;
        _distanceToTarget = Vector3.Distance(targetPosition, transform.position);
        _navMeshAgent.SetDestination(_distanceToTarget <= chaseRange ? targetPosition : basePos.position);
    }
}
