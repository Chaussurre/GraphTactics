using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAIPlayer : Player
{
    [SerializeField]
    int DefenseSizeTrigger;
    [SerializeField]
    int AttackDifference;
    protected override bool Action(out Node from, out Node target)
    {
        HashSet<Node> PotentialAttackers = new HashSet<Node>();
        HashSet<Node> PotentialDefenders = new HashSet<Node>();

        foreach (Node node in Team.Nodes)
            if (SeeEnemy(node))
                PotentialAttackers.Add(node);
            else
                PotentialDefenders.Add(node);

        foreach(Node node in PotentialAttackers)
            foreach(Edge edge in node.Neighbourgs)
            {
                Node otherNode = edge.GetOtherNode(node);
                if(otherNode.GetArmySize() < node.GetArmySize() - AttackDifference && otherNode.GetTeam() != Team)
                {
                    from = node;
                    target = otherNode;
                    return true;
                }
            }

        foreach(Node node in PotentialDefenders)
            if(node.GetArmySize() >= DefenseSizeTrigger)
            {
                from = node;
                target = GreatestNeighbourg(node);
                return true;
            }

        from = null;
        target = null;
        return false;
    }

    private bool SeeEnemy(Node node)
    {
        foreach (Edge edge in node.Neighbourgs)
            if (edge.GetOtherNode(node).GetTeam() != Team)
                return true;

        return false;
    }

    private Node GreatestNeighbourg(Node node)
    {
        Node result = null;
        float size = -1;
        foreach(Edge edge in node.Neighbourgs)
        {
            Node other = edge.GetOtherNode(node);
            if (other.GetArmySize() > size)
            {
                size = other.GetArmySize();
                result = other;
            }
        }

        return result;
    }

    protected override bool CreateBuilding(out Node node, out Building buildingPrefab)
    {
        node = null;
        buildingPrefab = null;
        return false; //TODO
    }
}
