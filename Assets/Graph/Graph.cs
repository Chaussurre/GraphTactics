using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    readonly public HashSet<Node> Nodes = new HashSet<Node>();
    readonly public HashSet<Army> Armies = new HashSet<Army>();

    static public Graph Instance;
    public NodeSelectorManager NodeSelectorManager { get; private set; }
    public ArmyRationer ArmyRationer { get; private set; }

    public bool PauseGame = false;

    private void OnEnable() => Instance = this;
    private void OnDisable() => Instance = null;

    private void Start()
    {
        NodeSelectorManager = GetComponentInChildren<NodeSelectorManager>();

        foreach (Edge edge in GetComponentsInChildren<Edge>())
            edge.Left.AddNeighbourg(edge);

        foreach (Node node in GetComponentsInChildren<Node>())
            Nodes.Add(node);

        ArmyRationer = FindObjectOfType<ArmyRationer>();
    }

    public Team GetWinner()
    {
        Team winner = null;

        foreach(Node node in Nodes)
        {
            if (winner == null)
                winner = node.GetTeam();

            if (winner != node.GetTeam())
                return null;
        }
        return winner;
    }
    
    public Node FindClosestNode(Vector3 point)
    {
        Node node = null;
        float distance = float.MaxValue;
        foreach (Node target in Graph.Instance.Nodes)
        {
            float NewDistance = (point - target.transform.position).sqrMagnitude;
            if (distance > NewDistance)
            {
                distance = NewDistance;
                node = target;
            }
        }

        return node;
    }
}
