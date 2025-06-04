using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

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

    private void Start()
    {
        _startPosition = transform.position;
        _currentState = new IdleState();
        _currentState.Enter(this);
        _currentHealth = health;
        if (playerTransform == null)
        {
            playerTransform = FindObjectsByType<PlayerBasicMovement>(FindObjectsSortMode.None).First().transform;
            Debug.Log(playerTransform.tag);
        }
        _animator = GetComponent<Animator>();
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

        Destroy(gameObject, 1f);
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

    public Vector2 GetStartPosition() => _startPosition;

    public float DistanceToPlayer() => Vector2.Distance(transform.position, playerTransform.position);
}

