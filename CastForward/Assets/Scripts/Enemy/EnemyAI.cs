using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private PlayerController _player;
    public NavMeshAgent navMeshAgent;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _speed = 2.0f;
    [Header("Patrolling")]
    Vector3 walkPoint;
    private bool _walkPointSet;
    private bool _processing = true;
    public float walkPointRange;
    public bool inStaticAnimation = false;

    [Header("Attacking")]
    [SerializeField] private float _timeBetweenAttacks;
    [HideInInspector] public bool isAttacking = false;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public UnityEvent Attack;
    public float projectileForce = 0.1f;

    private void Awake()
    {
        if (navMeshAgent != null) Initialize();
    }

    public void Initialize()
    {
        _player = FindAnyObjectByType<PlayerController>();
        Attack ??= new();
        if (navMeshAgent != null) navMeshAgent.speed = _speed;
    }
    private void OnEnable()
    {
        EnemyEntity enemyEntity = gameObject.GetComponent<EnemyEntity>();
        enemyEntity.OnDeath += StopAI;
    }

    private void OnDisable()
    {
        EnemyEntity enemyEntity = gameObject.GetComponent<EnemyEntity>();
        enemyEntity.OnDeath -= StopAI;
    }
    private void Update()
    {
        if (!_processing) return;
        Collider[] sight = Physics.OverlapSphere(transform.position, sightRange);
        Collider[] attack = Physics.OverlapSphere(transform.position, attackRange);
        playerInSightRange = sight.Any(c => c.CompareTag("Player"));
        playerInAttackRange = attack.Any(c => c.CompareTag("Player"));

        if (!playerInSightRange && !playerInAttackRange && !inStaticAnimation) Patrolling();
        if (playerInSightRange && !playerInAttackRange && !inStaticAnimation) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    private void Patrolling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet) navMeshAgent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) _walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        Vector2 randomPoint = Random.insideUnitCircle * walkPointRange;
        walkPoint = new Vector3(randomPoint.x + transform.position.x,transform.position.y,randomPoint.y + transform.position.z);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, _whatIsGround))
            _walkPointSet = true;
    }
    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(_player.transform.position);
    }
    public void AttackPlayer()
    {
        float projectileTimeToTarget = (Vector3.Distance(transform.position, _player.transform.position)) / projectileForce;
        float randMod = Random.Range(0.9f, 1.1f);
        projectileTimeToTarget *= randMod;
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player.transform.position + _player.rb.velocity * projectileTimeToTarget,Vector3.up);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        if (!isAttacking)
        {
            Attack.Invoke();

            isAttacking = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void ResetAttack ()
    {
        isAttacking = false;
    }

    void StopAI()
    {
        navMeshAgent.SetDestination(transform.position);
        _processing = false;
        Collider collider = GetComponent<Collider>();
        if (collider != null) collider.enabled = false;
    }

    public void Resurrect() {
        navMeshAgent.SetDestination(transform.position);
        _processing = true;
        Collider collider = GetComponent<Collider>();
        if (collider != null) collider.enabled = true;
    }

    public void ModifySpeed(float multiplier)
    {
        _speed *= multiplier;
        navMeshAgent.speed = _speed;
    }
}
