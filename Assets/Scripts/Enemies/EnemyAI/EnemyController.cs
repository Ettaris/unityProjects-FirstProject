using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //TODO: add headers
    public int health = 3;

    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float attackCooldownTime = 1f;
    public float speed = 2f;
    public Transform playerTransform;

    private Vector2 _startPosition;
    private IEnemyState _currentState;
    private int _currentHealth;
    public Animator _animator { get; private set; }
    public MatchstickDetector _matchstickDetector { get; private set; }

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = FindObjectsByType<PlayerBasicMovement>(FindObjectsSortMode.None).First().transform;
            Debug.Log(playerTransform.tag);
        }
        _startPosition = transform.position;
        _currentHealth = health;
        _animator = GetComponent<Animator>();
        _matchstickDetector = GetComponent<MatchstickDetector>();


        _currentState = new IdleState();
        _currentState.Enter(this);
    }

    private void Update()
    {
        _currentState.Update(this);
    }

    public void ChangeState(IEnemyState newState)
    {
        _currentState.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    public void Burn()
    {
        Debug.Log("Monster is burning");
        //TODO: add dying states

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


    public Vector2 GetStartPosition() => _startPosition;

    public float DistanceToPlayer() => Vector2.Distance(transform.position, playerTransform.position);
    
    public bool IsMatchstickDetected() => _matchstickDetector.isMatchDetected;

    public bool IsEnemyAggressive() => _matchstickDetector.isEnemyAggressive;
    
    public List<Vector2> MatchstickPosition() => _matchstickDetector.matchstickPosition;
}

