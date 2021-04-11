using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModule : MonoBehaviour
{
    Node Node;
    [SerializeField]
    public Building BuildingPrefab;

    Building Building = null;
    private void Start()
    {
        Node = GetComponentInParent<Node>();
    }

    private void FixedUpdate()
    {
        if (Graph.Instance.PauseGame)
            return;

        if (Building == null || BuildingPrefab == null || Building.GetType() != BuildingPrefab.GetType())
            ChangeBuilding();

        Building?.OnUpdate(Time.fixedDeltaTime, Node);
    }

    void ChangeBuilding()
    {
        if (Building != null)
            Destroy(Building.gameObject);

        if (BuildingPrefab != null)
            Building = Instantiate(BuildingPrefab, transform);
        else
            Building = null;
    }

    public Building GetBuilding()
    {
        return Building;
    }
}
