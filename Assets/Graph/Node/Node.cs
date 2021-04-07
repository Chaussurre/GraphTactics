﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float armySize;

    [SerializeField]
    Team Team;

    readonly public HashSet<Node> Neighbourgs = new HashSet<Node>();

    public void AddNeighbourg(Node node)
    {
        Neighbourgs.Add(node);
        node.Neighbourgs.Add(this);
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

    public void TryAttack(Node target)
    {
        if (Neighbourgs.Contains(target))
        {
            target.Attacked(Team, GetArmySize());
            armySize -= GetArmySize();
        }
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