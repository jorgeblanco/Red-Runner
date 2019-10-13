using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform basePos;
    [SerializeField] private MeshRenderer indicator;
    [Header("Materials")]
    [SerializeField] private Material idleMaterial;
    [SerializeField] private Material provokedMaterial;
    [SerializeField] private Material attackingMaterial;
    [SerializeField] private Material searchingMaterial;
    [Header("Properties")]
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float searchTime = 5f;
    [SerializeField] private float attackRange = 1f;
    
    private NavMeshAgent _navMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;
    private EnemyState _state;
    private float _lostTime;

    enum EnemyState
    {
        Idle,
        Provoked,
        Attacking,
        Searching
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        UpdateState();
        switch (_state)
        {
            case EnemyState.Idle:
                _navMeshAgent.SetDestination(basePos.position);
                indicator.material = idleMaterial;
                break;
            case EnemyState.Provoked:
                _navMeshAgent.SetDestination(target.position);
                indicator.material = provokedMaterial;
                break;
            case EnemyState.Attacking:
                AttackTarget();
                indicator.material = attackingMaterial;
                break;
            case EnemyState.Searching:
                indicator.material = searchingMaterial;
                break;
        }
    }

    private void UpdateState()
    {
        _distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (_distanceToTarget <= attackRange)
        {
            _state = EnemyState.Attacking;
        }
        else if (_distanceToTarget <= chaseRange)
        {
            _state = EnemyState.Provoked;
        }
        else if(_distanceToTarget > chaseRange && _state == EnemyState.Provoked)
        {
            _state = EnemyState.Searching;
            _lostTime = Time.time + searchTime;
        }
        else if (_distanceToTarget > chaseRange && Time.time > _lostTime)
        {
            _state = EnemyState.Idle;
        }
    }

    private void AttackTarget()
    {
        Debug.Log("Attacking");
    }

    private void OnDrawGizmosSelected()
    {
        var position = transform.position;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, chaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, attackRange);
    }
}
