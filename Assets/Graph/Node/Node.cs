using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float armySize;

    [SerializeField]
    Team Team;

    readonly public HashSet<Edge> Neighbourgs = new HashSet<Edge>();


    public void AddNeighbourg(Edge edge)
    {
        Neighbourgs.Add(edge);
        edge.GetOtherNode(this).Neighbourgs.Add(edge);
    }


    public void Attacked(Team attacker, int armySize)
    {
        int defendingArmy = GetArmySize();
        int resultArmy = defendingArmy;

        if (Team != attacker)
            resultArmy -= armySize;
        else
            resultArmy += armySize;

        if(resultArmy < 0)
        {
            Team = attacker;
            resultArmy *= -1;
        }

        this.armySize = resultArmy;
    }

    public bool TryAttack(Node target)
    {
        foreach (Edge edge in Neighbourgs)
            if (edge.GetOtherNode(this) == target)
            {
                edge.SendArmy(this, GetArmySize());
                armySize -= GetArmySize();

                return true;
            }
        return false;
    }

    public void gainArmy(float gain)
    {
        armySize += gain;
        armySize = Mathf.Min(armySize, 100);
    }

    public int GetArmySize()
    {
        return Mathf.FloorToInt(armySize);
    }
    public Team GetTeam()
    {
        return Team;
    }
}
