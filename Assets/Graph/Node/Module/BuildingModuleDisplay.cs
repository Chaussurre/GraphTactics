using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BuildingModuleDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject RangeDisplay = null;

    private void Update()
    {
        BuildingModule Module = GetComponentInParent<BuildingModule>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        
        if (Module == null || renderer == null)
            return;

        Building building = Module.GetBuilding();
        
        Module.GetComponent<SpriteRenderer>().enabled = building != null;
        
        if (building != null)
        {
            renderer.sprite = building.GetIcone();
            DisplayRange(building);
        }
        else
            renderer.sprite = null;
    }

    void DisplayRange(Building building)
    {
        if (RangeDisplay == null)
            return;
        
        Node node = GetComponentInParent<Node>();

        Color color = node.GetTeam().GetColor();
        color = new Color(color.r, color.g, color.b, building.ZoneAlpha);

        RangeDisplay.GetComponent<SpriteRenderer>().color = color;

        RangeDisplay.transform.localScale = new Vector3(1, 1) * building.Range * 2 + Vector3.forward;
    }
}
