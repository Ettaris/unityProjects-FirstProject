using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MatchstickDetector : MonoBehaviour
{
    public float detectionRadius = 4f;
    public Vector3 detectionRadiusOffset = Vector3.zero;

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
        _matchsticks = Physics2D.OverlapCircleAll(transform.position + detectionRadiusOffset, detectionRadius, matchstickMask);
        if (_matchsticks.Length > 0)
        {
            isMatchDetected = true;
            SetEnemyAggressive();
            foreach (Collider2D matchstick in Physics2D.OverlapCircleAll(transform.position + detectionRadiusOffset, detectionRadius, matchstickMask))
            {
                matchstickPosition.Add(matchstick.transform.position);
            }
        }
        else
        {
            isMatchDetected = false;
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(MatchstickDetection());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + detectionRadiusOffset, detectionRadius);
    }

    public void SetEnemyAggressive()
    {
        StopCoroutine(ResetIsEnemyAggressive());
        isEnemyAggressive = true;
        //TODO: is it need?   StartCoroutine(ResetIsEnemyAggressive());
    }

    IEnumerator ResetIsEnemyAggressive()
    {
        yield return new WaitForSeconds(60);
        isEnemyAggressive = false;
    }
}
