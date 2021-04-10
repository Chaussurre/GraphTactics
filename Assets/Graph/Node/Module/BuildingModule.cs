using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModule : MonoBehaviour
{
    Node Node;
    [SerializeField]
    Building Building;

    private void Start()
    {
        Node = GetComponentInParent<Node>();
    }

    private void FixedUpdate()
    {
        Building?.OnUpdate(Time.fixedDeltaTime, Node);
    }

    public Building GetBuilding()
    {
        return Building;
    }
}
