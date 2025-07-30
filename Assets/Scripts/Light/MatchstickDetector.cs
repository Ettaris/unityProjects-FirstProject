using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MatchstickDetector : MonoBehaviour
{
    public float detectionRadius = 3f;
    public LayerMask matchstickMask;
    public bool isMatchDetected { get; private set; } = false;
    public bool isEnemyAggressive { get; private set; } = false;
    public List<Vector2> matchstickPosition;
    private Collider2D[] _matchsticks;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MatchstickDetection());
    }

    IEnumerator MatchstickDetection()
    {
        _matchsticks = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, 1, 0), detectionRadius, matchstickMask);
        if (_matchsticks.Length > 0)
        {
            isMatchDetected = true;
            SetEnemyAggressive();
            foreach (Collider2D matchstick in Physics2D.OverlapCircleAll(transform.position + new Vector3(0, 1, 0), detectionRadius, matchstickMask))
            {
                matchstickPosition.Add(matchstick.transform.position);
            }
        }
        else
        {
            isMatchDetected = false;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MatchstickDetection());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + new Vector3(0, 1, 0), detectionRadius);
    }

    public void SetEnemyAggressive()
    {
        if (!isEnemyAggressive)
        {
            isEnemyAggressive = true;
            StartCoroutine(ResetIsEnemyAggressive());
        }
    }

    IEnumerator ResetIsEnemyAggressive()
    {
        yield return new WaitForSeconds(30);
        isEnemyAggressive = false;
    }
}
