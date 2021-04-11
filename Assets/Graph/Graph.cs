using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    readonly public HashSet<Node> Nodes = new HashSet<Node>();
    readonly public HashSet<Army> Armies = new HashSet<Army>();

    static public Graph Instance;
    public NodeSelectorManager NodeSelectorManager { get; private set; }

    private void Start()
    {
        Instance = this;
        NodeSelectorManager = GetComponentInChildren<NodeSelectorManager>();

        foreach (Edge edge in GetComponentsInChildren<Edge>())
            edge.Left.AddNeighbourg(edge);

        foreach (Node node in GetComponentsInChildren<Node>())
            Nodes.Add(node);
    }
}
