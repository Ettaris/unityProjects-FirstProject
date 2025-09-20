using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Enemy Options")]
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float attackCooldownTime = 1f;
    public float speed = 2f;
    public int health = 3;
    public float avoidTime = 3f;


    public ParticleSystem burningVFX;

    [Header("Player object")]
    public Transform playerTransform;

    [Header("Don't touch")]
    [SerializeField] private IEnemyState _currentState;


    private Vector2 _startPosition;
    private int _currentHealth;
    public bool _isEnemyRevealed { get; private set; }
    private Vector3 _currentMoveDirection;
    public Animator _animator { get; private set; }
    public MatchstickDetector _matchstickDetector { get; private set; }
    private LightBlocker _lightBlocker;
    private float _avoidTimeValue;

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = FindObjectsByType<PlayerBasicMovement>(FindObjectsSortMode.None).First().transform;
        }
        _startPosition = transform.position;
        _currentHealth = health;
        _animator = GetComponent<Animator>();
        _matchstickDetector = GetComponent<MatchstickDetector>();
        _lightBlocker = GetComponent<LightBlocker>();
        _avoidTimeValue = avoidTime;

        _currentState = new IdleState();
        _currentState.Enter(this);
    }

    private void Update()
    {
        _currentState.Update(this);
        Debug.Log(_currentState);
        //TODO: check state in inspector
        if (!_isEnemyRevealed)
        {
            if (DistanceToPlayer() < chaseRange - 1)
            {
                RevealEnemy();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        Debug.Log("previous state is " + _currentState);
        _currentState.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    public void Burn()
    {
        Debug.Log("Monster is burning");
        //TODO: add dying states
        burningVFX.gameObject.SetActive(true);

        Destroy(gameObject, 1.5f);
    }

    public void TakeDamage(int damagePoints)
    {
        _currentHealth -= damagePoints;
        if (_currentHealth <= 0)
        {
            Debug.Log("Enemy is dying");
            Destroy(gameObject);
        }
    }

    //called via animation events
    public void EnemyAttacks()
    {
        foreach (DamadeByCollision damageByCollisionObject in gameObject.GetComponentsInChildren<DamadeByCollision>(true))
        {
            damageByCollisionObject.enabled = true;
        }
    }

    public void DisableAttackCollision()
    {
        foreach (DamadeByCollision damageByCollisionObject in gameObject.GetComponentsInChildren<DamadeByCollision>(true))
        {
            damageByCollisionObject.enabled = false;
        }
    }

    public void SetCurrentMoveDirection(Vector3 _curMoveDirection) => _currentMoveDirection = _curMoveDirection;

    public Vector3 GetCurrentMoveDirection() => _currentMoveDirection;

    public Vector2 GetStartPosition() => _startPosition;

    public float DistanceToPlayer() => Vector2.Distance(transform.position, playerTransform.position);

    public bool IsMatchstickDetected() => _matchstickDetector.isMatchDetected;

    public bool IsEnemyAggressive() => _matchstickDetector.isEnemyAggressive;

    public List<Vector2> MatchstickPosition() => _matchstickDetector.matchstickPosition;

    public bool IsInStaticLightZone() => _lightBlocker.IsInLightZone();

    public void RevealEnemy()
    {
        if (!_isEnemyRevealed)
        {
            _isEnemyRevealed = true;
            _animator.SetBool("IsKnown", true);
        }
    }

    public void ConcealEnemy()
    {
        if (_isEnemyRevealed)
        {
            _isEnemyRevealed = false;
            _animator.SetBool("IsKnown", false);
        }
    }

    public void ResetAvoidTime()
    {
        avoidTime = _avoidTimeValue;
    }
}

