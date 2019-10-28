using System;
using AssetPacks.Yurowm.Demo.Scripts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform basePos;
    [Header("Properties")]
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float searchTime = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private int baseDamage = 5;
    [SerializeField] private float velocityThreshold;
    [Header("SFX")]
    [SerializeField] private float stepSfxDelay = 0.5f;
    [SerializeField] private AudioClip[] footsteps;
    
    private NavMeshAgent _navMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;
    private EnemyState _state;
    private float _lostTime;
    private Actions _actions;
    private IDamageable _targetDamageable;
    private bool _lastSeen;
    private bool _dead;
    private float? _nextAttack;
    private AudioSource _audioSource;
    private float _nextStepTime;

    enum EnemyState
    {
        Idle,
        Provoked,
        Attacking,
        Searching
    }

    private void Start()
    {
        if (target == null)
        {
            AcquireTarget();
        }
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _actions = GetComponentInChildren<Actions>();
        _targetDamageable = target.GetComponent<IDamageable>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(_dead) {return;}
        
        UpdateState();
        UpdateAnimState();
        UpdateAction();
        UpdateSfx();
    }

    private void UpdateSfx()
    {
        if (Vector3.Distance(_navMeshAgent.velocity, Vector3.zero) > velocityThreshold && Time.time > _nextStepTime)
        {
            if (footsteps.Length > 0)
            {
                var footstep = footsteps[Random.Range(0, footsteps.Length)];
                _audioSource.pitch = Random.Range(0.75f, 1.25f);
                _audioSource.PlayOneShot(footstep);
                _nextStepTime = Time.time + stepSfxDelay;
            }
        }
    }

    private void UpdateAction()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                _navMeshAgent.SetDestination(basePos.position);
                _lastSeen = false;
                _nextAttack = null;
                break;
            case EnemyState.Provoked:
                _navMeshAgent.SetDestination(target.position);
                _lastSeen = false;
                _nextAttack = null;
                break;
            case EnemyState.Attacking:
                FaceTarget();
                TryAttack();
                break;
            case EnemyState.Searching:
                if (!_lastSeen)
                {
                    _navMeshAgent.SetDestination(target.position);
                    _lastSeen = true;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateState()
    {
        _distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (_distanceToTarget <= attackRange)
        {
            _state = EnemyState.Attacking;
            return;
        }
        if (_distanceToTarget <= chaseRange)
        {
            if(_state == EnemyState.Provoked) return;

            var position = transform.position;
            // Add one unit to the origin to account for the character height
            Vector3 origin = new Vector3(position.x, position.y + 1, position.z);
            Debug.DrawRay(origin, target.position - origin, Color.yellow);
            
            if (Physics.Raycast(
                origin,
                target.position - origin,
                out var hit,
                chaseRange
                ))
            {
                if (hit.collider.GetComponentInChildren<PlayerHealth>() != null)
                {
                    _state = EnemyState.Provoked;
                    return;
                }
            }
        }
        if(_distanceToTarget > chaseRange && _state == EnemyState.Provoked)
        {
            _state = EnemyState.Searching;
            _lostTime = Time.time + searchTime;
            return;
        }
        if (_state == EnemyState.Searching && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _state = EnemyState.Idle;
            return;
        }
        if (Time.time > _lostTime)
        {
            _state = EnemyState.Idle;
            return;
        }
    }

    private void UpdateAnimState()
    {
        if(_state == EnemyState.Provoked || _state == EnemyState.Searching)
        {
           _actions.Run();
        }
        else if (_state == EnemyState.Attacking)
        {
            if (_nextAttack == null)
            {
               _actions.Attack();
            }
        }
        else if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
        {
            _actions.Walk();
        }
        else
        {
            _actions.Stay();
        }
    }

    private void AcquireTarget()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
        if (target == null)
        {
            throw new ApplicationException("Target not found");
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

    private void TryAttack()
    {
        if (_nextAttack == null)
        {
            _nextAttack = Time.time + attackDelay;
        }
        else if (_nextAttack <= Time.time)
        {
            _nextAttack = null;
            AttackTarget();
        }
    }
    
    private void AttackTarget()
    {
        _targetDamageable?.TakeDamage(baseDamage);
    }

    public void OnDamageTaken()
    {
        _state = EnemyState.Provoked;
        _actions.Damage();
    }

    public void OnDeath()
    {
        _dead = true;
        _actions.Death();
        GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;
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
