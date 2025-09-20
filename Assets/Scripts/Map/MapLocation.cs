using UnityEngine;
using UnityEngine.UI;

public class MapLocation : MonoBehaviour
{
    public int locationID;
    public bool isDiscovered = false;
    public bool isCurrent = false;

    public void Reveal()
    {
        if (!isDiscovered)
        {
            isDiscovered = true;
            gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        isDiscovered = false;
        gameObject.SetActive(false);
    }

    public void SetCurrentLocation()
    {
        isCurrent = true;
        //TODO: make this location brighter than others
    }
}

