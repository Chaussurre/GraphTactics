using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    HashSet<Node> Nodes = new HashSet<Node>();


    private void Start()
    {
        foreach (Edge edge in GetComponentsInChildren<Edge>())
            edge.Left.AddNeighbourg(edge);

        foreach (Node node in GetComponentsInChildren<Node>())
            Nodes.Add(node);
    }
}
