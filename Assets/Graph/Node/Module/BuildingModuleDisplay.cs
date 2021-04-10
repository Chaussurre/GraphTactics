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
        if (building == null)
            building = Module.BuildingPrefab;


        if (building != null)
        {
            Module.GetComponent<SpriteRenderer>().enabled = true;
            renderer.sprite = building.GetIcone();
            DisplayRange(building.Range, building.ZoneAlpha);
        }
        else
        {
            Module.GetComponent<SpriteRenderer>().enabled = false;
            renderer.sprite = null;
            DisplayRange(0, 0);
        }
    }

    void DisplayRange(float size, float alpha)
    {
        if (RangeDisplay == null)
            return;
        
        Node node = GetComponentInParent<Node>();

        Color color = node.GetTeam().GetColor();
        color = new Color(color.r, color.g, color.b, alpha);

        RangeDisplay.GetComponent<SpriteRenderer>().color = color;

        RangeDisplay.transform.localScale = new Vector3(1, 1) * size * 2 + Vector3.forward;
    }
}
