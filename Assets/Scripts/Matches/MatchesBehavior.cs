using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MatchesBehavior : MonoBehaviour
{
    [SerializeField] private float _timeToDie = 20f;

    private Rigidbody2D _rb2d;
    private Light2D _light;
    private bool _isAlreadyDying = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _light = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeToDie >= 0)
            _timeToDie -= Time.deltaTime;
        if (!_isAlreadyDying && _timeToDie <= 0)
        {
            _isAlreadyDying = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        while(_light.pointLightOuterRadius > 0)
        {
            _light.pointLightOuterRadius -= 0.05f;
            yield return new WaitForSeconds(0.02f);
        }
        if (_light.pointLightOuterRadius < 0)
            _light.pointLightOuterRadius = 0;
        StopCoroutine(Fade());
        Destroy(this.gameObject);
    }


}
