using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class OilPool : MonoBehaviour
{
    public GameObject fireEffect;
    public float burnDuration = 5f;
    public float burnRaisingTime = 1f;
    private bool _isBurning = false;
    private HashSet<GameObject> objectsInside = new HashSet<GameObject>();
    private int burnRaiseTimer;
    private Vector3 fireEffectStartingScale;
    private float originLightIntenstity;
    private Light2D light;

    public void Ignite()
    {
        light = GetComponentInChildren<Light2D>(includeInactive: true);
        if (_isBurning) return;
        _isBurning = true;
        burnRaiseTimer = 0;
        StopAllCoroutines();
        originLightIntenstity = light.intensity;
        fireEffectStartingScale = fireEffect.transform.localScale;
        fireEffect.SetActive(true);
        StartCoroutine(BurnRaiseAndFadeTimer());
        Debug.Log("Was ignited");

        foreach (var obj in objectsInside)
        {
            if (obj != null)
                obj.GetComponent<EnemyController>()?.Burn();
        }

        Invoke(nameof(Extinguish), burnDuration);
    }

    private void Extinguish()
    {
        _isBurning = false;
        StartCoroutine(BurnRaiseAndFadeTimer());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsInside.Add(other.gameObject);

        if (_isBurning && other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>()?.Burn();
        }
    }

    IEnumerator BurnRaiseAndFadeTimer()
    {
        while (burnRaiseTimer < 50 && _isBurning)
        {
            yield return new WaitForSeconds(burnRaisingTime / 50);
            burnRaiseTimer += 1;
            fireEffect.transform.localScale = fireEffectStartingScale / 50 * burnRaiseTimer;
            light.intensity = originLightIntenstity / 50 * burnRaiseTimer;
        }
        while (burnRaiseTimer > 0 && !_isBurning)
        {
            yield return new WaitForSeconds(burnRaisingTime / 50);
            burnRaiseTimer -= 1;
            fireEffect.transform.localScale = fireEffectStartingScale / 50 * burnRaiseTimer;
            light.intensity = originLightIntenstity / 50 * burnRaiseTimer;
            if (burnRaiseTimer == 0)
            {
                fireEffect.SetActive(false);
                fireEffect.transform.localScale = fireEffectStartingScale;
                light.intensity = originLightIntenstity;
            }
        }
        

    }

}
