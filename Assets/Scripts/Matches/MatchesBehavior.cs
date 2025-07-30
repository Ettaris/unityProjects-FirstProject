using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MatchesBehavior : MonoBehaviour
{
    [Header("Burn Settings")]
    [SerializeField] private float _timeToBurn = 20f;

    [SerializeField] private float _distanceToStop = .1f;
    [SerializeField] private float _slowDownRadius = .4f;

    private Rigidbody2D _rb;
    private Light2D _light;
    private BoxCollider2D _boxCollider;
    private bool _isAlreadyDying = false;
    private bool _isStopped = false;
    private Vector2 _targetPosition = Vector2.zero;
    private ParticleSystem _fireTrail;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _light = GetComponentInChildren<Light2D>();
        _fireTrail = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_timeToBurn >= 0)
            _timeToBurn -= Time.deltaTime;
        if (!_isAlreadyDying && _timeToBurn <= 0)
        {
            _isAlreadyDying = true;
            StartCoroutine(SlowFade());
        }


        if (!_isStopped)
        {
            float distance = Vector2.Distance(transform.position, _targetPosition);
            //TODO: if cannot reach target position.
            if (distance <= _distanceToStop)
            {
                _rb.linearVelocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Kinematic; // Stops further physics movement
                _boxCollider.isTrigger = true;
                _isStopped = true;
            }
            else if (distance < _slowDownRadius)
            {
                // Gradually slow down
                float t = distance / _slowDownRadius; // 0..1
                float adjustedSpeed = Mathf.Lerp(0.2f, _rb.linearVelocity.magnitude, t);
                Vector2 dir = (_targetPosition - (Vector2)transform.position).normalized;
                if (_rb.linearVelocity.magnitude > 0.6f)
                    _rb.AddForce(1.3f * adjustedSpeed * -dir);
                else if (distance < _slowDownRadius / 5)
                {
                    _rb.linearVelocity = dir * adjustedSpeed;
                    
                }
            }



        }


    }

    IEnumerator SlowFade(float lightDecrease = .04f, float secondsStep = .015f)
    {
        while(_light.pointLightOuterRadius > 0)
        {
            _light.pointLightOuterRadius -= lightDecrease;
            yield return new WaitForSeconds(secondsStep);
        }
        if (_light.pointLightOuterRadius < 0)
            _light.pointLightOuterRadius = 0;
        StopCoroutine(SlowFade());
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out OilPool oilpool))
        {
            oilpool.Ignite();
            Destroy(gameObject);
        }
    }

    public void SetTargetPosition(Vector2 targetPosition) => _targetPosition = targetPosition;
}
