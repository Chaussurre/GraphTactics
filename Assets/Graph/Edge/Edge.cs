using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    [SerializeField]
    ArmyMove ArmyPrefab;

    public Node Left;
    public Node Right;

    readonly public HashSet<Army> armies = new HashSet<Army>();

    public Node GetOtherNode(Node node)
    {
        if (node == Left)
            return Right;
        return Left;
    }

    public void SendArmy(Node from, int armySize)
    {
        Node target = GetOtherNode(from);
        ArmyMove army = Instantiate(ArmyPrefab, transform.parent);
        army.Send(armySize, from, target);
    }
}
