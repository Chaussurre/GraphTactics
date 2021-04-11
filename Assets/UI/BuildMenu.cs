using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    List<Button> buttons = new List<Button>();

    Building BuildingCreated = null;
    void Start()
    {
        buttons.AddRange(GetComponentsInChildren<Button>());
    }

    public void CreateBuilding(Building building)
    {
        BuildingCreated = building;
    }

    public bool GetCreatedBuilding(out Building building)
    {
        building = BuildingCreated; //TODO
        BuildingCreated = null;
        return building != null;
    }
}
