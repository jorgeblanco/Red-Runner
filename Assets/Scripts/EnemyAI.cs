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
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private int baseDamage = 5;
    
    private NavMeshAgent _navMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;
    private EnemyState _state;
    private float _lostTime;
    private Animator _animator;
    private IDamageable _targetDamageable;
    
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Move = Animator.StringToHash("move");
    private static readonly int Attack = Animator.StringToHash("attack");
    private bool _lastSeen;

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
        _animator = GetComponent<Animator>();
        _targetDamageable = target.GetComponent<IDamageable>();
    }

    private void Update()
    {
        UpdateState();
        UpdateAnimState();
        UpdateAction();
        UpdateMaterial();
    }

    private void UpdateAction()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                _navMeshAgent.SetDestination(basePos.position);
                _lastSeen = false;
                break;
            case EnemyState.Provoked:
                _navMeshAgent.SetDestination(target.position);
                _lastSeen = false;
                break;
            case EnemyState.Attacking:
                FaceTarget();
                break;
            case EnemyState.Searching:
                if (!_lastSeen)
                {
                    _navMeshAgent.SetDestination(target.position);
                    _lastSeen = true;
                }
                break;
        }
    }

    private void UpdateMaterial()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                indicator.material = idleMaterial;
                break;
            case EnemyState.Provoked:
                indicator.material = provokedMaterial;
                break;
            case EnemyState.Attacking:
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

    private void UpdateAnimState()
    {
        if(_state == EnemyState.Provoked || _state == EnemyState.Searching)
        {
           _animator.SetTrigger(Move); 
        }
        else if (_state == EnemyState.Attacking)
        {
           _animator.SetTrigger(Attack); 
        }
        else if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _animator.SetTrigger(Idle);
        }
    }

    private void FaceTarget()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            lookRotation, 
            Time.deltaTime * rotationSpeed  // Reduce the rotation speed for more natural turning
            );
    }
    
    private void AttackTarget()
    {
        Debug.Log("Attacking");
        _targetDamageable?.TakeDamage(baseDamage);
    }

    public void OnDamageTaken()
    {
        _state = EnemyState.Provoked;
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
