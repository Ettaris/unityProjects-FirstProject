using UnityEngine;
using System.Collections.Generic;

public class OilPool : MonoBehaviour
{
    public GameObject fireEffect;  
    public float burnDuration = 5f;
    private bool _isBurning = false;
    private HashSet<GameObject> objectsInside = new HashSet<GameObject>();

    public void Ignite()
    {
        if (_isBurning) return;
        _isBurning = true;
        fireEffect.SetActive(true);
        Debug.Log("Was ignited");

        foreach(var obj in objectsInside )
        {
            obj.GetComponent<EnemyController>()?.Burn();
        }

        Invoke(nameof(Extinguish), burnDuration);
    }

    private void Extinguish()
    {
        _isBurning = false;
        fireEffect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsInside.Add(other.gameObject);

        if (_isBurning && other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>()?.Burn();
        }
    }

}
