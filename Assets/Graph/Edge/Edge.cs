using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    [SerializeField]
    Army ArmyPrefab;

    public Node Left;
    public Node Right;

    readonly public HashSet<Army> Armies = new HashSet<Army>();

    public Node GetOtherNode(Node node)
    {
        if (node == Left)
            return Right;
        return Left;
    }

    public void SendArmy(Node from, int armySize)
    {
        Node target = GetOtherNode(from);
        Army army = Instantiate(ArmyPrefab, transform.parent);
        Armies.Add(army);
        army.Send(armySize, from, target, this);
    }

    private void Update()
    {
        foreach (Army army1 in Armies)
            foreach (Army army2 in Armies)
                if(army1.TryCollide(army2))
                    return; //Only deal in one collision at a time
    }
}
