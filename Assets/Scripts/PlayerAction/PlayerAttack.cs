using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    [Header("Attack Settings")]
    public float attackCooldown = 0.5f;
    public float attackRange = 0.5f;
    public int damage = 1;

    public Animator animator;
    private float _lastAttackTime;
    private bool _isReadyToAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //TODO: make realisation via InputSystem
        /*
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack"); // Play swing animation
                lastAttackTime = Time.time;
            }
        }
        */
        if (Time.time - _lastAttackTime >= attackCooldown)
        {
            _isReadyToAttack = true;
            _lastAttackTime = Time.time;
        }
    }

    // Called from Animation Event!
    public void DealSwordDamage()
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>()?.TakeDamage(damage);
        }

        Debug.Log("Sword damage applied.");
    }

    public void InputAttack(InputAction.CallbackContext context)
    {
        //TODO: clean it
        //if (context.performed && _isReadyToAttack) {
        //    _isReadyToAttack = false;
        //    animator.SetTrigger("Attack");
        //}

    }
}