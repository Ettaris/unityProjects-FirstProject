using UnityEngine;

public class LocationSetTrigger : MonoBehaviour
{
    [Header("location ID. Set null if you want automate")]
    public int locationID;

    private void Start()
    {
        if (locationID == 0)
        {
            locationID = GetComponentInParent<Checkpoint>().GetCheckpointID();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MapManager.Instance.SetCurrentLocation(locationID);
        }
    }
}

