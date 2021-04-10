using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BuildingModuleDisplay : MonoBehaviour
{


    private void Update()
    {
        BuildingModule Module = GetComponentInParent<BuildingModule>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        
        if (Module == null || renderer == null)
            return;

        Module.GetComponent<SpriteRenderer>().enabled = Module.GetBuilding() != null;

        if (Module.GetBuilding() != null)
            renderer.sprite = Module.GetBuilding().GetIcone();
        else
            renderer.sprite = null;
    }
}
