using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    List<Button> buttons = new List<Button>();

    Building BuildingCreated = null;

    [SerializeField]
    Image Panel;
    void Start()
    {
        buttons.AddRange(GetComponentsInChildren<Button>());
        Panel.color = FindObjectOfType<HumanPlayer>().Team.GetColor(); 
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
