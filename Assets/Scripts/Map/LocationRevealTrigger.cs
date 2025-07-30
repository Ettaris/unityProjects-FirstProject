using UnityEngine;

public class LocationSetTrigger : MonoBehaviour
{
    public int locationID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MapManager.Instance.SetCurrentLocation(locationID);
        }
    }
}

