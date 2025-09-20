using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("Colours")]
    public Color defaultColor;
    public Color currentLocationColor;

    public static MapManager Instance;

    public RectTransform mapContainer;
    public List<MapLocation> allLocations;

    private int _currentLocationID = 1;

    void Awake()
    {
        Instance = this;
        HideAll();
    }

    void HideAll()
    {
        allLocations.AddRange(mapContainer.GetComponentsInChildren<MapLocation>());
        foreach (var loc in allLocations)
            loc.Hide();
    }

    public void SetCurrentLocation(int locationID)
    {
        MapLocation newLocation = allLocations.Find(l => l.locationID == locationID);
        MapLocation OldLocation = allLocations.Find(l => l.locationID == _currentLocationID);
        if (newLocation != null)
        {
            newLocation.Reveal();
            OldLocation.GetComponent<Image>().color = defaultColor;
            newLocation.GetComponent<Image>().color = currentLocationColor;
            _currentLocationID = newLocation.locationID;
        }
    }
}

