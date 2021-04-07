using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    HashSet<Node> Nodes = new HashSet<Node>();

    [SerializeField, Range(0.1f, 3f)]
    float SoldierGrowth;

    private void Start()
    {
        foreach (Edge edge in GetComponentsInChildren<Edge>())
            edge.Left.AddNeighbourg(edge.Right);

        foreach (Node node in GetComponentsInChildren<Node>())
            Nodes.Add(node);
    }

    private void FixedUpdate()
    {
        foreach (Node node in Nodes)
            if (node.GetTeam() != null)
                node.gainArmy(SoldierGrowth * Time.fixedDeltaTime);
    }
}
