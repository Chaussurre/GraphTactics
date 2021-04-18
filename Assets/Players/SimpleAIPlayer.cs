using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAIPlayer : Player
{
    [SerializeField]
    int DefenseSizeTrigger;
    [SerializeField]
    int AttackDifference;

    Node lastActionFrom;

    protected override bool Action(out Node from, out Node target, out int Size)
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
                if(otherNode.GetArmySize() <= node.GetArmySize() - AttackDifference && otherNode.GetTeam() != Team)
                {
                    from = node;
                    target = otherNode;
                    Size = from.GetArmySize();
                    return true;
                }
            }

        
        foreach(Node node in PotentialDefenders)
            if(node.GetArmySize() >= DefenseSizeTrigger)
            {
                if (node == lastActionFrom)
                    continue;

                from = node;
                lastActionFrom = node;
                target = FirstStep(from, GreatestNode(PotentialAttackers));
                Size = from.GetArmySize();
                return true;
            }
            

        from = null;
        target = null;
        Size = 0;
        return false;
    }

    private bool SeeEnemy(Node node)
    {
        foreach (Edge edge in node.Neighbourgs)
            if (edge.GetOtherNode(node).GetTeam() != Team)
                return true;

        return false;
    }

    static private Node GreatestNode(HashSet<Node> nodes)
    {
        Node result = null;
        float size = -1;
        foreach(Node node in nodes)
            if (node.GetArmySize() > size)
            {
                size = node.GetArmySize();
                result = node;
            }

        return result;
    }

    private Node FirstStep(Node from, Node target)
    {
        List<Node> NodeSeen = new List<Node>() { target };

        while(NodeSeen.Count > 0)
        {
            Node node = NodeSeen[0];
            NodeSeen.RemoveAt(0);

            if (node == null)
                continue;

            foreach(Edge edge in node.Neighbourgs)
            {
                Node other = edge.GetOtherNode(node);
                if (other == from)
                    return node;
                if (other.GetTeam() == target.GetTeam())
                    NodeSeen.Add(other);
            }
        }

        return null; //No path found ?
    }

    protected override bool CreateBuilding(out Node node, out Building buildingPrefab)
    {
        node = null;
        buildingPrefab = null;
        return false; //TODO
    }
}
