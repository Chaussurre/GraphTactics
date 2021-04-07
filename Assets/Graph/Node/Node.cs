using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    int armySize;

    [SerializeField]
    Team Team;

    readonly public HashSet<Node> Neighbourgs = new HashSet<Node>();

    public void AddNeighbourg(Node node)
    {
        Neighbourgs.Add(node);
        node.Neighbourgs.Add(this);
    }

    public int GetArmySize()
    {
        return armySize;
    }

    public Team GetTeam()
    {
        return Team;
    }
}
